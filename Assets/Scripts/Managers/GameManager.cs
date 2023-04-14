using Diplom.Buildings;
using Diplom.UI.ChooseHero;
using Diplom.UI.Player;
using Diplom.Units.Enemy;
using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Diplom.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _panel;
        private Color _colorPanel;
        [SerializeField]
        private TMP_Text _resultText; 

        [SerializeField]
        private byte _wave;
        [SerializeField]
        private float _time;

        public byte Wave => _wave;
        // Start is called before the first frame update
        private void Awake()
        {
            _wave = 1;
            _time = 60;
        }
        void Start()
        {
            _colorPanel = _panel.GetComponent<Image>().color;
          
            
        }

        // Update is called once per frame
        void Update()
        {
        }
        public IEnumerator WaveChange()
        {
            
                while (_wave < 3)
                {
                    _time -= Time.deltaTime;
                    if (_time <= 0)
                    {
                        _wave++;
                        _time = 60;
                    }

                    yield return null;
                }
               
            
        }
        public void CheckWinConditions(BuildingComponent _building)
        {
           if (_building.Side==SideType.Enemy)
            {
                _panel.SetActive(true);
                    StartCoroutine(Blackout());
                    _resultText.text = "Win!";
                
            }
        }
        public void CheckLoseConditions(BuildingComponent _building)
        {
            {
                if (_building.Side == SideType.Friendly)
                {

                    _panel.SetActive(true);
                    StartCoroutine(Blackout());
                    _resultText.text = "Lose!";

                }
            }
        }
        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
        private IEnumerator Blackout()
        {
            while (_colorPanel.a <= 1)
            {
                _colorPanel.a += Time.deltaTime;
                _panel.GetComponent<Image>().color = _colorPanel;
                yield return null;
            }

        }
    }
}