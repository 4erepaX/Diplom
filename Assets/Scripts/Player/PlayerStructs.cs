using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Units.Player
{
    [Serializable]
    public class Container
    {
        private LinkedList<StatusData> _statuses = new LinkedList<StatusData>();
        [SerializeField]
        private float defaultValue;
        public Range Range;
        public float GetValue { get; private set; }
        public void AddStatus(StatusData data )
        {
        }
        public void RemoveStatusByID(ulong id)
        {

        }
    }
    public struct Range
    {
        public bool IsInit;
        public float Min;
        public float Max;

        public Range(float min, float max)
        {
            Min = min; Max = max; IsInit = true;
        }
        public override string ToString()
        {
            return string.Concat("Min: ", Min, " Max: ", Max);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Range)) return false;
            var range = (Range)obj;

            return range.Max == Max && range.Min == Min;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public struct StatusData
    {
        
    }

    public enum StatsType : byte
    {
        Strength,
        Intellegence,
        Agility,
        Health,
        Mana,
        Experience,
        HPRegInSec,
        MPRegInSec,
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

    }

}