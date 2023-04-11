using Diplom.UI.Shop;
using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Diplom.UI.Player
{
    public class PlayerItemComponent : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private ItemType _type;
        [SerializeField]
        private SizeType _size;
        private int _strength;
        private int _agility;
        private int _intellegence;
        private int _restoreSize;
        [SerializeField]
        private int _count;
        [SerializeField]
        private TMP_Text _countText;
        private PlayerBattleComponent _battleStats;
        private PlayerStatsComponent _stats;
        private Image _image;


        public ItemType Type => _type;
        public SizeType Size => _size;

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (_type)
            {

                case ItemType.HPPotion:
                    switch (_size)
                    {
                        case SizeType.Little:
                            _restoreSize = 100;
                            if (_battleStats.RestoreHP(_restoreSize))
                            _count -= 1;
                            _countText.text = string.Concat(_count);
                            if (_count <= 0) EndOfItem();
                            break;
                        case SizeType.Medium:
                            _restoreSize = 500;
                            if (_battleStats.RestoreHP(_restoreSize))
                            _count -= 1;
                            if (_count <= 0) EndOfItem();
                            break;
                        case SizeType.Large:
                            _restoreSize = 1000;
                            if (_battleStats.RestoreHP(_restoreSize))
                            _count -= 1;
                            if (_count <= 0) EndOfItem();
                            break;
                    }                    
                    break;
                case ItemType.MPPotion:
                    switch (_size)
                    {
                        case SizeType.Little:
                            _restoreSize = 50;
                            if (_battleStats.RestoreMP(_restoreSize))
                            _count -= 1;
                            if (_count <= 0) EndOfItem();
                            break;
                        case SizeType.Medium:
                            _restoreSize = 300;
                           if  (_battleStats.RestoreMP(_restoreSize))
                            _count -= 1;
                            if (_count <= 0) EndOfItem();
                            break;
                        case SizeType.Large:
                            _restoreSize = 750;
                            if (_battleStats.RestoreMP(_restoreSize)) _count -= 1;
                            if (_count <= 0) EndOfItem();
                            break;
                    }
                    break;
                case ItemType.MagicBook:
                    switch (_size)
                    {
                        case SizeType.Little:
                            _strength = 5;
                            _agility = 5;
                            _intellegence = 5;
                            _stats.SetAddStats(_strength,_agility,_intellegence);
                            _battleStats.Parameters();
                            _count -= 1;
                            if (_count <= 0) EndOfItem();
                            break;
                        case SizeType.Medium:
                            _strength = 25;
                            _agility = 25;
                            _intellegence = 25;
                            _stats.SetAddStats(_strength, _agility, _intellegence);
                            _battleStats.Parameters();
                            _count -= 1;
                            if (_count <= 0) EndOfItem();
                            break;
                        case SizeType.Large:
                            _strength = 50;
                            _agility = 50;
                            _intellegence = 50;
                            _stats.SetAddStats(_strength, _agility, _intellegence);
                            _battleStats.Parameters();
                            _count -= 1;
                            if (_count <= 0) EndOfItem();
                            break;
                    }
                    break;

            }
            _countText.text = string.Concat(_count);
        }

        // Start is called before the first frame update
        void Start()
        {
            _countText.text = string.Concat(_count);
            _battleStats = FindObjectOfType<PlayerBattleComponent>();
            _stats = FindObjectOfType<PlayerStatsComponent>();
            _image = GetComponent<Image>();
            _type = ItemType.None;
            _size = SizeType.None;
            _countText.enabled = false;
            _image.sprite = null;
            _countText.color = Color.red;
        }
        public void IncreaseCount()
        {
            _count += 1;
            _countText.text = string.Concat(_count);
        }
        public void AddItem(ItemShopComponent itemShop)
        {
            _type = itemShop.Type;
            _size = itemShop.Size;
            _image.sprite = itemShop.Sprite;
            _countText.enabled = true;
            _count = 1;
            _countText.text = string.Concat(_count);
           
        }
        private void EndOfItem()
        {
            _countText.enabled = false;
            _type = ItemType.None;
            _size = SizeType.None;
            _image.sprite = null;
        }
    }
}