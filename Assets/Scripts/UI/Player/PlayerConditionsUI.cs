using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Diplom.UI.Player
{
    public class PlayerConditionsUI : MonoBehaviour
    {

        [SerializeField]
        private TMP_Text _levelText;
        [SerializeField]
        private TMP_Text _healthText;
        [SerializeField]
        private TMP_Text _manaText;
        [SerializeField]
        private TMP_Text _experienceText;
        [SerializeField]
        private Slider _healthSlider;
        [SerializeField]
        private Slider _manaSlider;
        [SerializeField]
        private Slider _experienceSlider;

        private PlayerStatsComponent _playerStats;
        private PlayerBattleComponent _playerBattle;
        // Start is called before the first frame update
        private void Awake()
        {
            _playerStats = FindObjectOfType<PlayerStatsComponent>();
            _playerBattle= FindObjectOfType<PlayerBattleComponent>();
        }
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            
            if (_playerStats != null)
            {
                _levelText.text = string.Concat("Level ", _playerStats.Level.ToString());
                _healthSlider.value = _playerBattle.Health / _playerStats.Health;
                _healthText.text = _playerBattle.Health.ToString();
                _manaSlider.value= _playerBattle.Mana / _playerStats.Mana;
                _manaText.text = _playerBattle.Mana.ToString();
                _experienceSlider.value = _playerBattle.Experience / _playerStats.Experience;
                _experienceText.text = _playerBattle.Experience.ToString();
        
            }
            
        }

    }
}