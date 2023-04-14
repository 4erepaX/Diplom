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
        
        private float _moveSpeed;
        [SerializeField]
        private PlayerController _target;
        [SerializeField]
        private BuildingComponent _targetBuild;
        private TriggerComponent[] _triggers;
        private Rigidbody _body;
        private Animator _animator;
        [SerializeField]
        private EnemyStatsComponent _stats;
        [SerializeField]
        private EnemyBattleComponent _battleStats;
        private PlayerBattleComponent _playerBattleStats;
        private bool _isFinalWave;

        public PlayerController Target => _target;
        // Start is called before the first frame update
        void Start()
        {
            _body = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _triggers = GetComponentsInChildren<TriggerComponent>();           
            _targetBuild = FindObjectsOfType<BuildingComponent>().Where(t => t.Side == SideType.Friendly).FirstOrDefault();
            _stats = GetComponent<EnemyStatsComponent>();
            _battleStats = GetComponent<EnemyBattleComponent>();
            StartCoroutine(FindPlayer());
            StartCoroutine(MoveForward());
        }
        private IEnumerator FindPlayer()
        {
            while (_target == null)
            {
                _target = FindObjectOfType<PlayerController>();
                _playerBattleStats = _target.GetComponent<PlayerBattleComponent>();
                yield return null;
            }
        }    
        // Update is called once per frame
        private void FixedUpdate()
        {
            _moveSpeed = _stats.MoveSpeed;
            if (_target != null && Vector3.Distance(_target.transform.position, transform.position) < 10 && !_playerBattleStats.IsDie)
            {
                
                transform.LookAt(_target.transform);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                if (_playerBattleStats != null) AttackPlayer(_target.transform,2);
            }
            if (_targetBuild != null && (_target == null || Vector3.Distance(_target.transform.position, transform.position) > 10))
            {
                
                transform.LookAt(_targetBuild.transform);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                AttackBuilding(_targetBuild.transform,4);
            }
        }
        private void AttackPlayer(Transform target,float range)
        {
            if (!_playerBattleStats.IsDie)
            {
                if (Vector3.Distance(target.position, transform.position) < range)
                {
                    _body.velocity = new Vector3(0f, 0f, 0f);
                    _animator.SetFloat("Movement", 0f);
                    _animator.SetBool("Attack", true);
                   if (_battleStats.IsDie)
                    {
                        _animator.SetBool("Attack", false);
                        _body.velocity = new Vector3(0f, 0f, 0f);
                        _animator.SetFloat("Movement", 0f);
                    }
                }
                else
                {
                    _animator.SetBool("Attack", false);
                }
            }
            
        }
        private void AttackBuilding(Transform target, float range)
        {

                if (Vector3.Distance(target.position, transform.position) < range)
                {
                    _body.velocity = new Vector3(0f, 0f, 0f);
                    _animator.SetFloat("Movement", 0f);
                    _animator.SetBool("Attack", true);
                    if (_battleStats.IsDie)
                    {
                        _animator.SetBool("Attack", false);
                        _body.velocity = new Vector3(0f, 0f, 0f);
                        _animator.SetFloat("Movement", 0f);
                    }
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
                if (_battleStats.IsDie) break;
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