using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Diplom.Units
{
    public class TriggerComponent : MonoBehaviour
    {
        private Collider _collider;

        [SerializeField]
        private int _id = 0;

        public int GetID => _id;
        public bool Enable
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }
        // Start is called before the first frame update
        void Start()
        {
            _collider = GetComponent<Collider>();
            _collider.enabled = false;
            _collider.isTrigger = true;
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}
