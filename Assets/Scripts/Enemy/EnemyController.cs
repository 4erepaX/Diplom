using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Units.Enemy
{
    
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed;

        private Rigidbody _body;
        private Animator _animator;
        // Start is called before the first frame update
        void Start()
        {
            _body = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            _body.velocity= transform.forward * _moveSpeed * Time.fixedDeltaTime;
            _animator.SetFloat("Movement", _body.velocity.z);
        }
    
    }
}