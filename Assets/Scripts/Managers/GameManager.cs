using Diplom.UI.Player;
using Diplom.Units.Enemy;
using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Diplom.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _respawnPoint;
        [SerializeField]
        private GameObject _player;
        [SerializeField]
        private GameObject _UI;
        [SerializeField]
        private GameObject _enemyPrefab;
        [SerializeField]
        private Transform[] _spawnEnemyPoint;
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
            _player = FindObjectOfType<PlayerController>().gameObject;
            StartCoroutine(RespawnHero());
            StartCoroutine(SpawnEnemies());
            StartCoroutine(WaveChange());
            
        }

        // Update is called once per frame
        void Update()
        {
        }
        private IEnumerator WaveChange()
        {
            while (_wave<3)
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
        private IEnumerator RespawnHero()
        {
            while (true)
            {
                if (FindObjectOfType<PlayerController>() != null) yield return null;
                yield return new WaitForSeconds(5f);
                if (FindObjectOfType<PlayerController>() == null)
                {
                    _player.transform.position = _respawnPoint.position;
                    _player.SetActive(true);
                    FindObjectOfType<PlayerBattleComponent>().OnRespawn();
                }
            }
        }
        private IEnumerator SpawnEnemies()
        {
            while(true)
            {
                yield return new WaitForSeconds(20f);
                if (FindObjectsOfType<EnemyController>().Length < 12)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Instantiate(_enemyPrefab, _spawnEnemyPoint[i]);
                    }
                }
            }
        }
    }
}