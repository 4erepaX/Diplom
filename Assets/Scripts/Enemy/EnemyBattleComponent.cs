using Diplom.Managers.Enemy;
using Diplom.Projectile;
using Diplom.UI.Player;
using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Diplom.Units.Enemy
{
    public class EnemyBattleComponent : MonoBehaviour
    {
        [SerializeField]
        private Slider _healthBar;
        [SerializeField]
        private GameObject _canvas;
        private float _health;
        private Animator _animator;
        private EnemyStatsComponent _stats;
        private PlayerStatsComponent _playerStats;
        [SerializeField]
        private PlayerBattleComponent _playerBattleStats;
        private bool _isDie;
        private EnemyController _enemy;
        private EnemyManager _enemyManager;
        
        public float Health =>_health;
        public bool IsDie => _isDie;
        private void Start()
        {
            _isDie = false;
            _stats = GetComponent<EnemyStatsComponent>();
            _enemyManager = FindObjectOfType<EnemyManager>();
            _animator = GetComponent<Animator>();
            _health = _stats.EnemyHealth;
            _enemy = GetComponent<EnemyController>();
            StartCoroutine(FindPlayer());
        }
        private IEnumerator FindPlayer()
        {
            while (true)
            {
                if (_enemy.Target != null)
                {
                    _playerStats = _enemy.Target.GetComponent<PlayerStatsComponent>();
                    _playerBattleStats = _enemy.Target.GetComponent<PlayerBattleComponent>();
                    break;
                }
                yield return null;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            
            var enemy = other.GetComponentInParent<PlayerStatsComponent>();
            if (other.GetComponent<ProjectileMove>() != null)
            {
                enemy = FindObjectOfType<PlayerStatsComponent>();
                Destroy(other.gameObject);
            }
            if (enemy != null && other.isTrigger && enemy.GetComponentInParent<PlayerController>().Enemy==this.GetComponent<EnemyController>())
            {
                _health -= enemy.Attack - _stats.EnemyDefence / 10;
               
            }
            OnDie();
            
        }
        private void OnDie()
        {
            if (_health <= 0)
            {
                if (!_isDie) _playerBattleStats.GetDrop(_stats.DropExperience, _stats.DropGold);
                _animator.SetTrigger("Die");
                _health = 0;
                _isDie = true;
                _enemyManager.KillEnemy(gameObject);

            }
        }
        private void FixedUpdate()
        {
            _healthBar.value = _health / _stats.EnemyHealth;
            _canvas.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        private void OnDie_UnityEvent(AnimationEvent data)
        {
            if (data.intParameter == 0) Destroy(gameObject);
        }
        public void ReceiveSkill(PlayerSkillComponent _skill)
        {
            switch (_skill.Type)
            {
                case SkillType.Debaff:
                    _stats.DecreaseParameters(_skill);
                    break;
                case SkillType.Attack:
                    _health -= _playerStats.Attack * 2;
                    OnDie();
                    break;
            }
        }
    }
}