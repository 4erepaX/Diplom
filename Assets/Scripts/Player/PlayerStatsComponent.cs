using Diplom.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Diplom.Units.Player
{
    public class PlayerStatsComponent : MonoBehaviour
    {
        
        [SerializeField]
        private PlayerLevelConfiguration _levelConfiguration;
        [SerializeField]
        private PlayerBattleComponent _playerBattle;
        [SerializeField, Range(100f, 200f), Tooltip("Скорость движения персонажа")]
        private float _moveSpeed = 100f;

        private float _health;
        private float _mana;
        private float _attack;
        private float _defence;
        private float _defaultHealth;
        private float _defaultMana;
        private float _defaultAttack;
        private float _defaultDefence;
        private float _addedHealth;
        private float _addedMana;
        private float _addedAttack;
        private float _addedDefence;
        private float _experience;
        private int _strength;
        private int _agility;
        private int _intellegence;
        private byte _level;



        public float Health => _health;
        public float Mana => _mana;
        public float Experience => _experience;
        public float Attack => _attack;
        public float Defence => _defence;
        public byte Level => _level;
        public int Strength=> _strength;
        public int Agility=>_agility;
        public int Intellegence=>_intellegence;
        public float GetMoveSpeed => _moveSpeed;
        private bool LevelUp()
        {
            if (_level < 10 && _playerBattle.Experience >= _experience) return true;
            else return false;
        }

        private void GetLevelParameters()
        {
            _strength = _levelConfiguration.Levels[_level - 1].Strength;
            _agility = _levelConfiguration.Levels[_level - 1].Agility;
            _intellegence = _levelConfiguration.Levels[_level - 1].Intellegence;
            _experience = _levelConfiguration.Levels[_level - 1].Experience;
        }
        private void SetDefaultParameters()
        {
            _defaultHealth = _strength * 100;
            _defaultMana = _intellegence * 100;
            _defaultAttack = _strength * 5 + _agility * 5 + _intellegence * 5;
            _defaultDefence = _agility * 10;
        }

        private void SetAddedParameters()
        {
            _addedHealth = 0;
            _addedMana = 0;
            _addedAttack = 0;
            _addedDefence = 0;
        }
        private void SetParameters()
        {
            _health = _defaultHealth + _addedHealth;
            _mana = _defaultMana + _addedMana;
            _attack = _defaultAttack + _addedAttack;
            _defence = _defaultDefence + _addedDefence;
            
        }
        private void Awake()
        {
            
            _level = 1;
            GetLevelParameters();
            SetDefaultParameters();
            SetAddedParameters();
            SetParameters();
        }
        
        private void FixedUpdate()
        {
           
            if (LevelUp())
            {
                Debug.Log("Работает?");
                _level++;
                GetLevelParameters();
                SetDefaultParameters();
                SetParameters();
                _playerBattle.LevelUp();
            }
        }


    }
}