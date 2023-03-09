using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Diplom
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


        [SerializeField, Range(100f, 200f), Tooltip("Скорость движения персонажа")]
        private float _forwardMoveSpeed = 10f;

        private float _minSpeed = 2f;
        private float _maxSpeed = 10f;
        private Animator _animator;
        private int _hp;
        private int _mp;
        private float _attack;

        protected Rigidbody _body;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            _body = GetComponent<Rigidbody>();

        }
        protected void Movement()
        {
            var position = Mouse.current.position.ReadValue();
            var targetMove = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 19.5f));
            StartCoroutine(MoveForward(targetMove));

        }
        private IEnumerator MoveForward(Vector3 target)
        {
            while (true)
            {
                var velocity = _body.velocity;
                transform.LookAt(target);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                velocity.z = Mathf.Clamp(velocity.z + _forwardMoveSpeed * Time.fixedDeltaTime, _minSpeed, _maxSpeed);
                _body.velocity = transform.forward * _forwardMoveSpeed * Time.fixedDeltaTime;
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
        protected void Levels()
        {

        }
    }
}