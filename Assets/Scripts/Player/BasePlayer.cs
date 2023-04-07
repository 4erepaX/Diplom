using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Diplom.Units.Enemy;
using System.Linq;
using Diplom.Buildings;
using Diplom.UI.Enemy;

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
        [SerializeField]
        private LayerMask _mask;
        private BuildingComponent _enemyBuilding;

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
                    _range = 5;
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
                        var targetMove = hit.point;
                        if (_enemy == null) StartCoroutine(MoveForward(targetMove));
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
                    if (_enemy != null || _enemyBuilding != null) StartCoroutine(MoveTarget());
                }
            }
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
        private IEnumerator MoveTarget()
        {
            while (_enemy != null || _enemyBuilding != null)
            {
                Vector3 target;
                if (_enemy != null)
                {
                    target = _enemy.transform.position;
                    MoveToEnemy(target, _range);
                }
                if (_enemyBuilding != null)
                {
                    target = _enemyBuilding.transform.position;
                    MoveToEnemy(target, _range + 3f);
                }
                yield return new WaitForFixedUpdate();
            }
        
        }
        private void MoveToEnemy(Vector3 target, float range)
        {
            var velocity = _body.velocity;
            transform.LookAt(target);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
            velocity.z = _forwardMoveSpeed * Time.fixedDeltaTime;
            _body.velocity = transform.forward * velocity.z;
            _animator.SetFloat("Movement", velocity.z);
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
        

        private void OnCollider_UnityEvent(AnimationEvent data)
        {
            var collider = _colliders.FirstOrDefault(t => t.GetID == data.intParameter);

            collider.Enable = data.floatParameter == 1;
        }
    }
}