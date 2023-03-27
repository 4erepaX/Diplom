using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Units.Enemy
{
    [Serializable]
    public struct EnemyLevels
    {
        [SerializeField]
        private byte _wave;
        [SerializeField]
        private int _strength;
        [SerializeField]
        private int _agility;
        [SerializeField]
        private int _intellegence;
        [SerializeField]
        private int _gold;
        [SerializeField]
        private int _experience;


        public byte Wave => _wave;
        public int Strength => _strength;
        public int Agility => _agility;
        public int Intellegence => _intellegence;           
        public int Gold => _gold;
        public int Experience => _experience;
    }
}