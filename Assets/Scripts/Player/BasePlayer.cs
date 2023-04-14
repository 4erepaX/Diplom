using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Diplom.Units.Enemy;
using System.Linq;
using Diplom.Buildings;
using Diplom.UI.Enemy;
using TMPro;

namespace Diplom.Units.Player
{
    public abstract class BasePlayer : MonoBehaviour

    {
        [SerializeField]
        private PlayerType _type;
        private Animator _animator;
        private float _forwardMoveSpeed;
        private EnemyController _enemy;
        private EnemyBattleComponent _enemyBattleStat;
        private EnemyStatsComponent _enemyStat;
        private TriggerComponent[] _colliders;
        private PlayerStatsComponent _stats;
        protected Rigidbody _body;
        private int _range;
        private bool _isMoving;
        [SerializeField]
        private LayerMask _mask;
        private BuildingComponent _enemyBuilding;
        private Vector3 _targetMove;
        public EnemyController Enemy => _enemy;
        public EnemyBattleComponent EnemyBattleStat=>_enemyBattleStat;
        public EnemyStatsComponent EnemyStat=>_enemyStat;
        public BuildingComponent EnemyBuilding => _enemyBuilding;
        
        private void OnEnable()
        {
            switch (_type)
            {
                case PlayerType.Warrior:
                    _range = 2;
                    break;
                case PlayerType.Wizzard:
                    _range = 8;
                    break;

            }
            _colliders = GetComponentsInChildren<TriggerComponent>();
            _animator = GetComponentInChildren<Animator>();
            _body = GetComponentInChildren<Rigidbody>();
            _stats = GetComponentInChildren<PlayerStatsComponent>();
            
        }
        protected void Movement()
        {
            if (gameObject.activeSelf)
            {
                _forwardMoveSpeed = _stats.GetMoveSpeed;
                var position = Mouse.current.position.ReadValue();

                var ray = Camera.main.ScreenPointToRay(position);

                if (Physics.Raycast(ray, out var hit))
                {

                    if (Mathf.Pow(2, hit.collider.gameObject.layer) == _mask.value)
                    {
                        _targetMove = hit.point;
                        if (_enemy == null)
                        {
                            
                            StartCoroutine(MoveForward(_targetMove));
                            
                        }
                    }
                    if (hit.collider.GetComponent<EnemyController>() != null)
                    {
                        _enemy = hit.collider.GetComponent<EnemyController>();
                        _enemyBattleStat = hit.collider.GetComponent<EnemyBattleComponent>();
                        _enemyStat = hit.collider.GetComponent<EnemyStatsComponent>();

                    }
                    else
                    {
                        _enemy = null;
                        _enemyBattleStat = null;
                        _enemyStat = null;
                    }
                    if (hit.collider.GetComponent<BuildingComponent>() != null)
                    {
                        _enemyBuilding = hit.collider.GetComponent<BuildingComponent>();
                        if (_enemyBuilding.Side == SideType.Friendly) _enemyBuilding = null;
                    }
                    else
                        _enemyBuilding = null;
                    if (_enemy != null || _enemyBuilding != null) MoveTarget();
                }
            }
        }
        private IEnumerator MoveForward(Vector3 target)
        {
            _animator.SetBool("Attack", false) ;
            _isMoving = true;
            while (Vector3.Distance(target, transform.position) > 0.5)
            {
                var velocity = _body.velocity;
                transform.LookAt(target);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                velocity.z = _forwardMoveSpeed * Time.fixedDeltaTime;
                _body.velocity = transform.forward * velocity.z;
                _animator.SetFloat("Movement", velocity.z);
                if (target!=_targetMove) break;
                yield return new WaitForFixedUpdate();
            }
            if (Vector3.Distance(target, transform.position) <= 0.5)
            {
                _body.velocity = new Vector3(0f, 0f, 0f);
                _animator.SetFloat("Movement", 0f);
                _isMoving = false;

            }
            yield return null;
        }
        private void MoveTarget()
        {
            StartCoroutine(MoveToEnemy(_range));
        }
        private IEnumerator MoveToEnemy(float range)
        {
            while (_enemy != null || _enemyBuilding != null)
            {
                if (_enemy != null)
                {
                    _targetMove = _enemy.transform.position;
                    var velocity = _body.velocity;
                    transform.LookAt(_targetMove);
                    transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                    velocity.z = _forwardMoveSpeed * Time.fixedDeltaTime;
                    _body.velocity = transform.forward * velocity.z;
                    _animator.SetFloat("Movement", velocity.z);
                    AttackEnemy(_targetMove, range);
                }
                if (_enemyBuilding != null)
                {
                    _targetMove = _enemyBuilding.transform.position;
                    var velocity = _body.velocity;
                    transform.LookAt(_targetMove);
                    transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                    velocity.z = _forwardMoveSpeed * Time.fixedDeltaTime;
                    _body.velocity = transform.forward * velocity.z;
                    _animator.SetFloat("Movement", velocity.z);
                    AttackEnemyHouse(_targetMove, range);

                }

                
                yield return new WaitForFixedUpdate();
            }
            
        }

        
        private void AttackEnemy(Vector3 target, float range)
        {
            if (!_enemyBattleStat.IsDie)
            {
               
                if (Vector3.Distance(target, transform.position) < range)
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
            if (_enemyBattleStat.IsDie)
            {
                _body.velocity = new Vector3(0f, 0f, 0f);
                _animator.SetFloat("Movement", 0f);
                _targetMove = transform.position;
                _enemy = null;
                _enemyBattleStat = null;
                _enemyStat = null;
                _animator.SetBool("Attack", false);

            }
        }
        private void AttackEnemyHouse(Vector3 target, float range)
        {
                if (Vector3.Distance(target, transform.position) < range+2.5)
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
        private void OnCollider_UnityEvent(AnimationEvent data)
        {
            var collider = _colliders.FirstOrDefault(t => t.GetID == data.intParameter);

            collider.Enable = data.floatParameter == 1;
        }
    }
}