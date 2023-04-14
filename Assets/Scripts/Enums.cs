using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom
{
    public enum PlayerType : byte
    {
        Warrior = 0,
        Wizzard = 1
    }
    public enum SideType : byte
    {
        Friendly = 0,
        Enemy = 1,
        Neutral = 2
    }
    public enum ItemType : byte
    {
        HPPotion = 0,
        MPPotion = 1,
        MagicBook = 2,
        None = 3
    }
    public enum SizeType : byte
    {
        Little = 0,
        Medium = 1,
        Large = 2,
        None = 3
    }
    public enum SkillType : byte
    {
        Debaff = 0,
        Attack = 1,
        Baff = 2,
        None = 3
    }
    public enum TargetType: byte
    {
        Enemies=0,
        Self=1,
        None=2
    }
    public enum DebaffType : byte
    {
        None=0,
        DecreaseAttack=1,
        DecreaseDefense=2,
        Slowdown=3
    }
}