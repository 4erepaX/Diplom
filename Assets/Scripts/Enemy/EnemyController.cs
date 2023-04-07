using Diplom.Buildings;
using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Diplom.Units.Enemy
{

    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private PlayerController _target;
        [SerializeField]
        private BuildingComponent _targetBuild;
        private TriggerComponent[] _triggers;
        private Rigidbody _body;
        private Animator _animator;
        
        private bool _isFinalWave;
        // Start is called before the first frame update
        void Start()
        {
            _body = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _triggers = GetComponentsInChildren<TriggerComponent>();
            StartCoroutine(MoveForward());
            _targetBuild = FindObjectsOfType<BuildingComponent>().Where(t => t.Side == SideType.Friendly).FirstOrDefault();
            _target = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            
            if (_target != null && Vector3.Distance(_target.transform.position, transform.position) < 10)
            {
                transform.LookAt(_target.transform);
                Attack(_target.transform,2);
            }
            if (_targetBuild != null && (_target == null || Vector3.Distance(_target.transform.position, transform.position) > 10))
            {
                transform.LookAt(_targetBuild.transform);
                Attack(_targetBuild.transform,4);
            }
        }
        private void Attack(Transform target,float range)
        {
            if (Vector3.Distance(target.position, transform.position) < range)
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
        }
        private IEnumerator MoveForward()
        {
            while (true)
            {
                _body.velocity = transform.forward * _moveSpeed * Time.fixedDeltaTime;
                _animator.SetFloat("Movement",Mathf.Abs( _body.velocity.z));
                yield return null;
            }
        }
        private void OnCollider_UnityEvent(AnimationEvent data)
        {
            var trigger = _triggers.FirstOrDefault(t => t.GetID == data.intParameter);

            trigger.Enable = data.floatParameter == 1;
        }

    }
}