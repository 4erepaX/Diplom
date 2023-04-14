using Diplom.UI.Shop;
using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.UI.Player
{
    public class PlayerSkillBoxComponent : MonoBehaviour
    {
        [SerializeField]
        private PlayerSkillComponent[] _skills= new PlayerSkillComponent[3];
        private PlayerBattleComponent _battleStats;
        // Start is called before the first frame update
        void Start()
        {
            _battleStats = FindObjectOfType<PlayerBattleComponent>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public bool AddSkill(SkillShopComponent skillShop)
        {
           if (_battleStats.BuySkill(skillShop))
            {
                if (!skillShop.IsBought)
                {
                    for (int i = 0; i < _skills.Length; i++)
                    {
                        if (skillShop.Type != SkillType.None)
                        {
                            if (_skills[i].Type == SkillType.None)
                            {
                                _skills[i].AddSkill(skillShop);
                                return true;
                            }
                        }

                    }
                }
            }
            return false;
        }
    }
}