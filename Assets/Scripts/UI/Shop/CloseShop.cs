using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.UI.Shop
{
    public class CloseShop : MonoBehaviour
    {
        [SerializeField]
        private GameObject _shopPanel;
        public void Close()
        {
            _shopPanel.SetActive(false);
        }
    }
}