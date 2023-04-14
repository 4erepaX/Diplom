using Diplom.UI.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Diplom.UI.Shop
{
    public class ItemShopComponent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private ItemType _type;
        [SerializeField]
        private SizeType _size;
        [SerializeField]
        private GameObject _descriptionGO;
        [SerializeField]
        private DescriptionComponent _descriptionComponent;
        private int _cost;
        private Sprite _sprite;
        private string _description;
        private PlayerInventoryComponent _inventory;

        public ItemType Type => _type;
        public SizeType Size => _size;
        public Sprite Sprite => _sprite;
        public int Cost => _cost;
        public string Description=>_description;
        public void OnPointerClick(PointerEventData eventData)
        {
            _inventory.AddItem(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_type == ItemType.None) return;
            _descriptionGO.transform.position = transform.position+new Vector3(50f,150f,0f);
            _descriptionComponent.GetDescription(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_type == ItemType.None) return;
            _descriptionGO.transform.position =new Vector3(-150f,-640f,0f);
        }

        // Start is called before the first frame update
        void Start()
        {
            _sprite = GetComponent<Image>().sprite;
            _descriptionGO = FindObjectOfType<DescriptionComponent>().gameObject;
            _descriptionComponent = FindObjectOfType<DescriptionComponent>();
            _inventory = FindObjectOfType<PlayerInventoryComponent>();
            switch (_type)
            {
                case ItemType.None:
                    gameObject.SetActive(false);
                    break;
                case ItemType.HPPotion:
                    switch (_size)
                    {
                        case SizeType.Little:
                            _cost = 50;
                            _description = "Restore 100 HP";
                            break;
                        case SizeType.Medium:
                            _cost = 100;
                            _description = "Restore 500 HP";
                            break;
                        case SizeType.Large:
                            _cost = 200;
                            _description = "Restore 1000 HP";
                            break;
                    }
                    break;
                case ItemType.MPPotion:
                    switch (_size)
                    {
                        case SizeType.Little:
                            _cost = 50;
                            _description = "Restore 50 MP";
                            break;
                        case SizeType.Medium:
                            _cost = 100;
                            _description = "Restore 300 MP";
                            break;
                        case SizeType.Large:
                            _cost = 200;
                            _description = "Restore 750 MP";
                            break;
                    }
                    break;
                case ItemType.MagicBook:
                    switch (_size)
                    {
                        case SizeType.Little:
                            _cost = 200;
                            _description = "Add +5 Strength, Agility, Intellegence";
                            break;
                        case SizeType.Medium:
                            _cost = 400;
                            _description = "Add +25 Strength, Agility, Intellegence"; ;
                            break;
                        case SizeType.Large:
                            _cost = 800;
                            _description = "Add +50 Strength, Agility, Intellegence";
                            break;
                    }
                    break;
            }  

                

        }

    }
}