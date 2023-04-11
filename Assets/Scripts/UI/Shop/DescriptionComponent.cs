using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Diplom.UI.Shop
{
    public class DescriptionComponent : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _costText;
        [SerializeField]
        private TMP_Text _descriptionText;
        [SerializeField]
        private Image _sprite;
        public void GetDescription(ItemShopComponent itemShop)
        {
           _costText.text=string.Concat("Cost: ", itemShop.Cost);
            _descriptionText.text = string.Concat(itemShop.Description);
            _sprite.sprite = itemShop.Sprite;
        }
    }
}