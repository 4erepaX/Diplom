using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Units.Player
{
    public class CameraComponent : MonoBehaviour
    {
        private PlayerController _target;
        // Start is called before the first frame update
        void Start()
        {
            _target = transform.parent.GetComponent<PlayerController>();
            transform.parent = null;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            transform.position = _target.transform.position;
        }
       
    }
}