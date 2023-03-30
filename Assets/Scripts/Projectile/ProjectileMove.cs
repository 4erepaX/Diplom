using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Diplom.Projectile
{
    public class ProjectileMove : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed=2;
        private float _time=10;
        // Start is called before the first frame update
        void Start()
        {
            transform.parent = null;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            transform.rotation =  Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y-15f, 0f));
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }

        // Update is called once per frame
        void Update()
        {
            _time -= Time.fixedDeltaTime;
            transform.position += transform.forward * _moveSpeed * Time.fixedDeltaTime;
            if (_time <= 0) Destroy(gameObject);
        }
    }
}