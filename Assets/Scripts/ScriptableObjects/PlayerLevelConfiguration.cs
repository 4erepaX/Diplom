using Diplom.Units.Player;
using OneLine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.ScriptableObjects
{
    [CreateAssetMenu(fileName ="NewLevelConfigurations", menuName = "LevelConfigs/Level Configuration")]
    public class PlayerLevelConfiguration : ScriptableObject
    {
        [SerializeField, OneLine(Header =LineHeader.Short)]
        private PlayerLevel[] _levels=new PlayerLevel[10];
    }
}