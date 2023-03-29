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
        // Start is called before the first frame update
        void Start()
        {
            _player = FindObjectOfType<PlayerController>().gameObject;
            StartCoroutine(RespawnHero());
            StartCoroutine(SpawnEnemies());
        }

        // Update is called once per frame
        void Update()
        {
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