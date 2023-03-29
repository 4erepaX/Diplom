using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Diplom.Units.Enemy;
using System.Linq;

namespace Diplom.Units.Player
{
    public abstract class BasePlayer : MonoBehaviour

    {
        private Animator _animator;
        private float _forwardMoveSpeed;
        private EnemyController _enemy;
        private TriggerComponent[] _colliders;
        private PlayerStatsComponent _stats;
        protected Rigidbody _body;

        public EnemyController Enemy => _enemy;

        private void OnEnable()
        {
            _colliders = GetComponentsInChildren<TriggerComponent>();
            _animator = GetComponentInChildren<Animator>();
            _body = GetComponentInChildren<Rigidbody>();
            _stats = GetComponentInChildren<PlayerStatsComponent>();
            
        }
        protected void Movement()
        {
            _forwardMoveSpeed = _stats.GetMoveSpeed;
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
                transform.LookAt(target);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                velocity.z = _forwardMoveSpeed * Time.fixedDeltaTime;
                _body.velocity = transform.forward * velocity.z;
                _animator.SetFloat("Movement", velocity.z);
                if (Vector3.Distance(target, transform.position) < 2)
                {
                    _body.velocity = new Vector3(0f, 0f, 0f);
                    _animator.SetFloat("Movement", 0f);
                    if (!_animator.GetBool("Die")) _animator.SetBool("Attack", true);
                    else _animator.SetBool("Attack", false);
                }
                else
                {
                    _animator.SetBool("Attack", false);
                }
                yield return new WaitForFixedUpdate();
            }
        
        }
        

        private void OnCollider_UnityEvent(AnimationEvent data)
        {
            var collider = _colliders.FirstOrDefault(t => t.GetID == data.intParameter);

            collider.Enable = data.floatParameter == 1;
        }
    }
}