using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Units.Player
{
    public enum StatsType : byte
    {
        Health,
        Mana,
        Experience,
        Damage,
        CriticalChance,
        MoveSpeed,
    }
    [Serializable]
    public struct PlayerLevel
    {
        [SerializeField]
        private byte _level;
        [SerializeField]
        private int _strength;
        [SerializeField]
        private int _agility;
        [SerializeField]
        private int _intellegence;
        [SerializeField]
        private int _experience;

        public byte Level => _level;
        public int Strength=>_strength;
        public int Agility=>_agility;
        public int Intellegence=>_intellegence;
        public int Experience => _experience;
    }

}