using Diplom.Managers.Enemy;
using Diplom.Projectile;
using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Units.Enemy
{
    public class EnemyBattleComponent : MonoBehaviour
    {
        private float _health;
        private Animator _animator;
        private EnemyStatsComponent _stats;
        private bool _isDie;

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
            if (_health <= 0)
            {

                _animator.SetTrigger("Die");
                _health = 0;
                _isDie = true;
                _enemyManager.KillEnemy(gameObject);
            }
            
        }
        private void OnDie_UnityEvent(AnimationEvent data)
        {
            if (data.intParameter == 0) Destroy(gameObject);
        }
    }
}