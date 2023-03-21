using Diplom.Units.Player;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom
{
    [Serializable]
    public class StatsDictionary : SerializableDictionaryBase<StatsType, Container> { }
}