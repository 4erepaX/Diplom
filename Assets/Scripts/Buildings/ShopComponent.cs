using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Diplom.Buildings.Shop
{
    public class ShopComponent : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private GameObject _shopPanel;
        private PlayerController _player;
        private float ThetaScale = 0.01f;
        private float radius = 10f;
        private int Size;
        private LineRenderer LineDrawer;
        private float Theta = 0f;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (Vector3.Distance(_player.transform.position,transform.position)<10)
            {
                _shopPanel.SetActive(true);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            LineDrawer = GetComponent<LineRenderer>();
                _player = FindObjectOfType<PlayerController>();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(transform.position, _player.transform.position) < 20)
            {
                Theta = 0f;
                Size = (int)((1f / ThetaScale) + 1f);
                LineDrawer.positionCount = Size;
                LineDrawer.startWidth = 0.1f;
                for (int i = 0; i < Size; i++)
                {
                    Theta += (2.0f * Mathf.PI * ThetaScale);
                    float x = radius * Mathf.Cos(Theta);
                    float y = radius * Mathf.Sin(Theta);
                    Vector3 vector = new Vector3(x, 2, y) + transform.position;
                    vector.y = 0.5f;
                    LineDrawer.SetPosition(i, vector);
                }
                if (Vector3.Distance(transform.position, _player.transform.position) > 10)
                {
                    _shopPanel.SetActive(false);
                }
            }
            else
            {
                LineDrawer.positionCount = 0;
            }
        }
    }
}