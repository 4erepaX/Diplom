using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Diplom.Buildings.Shop
{
    public class ShopComponent : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private GameObject _shopPanel;
        private PlayerController _player;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (Vector3.Distance(_player.transform.position,transform.position)<10)
            {
                _shopPanel.SetActive(true);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
      
                _player = FindObjectOfType<PlayerController>();
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}