using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Diplom.UI.ChooseHero
{
    public class ChooseHero : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private GameObject _heroPrefab;
        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private GameObject _UI;
        [SerializeField]
        private GameObject _chooseUI;
        [SerializeField]
        private GameObject _gameManager;
        public void OnPointerClick(PointerEventData eventData)
        {
            Instantiate(_heroPrefab, _spawnPoint);
            _UI.SetActive(true);
            _gameManager.SetActive(true);
            Destroy(_chooseUI);
        }
    }
}