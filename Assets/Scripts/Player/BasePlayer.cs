using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Diplom.Enemy;

namespace Diplom.Player
{
    public abstract class BasePlayer : MonoBehaviour

    {

        [SerializeField]
        private int[] _level= new int[10];
        [SerializeField]
        private int _strength;
        [SerializeField]
        private int _agility;
        [SerializeField]
        private int _intellegence;


        [SerializeField, Range(1f, 10f), Tooltip("Скорость движения персонажа")]
        private float _forwardMoveSpeed = 3f;

        private Animator _animator;
        private int _hp;
        private int _mp;
        private float _attack;
        private EnemyController _enemy;


        protected Rigidbody _body;

        

        private void OnEnable()
        {
            _animator = GetComponentInChildren<Animator>();
            _body = GetComponentInChildren<Rigidbody>();

        }
        protected void Movement()
        {
            
            var position = Mouse.current.position.ReadValue();
            var targetMove = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 19.5f));
            var ray = Camera.main.ScreenPointToRay(position);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.GetComponent<EnemyController>()!=null)              
                    _enemy = hit.collider.GetComponent<EnemyController>();                
                else 
                    _enemy = null;
            }
            if (_enemy == null) StartCoroutine(MoveForward(targetMove));
            if (_enemy != null) StartCoroutine(MoveToEnemy());

        }
        private IEnumerator MoveForward(Vector3 target)
        {
            _animator.SetBool("Attack", false) ;
            while (_enemy == null)
            {
                
                var velocity = _body.velocity;
                transform.LookAt(target);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                velocity.z = _forwardMoveSpeed * Time.fixedDeltaTime;
                _body.velocity = transform.forward * velocity.z;
                _animator.SetFloat("Movement", velocity.z);
                if (Vector3.Distance(target, transform.position) < 0.5)
                {
                    _body.velocity = new Vector3(0f, 0f, 0f);
                    _animator.SetFloat("Movement", 0f);
                    StopCoroutine(MoveForward(target));
                }
                yield return new WaitForFixedUpdate();
            }

        }
        private IEnumerator MoveToEnemy()
        {
            while (_enemy!=null)
            {
                var target = _enemy.transform.position;
                var velocity = _body.velocity;
                Debug.Log(Vector3.Distance(target, transform.position));
                transform.LookAt(target);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                velocity.z = _forwardMoveSpeed * Time.fixedDeltaTime;
                _body.velocity = transform.forward * velocity.z;
                _animator.SetFloat("Movement", velocity.z);
                if (Vector3.Distance(target, transform.position) < 2)
                {
                    _body.velocity = new Vector3(0f, 0f, 0f);
                    _animator.SetFloat("Movement", 0f);
                    _animator.SetBool("Attack", true);
                }
                else
                {
                    _animator.SetBool("Attack", false);
                }
                yield return new WaitForFixedUpdate();
            }

        }
        protected void Levels()
        {

        }
    }
}