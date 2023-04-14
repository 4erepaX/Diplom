using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Diplom.UI.Enemy
{
    public class EnemyPanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _healthText;
        private PlayerController _player;
        // Start is called before the first frame update
        void Start()
        {
            _player = FindObjectOfType<PlayerController>();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (_player.EnemyBattleStat != null)
            {
                if (!_player.EnemyBattleStat.IsDie) _healthText.text = string.Concat("Health: ", _player.EnemyBattleStat.Health);
                else _healthText.text = string.Concat("Health: 0");
            }
            else _healthText.text = string.Concat("Health: 0");
        }
        
    }
}