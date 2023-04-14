using Diplom.Managers;
using Diplom.ScriptableObjects;
using Diplom.UI.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Units.Enemy
{
    public class EnemyStatsComponent : MonoBehaviour
    {
        [SerializeField]
        private EnemyWaveConfiguration _enemyWave;
        [SerializeField]
        private float _moveSpeed;

        private float _enemyHealth;
        private float _enemyMana;
        private float _enemyAttack;
        private float _enemyDefence;
        private int _gold;
        private int _experience;
        private byte _wave;
        private int _strength;
        private int _agility;
        private int _intellegence;
        private GameManager _gameManager;
        private float _startAttack;
        private float _startDefence;
        private float _startMoveSpeed;

        public byte Wave => _wave;
        public int Strength=> _strength;
        public int Agility=>_agility;
        public int Intellegence=>_intellegence;
        public float EnemyHealth => _enemyHealth;
        public float EnemyMana => _enemyMana;
        public float EnemyAttack => _enemyAttack;
        public float EnemyDefence => _enemyDefence;
        public int DropGold => _gold;
        public int DropExperience => _experience;
        public float MoveSpeed => _moveSpeed;
        // Start is called before the first frame update
        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
           _wave = _gameManager.Wave;
            GetWaveParameters();
            SetWaveParameters();
            SetDrop();
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
        private void SetDrop()
        {
            _gold = _enemyWave.Wave[_wave - 1].Gold;
            _experience = _enemyWave.Wave[_wave - 1].Experience;
        }
        public void DecreaseParameters(PlayerSkillComponent _skill)
        {
            switch (_skill.DebaffType)
            {
                case DebaffType.DecreaseAttack:
                    Debug.Log(_enemyAttack);
                    _startAttack = _enemyAttack;
                    _enemyAttack = 80 * _enemyAttack / 100;
                    StartCoroutine(SkillCancelled(_skill.SkillTime, DebaffType.DecreaseAttack, _startAttack));
                    break;
                case DebaffType.DecreaseDefense:
                    _startDefence = _enemyDefence;
                    _enemyDefence = 80 * _enemyDefence / 100;
                    StartCoroutine(SkillCancelled(_skill.SkillTime, DebaffType.DecreaseDefense, _startDefence));
                    break;
                case DebaffType.Slowdown:
                    _startMoveSpeed = _moveSpeed;
                    _moveSpeed= 80 * _moveSpeed / 100;
                    StartCoroutine(SkillCancelled(_skill.SkillTime, DebaffType.Slowdown, _startMoveSpeed));
                    break;
            }
        }
        private IEnumerator SkillCancelled(float time, DebaffType _type, float startEnemyParameter)
        {
            yield return new WaitForSeconds(time);
            switch (_type)
            {
                case DebaffType.DecreaseAttack:
                    _enemyAttack = startEnemyParameter;
                    break;
                case DebaffType.DecreaseDefense:
                    _enemyDefence = startEnemyParameter;
                    break;
                case DebaffType.Slowdown:
                    _moveSpeed = startEnemyParameter;
                    break;
            }
            
            
        }
    }
}