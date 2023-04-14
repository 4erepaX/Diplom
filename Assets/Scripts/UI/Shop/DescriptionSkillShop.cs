using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Diplom.UI.Shop
{
    public class DescriptionSkillShop : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _costText;
        [SerializeField]
        private TMP_Text _descriptionText;
        [SerializeField]
        private TMP_Text _manaCostText;
        [SerializeField]
        private TMP_Text _cooldownText;
        [SerializeField]
        private Image _sprite;
        // Start is called before the first frame update
        public void GetDescription(SkillShopComponent skillShop)
        {
            _costText.text = string.Concat("Cost: ", skillShop.Cost);
            _manaCostText.text = string.Concat("ManaCost: ", skillShop.ManaCost);
            _cooldownText.text = string.Concat("Cooldown: ", skillShop.Cooldown);
            _descriptionText.text = string.Concat(skillShop.Description);
            _sprite.sprite = skillShop.Sprite;
        }
    }
}