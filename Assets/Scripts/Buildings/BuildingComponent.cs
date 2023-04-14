using Diplom.Projectile;
using Diplom.Units.Enemy;
using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Diplom.Buildings
{
    public class BuildingComponent : MonoBehaviour
    {
        [SerializeField]
        private SideType _side;
        [SerializeField]
        private float _health;
        [SerializeField]
        private float _defence;
        [SerializeField]
        private TMP_Text _healthText;
        public SideType Side => _side;
        public float Health => _health;
        public float Defence => _defence;
        private void Start()
        {
            _health = 10000;
            _defence = 1000;
        }
        private void Update()
        {
            _healthText.text = string.Concat("Health: ",_health);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (_side == SideType.Friendly)
            {
                var enemy = other.GetComponentInParent<EnemyStatsComponent>();
                if (enemy != null && (other.isTrigger))
                {
                    _health -= Mathf.Clamp( enemy.EnemyAttack - _defence / 10,1,float.MaxValue);
                }
                if (_health <= 0)
                {
                    _health = 0;
                }
            }
            if (_side == SideType.Enemy)
            {
                var enemy = other.GetComponentInParent<PlayerStatsComponent>();
                if (other.GetComponent<ProjectileMove>() != null)
                {
                    enemy = FindObjectOfType<PlayerStatsComponent>();
                    Destroy(other.gameObject);
                }
                if (enemy != null && other.isTrigger && enemy.GetComponentInParent<PlayerController>().Enemy == this.GetComponent<EnemyController>())
                {
                    _health -= enemy.Attack - _defence / 10;

                }
                if (_health <= 0)
                {
                    _health = 0;
                    Destroy(gameObject);
                }

            }

        }
    }
}