using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Diplom.UI.ChooseHero
{
    public class ChooseHero : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private ChooseHero _hero;

        private bool _isChoosen;
        private Outline _outline;
        
        public bool IsChoosen => _isChoosen;
        private void Start()
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
            _isChoosen = false;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            
            if (!_hero._isChoosen)
            {
                if (!_isChoosen)
                {
                    _outline.enabled = true;
                    _isChoosen = true;
                }
                else
                {
                    _outline.enabled = false;
                    _isChoosen = false;
                }
            }
        }

    }
}