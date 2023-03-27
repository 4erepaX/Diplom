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
        private PlayerStatsComponent _stats;
        private float _health;
        private float _mana;
        private int _expirience;
        public float Health => _health;
        public float Mana => _mana;
        public float Experience => _expirience;
        // Start is called before the first frame update
        void Start()
        {
            _collider = GetComponent<Collider>();
            _stats = GetComponent<PlayerStatsComponent>();
            _health = _stats.Health;
            _mana = _stats.Mana;
            _expirience = _stats.Experience;
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponentInParent<EnemyStatsComponent>();
            if (enemy != null && (other.isTrigger))
            {
                _health -= enemy.EnemyAttack - _stats.Defence / 10;
            }
           
        }
    }
}