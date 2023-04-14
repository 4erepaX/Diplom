using Diplom.ScriptableObjects;
using Diplom.UI.Player;
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
        private int _addStrength;
        private int _addAgility;
        private int _addIntellegence;
        private byte _level;
        private float _hp;
        private float _mp;
        private float _startAttack;
        public float HP => _hp;
        public float MP => _mp;
        public float Health => _health;
        public float Mana => _mana;
        public float Experience => _experience;
        public float Attack => _attack;
        public float Defence => _defence;
        public byte Level => _level;
        public int Strength=> _strength;
        public int Agility=>_agility;
        public int Intellegence=>_intellegence;
        public int AddStrength => _addStrength;
        public int AddAgility => _addAgility;
        public int AddIntellegence => _addIntellegence;
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
            _addedHealth = _addStrength*100;
            _addedMana = _addIntellegence*100;
            _addedAttack = _addStrength * 5 + _addAgility * 5 + _addIntellegence * 5;
            _addedDefence = _addAgility * 10;
        }
        private void SetParameters()
        {
            float lasthp = _health;
            float lastmp = _mana;
            _health = _defaultHealth + _addedHealth;
            _mana = _defaultMana + _addedMana;
            _attack = _defaultAttack + _addedAttack;
            _defence = _defaultDefence + _addedDefence;
            if (lasthp != 0)
            _hp= _playerBattle.Health+((_health * 100 / lasthp - 100) * _playerBattle.Health) /(_playerBattle.Health * 100 / lasthp );
            if (lastmp!=0)
            _mp = _playerBattle.Mana + ((_mana * 100 / lastmp - 100) * _playerBattle.Mana) / (_playerBattle.Mana * 100 / lastmp); ;
        }
        private void Awake()
        {
            _playerBattle = GetComponent<PlayerBattleComponent>();
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
                _level++;
                GetLevelParameters();
                SetDefaultParameters();
                SetParameters();
                _playerBattle.LevelUp();
            }
        }
        public void SetAddStats(int Strength, int Agility, int Intellegence)
        {
            _addStrength += Strength;
            _addAgility += Agility;
            _addIntellegence += Intellegence;
            SetAddedParameters();
            SetParameters();
        }
        public void IncreaseParameters(PlayerSkillComponent _skill)
        {
            switch (_skill.Type)
            {
                case SkillType.Baff:
                    _startAttack = _attack;
                    _attack = 120 * _attack / 100;
                    StartCoroutine(SkillCancelled(_skill.SkillTime, SkillType.Baff, _startAttack));
                    break;
            }
        }
        private IEnumerator SkillCancelled(float time, SkillType _type, float startEnemyParameter)
        {
            yield return new WaitForSeconds(time);
            switch (_type)
            {
                case SkillType.Baff:
                    _attack = _startAttack;
                    break;
            }
        }
    }
}