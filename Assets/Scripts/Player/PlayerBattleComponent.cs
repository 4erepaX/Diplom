using Diplom.Managers.Enemy;
using Diplom.Spawners.Player;
using Diplom.UI.Player;
using Diplom.UI.Shop;
using Diplom.Units.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Units.Player
{
    public class PlayerBattleComponent : MonoBehaviour
    {
        [SerializeField]
        private Collider _collider;
        private Animator _animator;
        private PlayerStatsComponent _stats;
        [SerializeField]
        private EnemyStatsComponent _enemyStats;
        [SerializeField]
        private EnemyBattleComponent _enemyBattle;

        private PlayerSpawner _spawner;
        private PlayerController _playerController;
        private float _health;
        private float _mana;
        private float _experience = 0;
        private int _gold;
        private bool _isDie;
        public float Health => _health;
        public float Mana => _mana;
        public float Experience => _experience;
        public int Gold => _gold;
        public bool IsDie => _isDie;
        // Start is called before the first frame update
        void Start()
        {
            _collider = GetComponent<Collider>();
            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
            _stats = GetComponent<PlayerStatsComponent>();
            _spawner = FindObjectOfType<PlayerSpawner>();
            _health = _stats.Health;
            _mana = _stats.Mana;
            _isDie = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponentInParent<EnemyStatsComponent>();
            if (enemy != null && (other.isTrigger))
            {
                _health -= enemy.EnemyAttack - _stats.Defence / 10;
            }
            if (_health <= 0)
            {
                transform.position = new Vector3(0f, 0f, 0f);
                _animator.SetTrigger("Die");
                _health = 0;
                _isDie = true;
            }
        }
        private void OnDie_UnityEvent(AnimationEvent data)
        {
            _spawner.OnRespawn(gameObject, this);
            if (data.intParameter == 0) gameObject.SetActive(false);

        }
        private void OnFire_UnityEvent(AnimationEvent data)
        {

            if (data.intParameter == 1 && _animator.GetBool("Attack"))
            {
                var _projectile = GetComponent<PlayerProjectile>();
                Instantiate(_projectile.Projectile, _projectile.FirePoint);
            }
        }
        public void LevelUp()
        {

            _health = _stats.Health;
            _mana = _stats.Mana;
            _experience = 0;

        }

        public void GetDrop(int exp, int gold)
        {
            _experience += exp;
            _gold += gold;
        }

        public void OnRespawn()
        {
            _health = _stats.Health;
            _mana = _stats.Mana;
            _animator.SetBool("Attack", false);
            _isDie = false;
        }
        public bool RestoreHP(int RestoreSize)
        {
            if (_health == _stats.Health) return false;
            if (_health + RestoreSize >= _stats.Health)
            {
                _health = _stats.Health;
            }
            if (_health + RestoreSize < _stats.Health)
            {
                _health += RestoreSize;
            }
            return true;
        }
        public bool RestoreMP(int RestoreSize)
        {
            if (_mana == _stats.Mana) return false;
            if (_mana + RestoreSize >= _stats.Mana)
            {
                _mana = _stats.Mana;
            }
            if (_mana + RestoreSize < _stats.Mana)
            {
                _mana += RestoreSize;
            }
            return true;
        }
        public void Parameters()
        {
            _health = _stats.HP;
            _mana = _stats.MP;
        }
        public bool BuyItem(ItemShopComponent itemShop)
        {
            if (_gold >= itemShop.Cost)
            {
                _gold -= itemShop.Cost;
                return true;
            }
            return false;
        }
        public bool BuySkill(SkillShopComponent skillShop)
        {
            if (_gold >= skillShop.Cost)
            {
                _gold -= skillShop.Cost;
                return true;
            }
            return false;
        }
        public bool ManaCost(PlayerSkillComponent _skill)
        {
            if (_mana < _skill.Cost) return false;
            _mana -= _skill.Cost;
            return true;
        }
    }
}