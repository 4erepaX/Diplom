using Diplom.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Units.Enemy
{
    public class EnemyStatsComponent : MonoBehaviour
    {
        [SerializeField]
        private EnemyWaveConfiguration _enemyWave;

        private float _enemyHealth;
        private float _enemyMana;
        private float _enemyAttack;
        private float _enemyDefence;
        private byte _wave;
        private int _strength;
        private int _agility;
        private int _intellegence;

        public float EnemyHealth => _enemyHealth;
        public float EnemyMana => _enemyMana;
        public float EnemyAttack => _enemyAttack;
        public float EnemyDefence => _enemyDefence;


        // Start is called before the first frame update
        void Start()
        {
            //Перенести в gameManager
            _wave = 1;
            GetWaveParameters();
            SetWaveParameters();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void GetWaveParameters()
        {
            _strength = _enemyWave.Wave[_wave - 1].Strength;
            _agility = _enemyWave.Wave[_wave - 1].Agility;
            _intellegence = _enemyWave.Wave[_wave - 1].Intellegence;
        }
        private void SetWaveParameters()
        {
            _enemyHealth = _strength * 100;
            _enemyMana = _intellegence * 100;
            _enemyAttack = _strength * 5 + _agility * 5 + _intellegence * 5;
            _enemyDefence = _agility * 10;
        }

    }
}