using Diplom.Managers.Enemy;
using Diplom.Units.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Spawners.Enemy
{
    public class EnemySpawner : BaseSpawner
    {
        private GameObject _enemy;
        private LinkedList<EnemyController> _enemies = new LinkedList<EnemyController>();
        [SerializeField]
        private EnemyManager _enemyManager;
        public GameObject Enemy => _enemy;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        // Update is called once per frame
        void Update()
        {

        }
        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(20f);
                if (_enemyManager.Enemies.Count < 12)
                {
                    for (int i = 0; i < _spawnPoint.Length; i++)
                    {
                        _enemy = Instantiate(_prefab, _spawnPoint[i]);
                        _enemyManager.SetEnemies();
                        
                    }
                    
                }
                
            }
        }
        
    }
}