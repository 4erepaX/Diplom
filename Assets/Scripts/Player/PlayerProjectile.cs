using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Units.Player
{
    public class PlayerProjectile : MonoBehaviour
    {
        [SerializeField]
        private Transform _firePoint;
        [SerializeField]
        private GameObject _projectile;

        public Transform FirePoint => _firePoint;
        public GameObject Projectile => _projectile;
    }
}