using Diplom.Spawners.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Diplom.UI.ChooseHero
{
    public class ChoosePlayer : MonoBehaviour

    {
        [SerializeField]
        private GameObject _panel;
        [SerializeField]
        private GameObject _level;
        [SerializeField]
        private GameObject _warriorPrefab;
        [SerializeField]
        private GameObject _wizzardPrefab;
        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private GameObject _UI;
        [SerializeField]
        private ChooseHero _warrior;
        [SerializeField]
        private ChooseHero _wizzard;
        [SerializeField]
        private PlayerSpawner _spawner;

        private Color _colorPanel; 
        private bool _isWarrior;
        private bool _isWizzard;
        
        public GameObject WarriorPrefab=>_warriorPrefab;
        public GameObject WizzardPrefab => _wizzardPrefab;
        private void Start()
        {
            _colorPanel = _panel.GetComponent<Image>().color;
        }
        public void StartGame()
        {
            _isWarrior = _warrior.IsChoosen;
            _isWizzard = _wizzard.IsChoosen;
            
            if (_isWarrior) StartCoroutine(Choose(_warriorPrefab, _spawnPoint));
            if (_isWizzard) StartCoroutine(Choose(_wizzardPrefab, _spawnPoint));
            

        }

        private IEnumerator Choose(GameObject HeroPrefab, Transform SpawnPoint)
        {
            while (_colorPanel.a >= 0)
            {
                _colorPanel.a -= Time.deltaTime;
                _panel.GetComponent<Image>().color = _colorPanel;
                if (_colorPanel.a <= 0f)
                {
                    Destroy(_panel);
                    Instantiate(HeroPrefab, SpawnPoint);
                   
                    _UI.SetActive(true);
                    _level.SetActive(true);
                    _spawner.GetHero(HeroPrefab);
                }
                    yield return null;
            }
            
        }
    }
}