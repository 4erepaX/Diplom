using Diplom.UI.Shop;
using Diplom.Units.Enemy;
using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Diplom.UI.Player
{
    public class PlayerSkillComponent : MonoBehaviour, IPointerClickHandler, IDragHandler,IPointerUpHandler
    {
        [SerializeField]
        private SkillType _type;
        [SerializeField]
        private float _cooldown;
        [SerializeField]
        private float _cost;
        [SerializeField]
        private TargetType _target;
        [SerializeField]
        private DebaffType _debaffType;
        [SerializeField]
        private float _skillTime;
        [SerializeField]
        private TMP_Text _cooldownText;
        [SerializeField]
        private GameObject _blackout;
        
        [SerializeField]
        private float ThetaScale = 0.01f;
        [SerializeField]
        private float radius = 3f;
        private int Size;
        private LineRenderer LineDrawer;
        private float Theta = 0f;
        private PlayerBattleComponent _playerBattleStats;
        private PlayerStatsComponent _playerStats;
        private List<EnemyBattleComponent> _enemyBattleStats;
        private bool _isActivated = false;
        private Image _image;
        private string _description;

        public SkillType Type => _type;
        public float Cost => _cost;
        public DebaffType DebaffType=> _debaffType;
        public float SkillTime=> _skillTime;
        public void OnDrag(PointerEventData eventData)
        {
            if (!_isActivated)
            {
                if (_playerBattleStats.Mana >= _cost)
                {
                    switch (_target)
                    {
                        case TargetType.Enemies:
                            DrawSkillArea();
                            break;
                    }
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isActivated)
            {
                if (_playerBattleStats.Mana >= _cost)
                {
                    switch (_target)
                    {
                        case TargetType.Self:
                            _playerStats.IncreaseParameters(this);
                            StartCoroutine(OnCooldown());
                            _isActivated = true;
                            break;
                        case TargetType.Enemies:
                            DrawSkillArea();
                            break;
                    }
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isActivated)
            {
                if (_playerBattleStats.ManaCost(this))
                {
                    switch (_target)
                    {
                        case TargetType.Enemies:

                            LineDrawer.positionCount = 0;
                            var position = Mouse.current.position.ReadValue();
                            var ray = Camera.main.ScreenPointToRay(position);
                            if (Physics.Raycast(ray, out var hit))
                            {
                                _enemyBattleStats = FindObjectsOfType<EnemyBattleComponent>().Where(t => Vector3.Distance(t.transform.position, hit.point) < 3).ToList();
                            }

                            foreach (var enemyStat in _enemyBattleStats)
                            {
                                enemyStat.ReceiveSkill(this);
                            }
                            StartCoroutine(OnCooldown());
                            _isActivated = true;
                            break;
                    }
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _image = GetComponent<Image>();
            LineDrawer = GetComponent<LineRenderer>();
            _playerBattleStats = FindObjectOfType<PlayerBattleComponent>();
            _playerStats = FindObjectOfType<PlayerStatsComponent>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void DrawSkillArea()
        {
            var position = Mouse.current.position.ReadValue();
            var ray = Camera.main.ScreenPointToRay(position);
            if (Physics.Raycast(ray, out var hit))
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
                    Vector3 vector = new Vector3(x, 2, y) + hit.point;
                    vector.y = 1;
                    LineDrawer.SetPosition(i, vector);
                }
            }
        }
        private IEnumerator OnCooldown()
        {
            var time = _cooldown;
            while (time > 0)
            {
                _blackout.SetActive(true);               
                time -= Time.deltaTime;
                
                _cooldownText.text = string.Concat(Mathf.RoundToInt(time));
                if (time <= 0)
                {
                    _blackout.SetActive(false);
                    _isActivated = false;
                }
                    yield return null;
            }
              
        }
        public void AddSkill(SkillShopComponent skillShop)
        {
            _type = skillShop.Type;
            _cooldown = skillShop.Cooldown;
            _image.sprite = skillShop.Sprite;
            _cost = skillShop.ManaCost;
            _target = skillShop.Target;
            _skillTime = skillShop.SkillTime;
            _debaffType = skillShop.DebaffType;
            _description = skillShop.Description;

        }
    }
}