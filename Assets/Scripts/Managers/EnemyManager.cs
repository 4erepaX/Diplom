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