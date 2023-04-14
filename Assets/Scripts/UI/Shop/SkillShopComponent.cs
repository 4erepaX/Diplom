using Diplom.UI.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Diplom.UI.Shop
{
    public class SkillShopComponent : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler
    {
        [SerializeField]
        private SkillType _type;
        [SerializeField]
        private float _cooldown;
        [SerializeField]
        private int _cost;
        [SerializeField]
        private float _manaCost;
        [SerializeField]
        private TargetType _target;
        [SerializeField]
        private DebaffType _debaffType;
        [SerializeField]
        private float _skillTime;
        [SerializeField]
        private bool _isBought;
        private string _description;
        private Sprite _sprite;
        private PlayerSkillBoxComponent _skillBox;
        [SerializeField]
        private GameObject _boughtPanel;

        public SkillType Type=> _type;
        public Sprite Sprite => _sprite;
        public float Cooldown=>_cooldown;
        public int Cost => _cost;
        public float ManaCost=>_manaCost;
        public bool IsBought => _isBought;
        public TargetType Target=>_target;

        public DebaffType DebaffType=>_debaffType;

        public float SkillTime=> _skillTime;

        public string Descripion=>_description;
        // Start is called before the first frame update
        void Start()
        {
            _skillBox = FindObjectOfType<PlayerSkillBoxComponent>();
            _sprite = GetComponent<Image>().sprite;
            _isBought = false;
            _boughtPanel.SetActive(false);
            switch (_target)
            {
                case TargetType.Self:
                    switch (_type)
                    {
                        case SkillType.Baff:
                            _cooldown = 10;
                            _manaCost = 140;
                            _cost = 1000;
                            _description = "Increase attack damage by 20%";
                            _skillTime = 10;
                            break;
                    }
                    break;
                case TargetType.Enemies:
                    AttackSkills();
                    break;
            }

        }

        private void AttackSkills()
        {
            switch (_type)
            {
                case SkillType.Attack:
                    _cooldown = 10;
                    _manaCost = 100;
                    _cost = 1000;
                    _description = "200% attack damage";
                    break;
                case SkillType.Debaff:
                    DebaffSkills();
                    break;
            }
        }
        private void DebaffSkills()
        {
            switch (_debaffType)
            {
                case DebaffType.DecreaseAttack:
                    _cooldown = 15;
                    _manaCost = 150;
                    _cost = 1000;
                    _skillTime = 10;
                    _description = "Reduces attack damage by 20%";
                    break;
                case DebaffType.DecreaseDefense:
                    _cooldown = 15;
                    _manaCost = 200;
                    _cost = 1000;
                    _skillTime = 10;
                    _description = "Reduces defence by 20%";
                    break;
                case DebaffType.Slowdown:
                    _cooldown = 15;
                    _manaCost = 150;
                    _cost = 1000;
                    _skillTime = 10;
                    _description = "Reduces move speed by 20%";
                    break;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_skillBox.AddSkill(this))
            {
                _isBought = true;
                _boughtPanel.SetActive(true);
            }
        }
    }
}