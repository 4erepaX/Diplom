using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Diplom
{
    public class PlayerController : BasePlayer
    {
        private PlayerControl _control;
        
       
        private void Awake()
        {
            _control = new PlayerControl();
            _control.Player.Enable();
        }
        // Start is called before the first frame update
        void Start()
        {
            _control.Player.Movement.performed += _ => Movement();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}