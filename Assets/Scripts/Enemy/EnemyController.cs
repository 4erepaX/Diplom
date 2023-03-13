using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Enemy
{

    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed=1;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            transform.position += transform.forward * _moveSpeed * Time.fixedDeltaTime;
        }
    }
}