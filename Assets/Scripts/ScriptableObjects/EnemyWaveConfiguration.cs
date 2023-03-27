using Diplom.Units.Enemy;
using OneLine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWaveConfigurations", menuName = "WaveConfigs/Wave Configuration")]
    public class EnemyWaveConfiguration : ScriptableObject
    {    
            [SerializeField, OneLine(Header = LineHeader.Short)]
            private EnemyLevels[] _wave = new EnemyLevels[3];

            public EnemyLevels[] Wave => _wave;
        
    }
}