using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using FSM;

namespace HyperCasual.Assets.Src.Scripts.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundDistance = 1f;
        [SerializeField] private PlayerAnimation playerAnimation;
        [SerializeField] private PlayerMovementState playerMovement;
        [SerializeField] private GameObject keyIcon;
        [SerializeField] private float fallingForwardForce = 1f;

        private Rigidbody _rigidbody;
        private StateMachine _stateMachine;

        public PlayerAnimation PlayerAnimation { get => playerAnimation; }
        public Rigidbody Rigidbody { get => _rigidbody; }
        public float FallingForwardForce { get => fallingForwardForce; }

        //movement state
        [Header("Movement")]
        [SerializeField] private float speed = 6.0f;
        [SerializeField] private float rotationSpeed = 120.0f;
        [SerializeField] private GameObject playerModel;
        [SerializeField] private CharacterController _characterController;

        public CharacterController CharacterController { get => _characterController; }
        public float Speed { get => speed; }
        public float RotationSpeed { get => rotationSpeed; }
        public GameObject PlayerModel { get => playerModel; }


        private void Start()
        {
            playerAnimation = GetComponent<PlayerAnimation>();
            _rigidbody = GetComponent<Rigidbody>();

            InitStateMachine();

            //register events
            GameManager.Instance.OnGameStarted += StartGame;
            GameManager.Instance.OnGameOver += GameOver;
            GameManager.Instance.OnWinner += GameOver;
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine(this);
            _stateMachine.AddState("Moving", new PlayerMovementState(this));
            _stateMachine.AddState("Idle", new PlayerIdleState(this));
            _stateMachine.AddState("Start", new PlayerStartState(this));
            _stateMachine.AddState("Crash", new PlayerCrashState(this));
            _stateMachine.AddState("Falling", new PlayerFallingState(this));
            _stateMachine.SetStartState("Idle");
            _stateMachine.Init();

        }

        public void StartGame()
        {
            _stateMachine.RequestStateChange("Start");
        }

        private void FixedUpdate()
        {
            if (_stateMachine.ActiveStateName == "Falling")
                return;

            RaycastHit hitInfo;
            var grounded = Physics.Raycast(transform.position, -transform.up, out hitInfo, groundDistance, groundLayer);
            if (!grounded)
            {
                _stateMachine.RequestStateChange("Falling");
            }
            else
            {
                if (hitInfo.collider.tag == "Dropping_Tile")
                {
                    hitInfo.collider.GetComponent<DroppingTile>().Drop();
                }
            }
        }

        private void Update()
        {
            _stateMachine.OnLogic();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Key")
            {
                CollectKey(other.gameObject);
            }
            else if (other.gameObject.tag == "Door")
            {
                GameManager.Instance.EnteredDoor(other.GetComponentInParent<Door>());
            }
            else if (other.gameObject.tag == "Door_Range")
            {
                GameManager.Instance.EnteredDoorRange(other.GetComponent<Door>());
            }
            else if (other.gameObject.tag == "Gem")
            {
                Destroy(other.gameObject);
                GameManager.Instance.CollectGem();
            }
            else if (other.gameObject.tag == "RollingBlade")
            {
                Crash();
            }

            UpdateKeyIcon();
        }

        private void UpdateKeyIcon()
        {
            if (GameManager.Instance.KeysOwned > 0)
            {
                keyIcon.SetActive(true);
            }
            else
            {
                keyIcon.SetActive(false);
            }
        }

        private void CollectKey(GameObject keyObject)
        {
            Destroy(keyObject);
            GameManager.Instance.CollectKey();
        }

        public void Crash()
        {
            _stateMachine.RequestStateChange("Crash");
        }

        public void GameOver()
        {
            _stateMachine.RequestStateChange("Idle");
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnGameStarted -= StartGame;
        }
    }
}