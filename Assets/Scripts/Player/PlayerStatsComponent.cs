using Diplom.Assistants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Diplom.Units.Player
{
    public class PlayerStatsComponent : MonoBehaviour
    {
        private IReadOnlyDictionary<StatsType, Container> _statsDic;

        [SerializeField]
        private PlayerStatsAssistant _stats;
        [SerializeField, Range(100f, 200f), Tooltip("Скорость движения персонажа")]
        private float _moveSpeed = 100f;

        public float GetMoveSpeed => _moveSpeed;
    }
}
