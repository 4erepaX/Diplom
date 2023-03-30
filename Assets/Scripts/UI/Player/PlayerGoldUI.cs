using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Diplom.UI.Player
{
    public class PlayerGoldUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _goldText;
        [SerializeField]
        private PlayerBattleComponent _battleStats;
        // Start is called before the first frame update
        void Start()
        {
            _battleStats = FindObjectOfType<PlayerBattleComponent>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_battleStats!=null)
            {
                _goldText.text = _battleStats.Gold.ToString();
            }
        }
    }
}