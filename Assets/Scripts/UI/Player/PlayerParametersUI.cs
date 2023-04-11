using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Diplom.UI.Player
{
    public class PlayerParametersUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _strengthText;
        [SerializeField]
        private TMP_Text _agilityText;
        [SerializeField]
        private TMP_Text _intellegenceText;
        [SerializeField]
        private PlayerStatsComponent _stats;
        // Start is called before the first frame update
        void Start()
        {
            _stats = FindObjectOfType<PlayerStatsComponent>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_stats != null)
            {
                _strengthText.text = string.Concat(_stats.Strength+_stats.AddStrength);
                _agilityText.text = string.Concat(_stats.Agility + _stats.AddAgility);
                _intellegenceText.text = string.Concat(_stats.Intellegence + _stats.AddIntellegence);
            }
        }
    }
}