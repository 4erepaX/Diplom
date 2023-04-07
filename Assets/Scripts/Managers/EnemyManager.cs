using Diplom.Spawners.Enemy;
using Diplom.Units.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Managers.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        private LinkedList<GameObject> _enemies = new LinkedList<GameObject>();
        [SerializeField]
        private EnemySpawner _enemySpawner;
        public LinkedList<GameObject> Enemies => _enemies;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            Debug.Log(_enemies.Count);
        }
        public void SetEnemies()
        {
            _enemies.AddLast(_enemySpawner.Enemy);
        }
            
        public void KillEnemy(GameObject enemy)
        {
           _enemies.Remove(enemy);
        }

    }
}