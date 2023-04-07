using Diplom.Managers.Enemy;
using Diplom.Units.Enemy;
using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Diplom.UI.Enemy
{
    public class EnemyParametersUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _waveText;
        [SerializeField]
        private TMP_Text _healthText;
        [SerializeField]
        private TMP_Text _manaText;
        [SerializeField]
        private TMP_Text _strengthText;
        [SerializeField]
        private TMP_Text _agilityText;
        [SerializeField]
        private TMP_Text _intellegenceText;
        [SerializeField]
        private EnemyManager _enemyManager;

        private EnemyBattleComponent _enemyBattle;
        private EnemyStatsComponent _enemyStats;
        private PlayerController _playerController;


        // Start is called before the first frame update
        void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
            _waveText.text = string.Concat("Wave= -");
            _healthText.text = string.Concat("Health= -");
            _manaText.text = string.Concat("Mana= -");
            _strengthText.text = string.Concat("None");
            _agilityText.text = string.Concat("None");
            _intellegenceText.text = string.Concat("None");
        }

        // Update is called once per frame
        void Update()
        {
            if (_playerController.Enemy != null)
            {
                _enemyBattle = _playerController.EnemyBattleStat;
                _enemyStats = _playerController.EnemyStat;
                _waveText.text = string.Concat("Wave= ", _enemyStats.Wave);
                _healthText.text = string.Concat("Health= ", _enemyBattle.Health);
                _manaText.text = string.Concat("Mana= ", _enemyStats.EnemyMana);
                _strengthText.text = string.Concat(_enemyStats.Strength);
                _agilityText.text = string.Concat(_enemyStats.Agility);
                _intellegenceText.text = string.Concat(_enemyStats.Intellegence);
            }
            if (_playerController.EnemyBuilding != null)
            {
    
                _waveText.text = string.Concat("Wave= -");
                _healthText.text = string.Concat("Health= ", _playerController.EnemyBuilding.Health);
                _manaText.text = string.Concat("Mana= 0");
                _strengthText.text = string.Concat("None");
                _agilityText.text = string.Concat("None");
                _intellegenceText.text = string.Concat("None");
            }
        }

    }
}