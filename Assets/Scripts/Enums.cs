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
    public enum ItemType: byte
    {
        HPPotion=0,
        MPPotion=1,
        MagicBook=2,
        None=3
    }
    public enum SizeType:byte
    {
        Little=0,
        Medium=1,
        Large=2,
        None=3
    }
}