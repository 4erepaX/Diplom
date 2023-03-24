using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Buildings
{
    public class BuildingComponent : MonoBehaviour
    {
        [SerializeField]
        private SideType _side;


        public SideType Side => _side;
    }
}