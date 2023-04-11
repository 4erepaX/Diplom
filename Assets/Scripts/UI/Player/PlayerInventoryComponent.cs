using Diplom.UI.Shop;
using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.UI.Player
{

    
    public class PlayerInventoryComponent : MonoBehaviour
    {
        [SerializeField]
        private PlayerItemComponent[] _items= new PlayerItemComponent[5];
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
        public bool AddItem(ItemShopComponent itemShop)
        {
            if (_battleStats.BuyItem(itemShop))
            {
                for (int i = 0; i < _items.Length; i++)
                {
                    if (_items[i].Type == ItemType.None || (_items[i].Type == itemShop.Type && _items[i].Size == itemShop.Size))
                    {
                        if (_items[i].Type == itemShop.Type && _items[i].Size == itemShop.Size)
                        {
                            _items[i].IncreaseCount();
                        }
                        if (_items[i].Type == ItemType.None)
                        {
                            _items[i].AddItem(itemShop);
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }
}