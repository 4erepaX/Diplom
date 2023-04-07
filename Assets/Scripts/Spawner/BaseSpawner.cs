using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Diplom.Spawners
{
    public abstract class BaseSpawner : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _prefab;
        [SerializeField]
        protected Transform[] _spawnPoint;

    }
}