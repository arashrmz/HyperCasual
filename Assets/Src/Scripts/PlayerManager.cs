using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public enum PlayerState
    {
        Idle,
        Moving,
        Start,
        Crash,
        Falling
    }

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance = 1f;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject keyIcon;
    [SerializeField] private float fallingForwardForce = 1f;

    private PlayerState _playerState = PlayerState.Idle;
    private Rigidbody _rigidbody;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
        _rigidbody = GetComponent<Rigidbody>();

        _playerState = PlayerState.Idle;
    }

    public void StartGame()
    {
        _playerState = PlayerState.Start;
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        playerAnimation.OnStart();
        yield return new WaitForSeconds(1f);
        _playerState = PlayerState.Moving;
    }

    private void FixedUpdate()
    {
        if (_playerState == PlayerState.Falling)
            return;
        RaycastHit hitInfo;
        var grounded = Physics.Raycast(transform.position, -transform.up, out hitInfo, groundDistance, groundLayer);
        if (!grounded && _playerState != PlayerState.Falling)
        {
            _playerState = PlayerState.Falling;
            GameManager.Instance.OnFallDown();
            playerAnimation.OnFalling();
            _rigidbody.AddForce(transform.forward * fallingForwardForce, ForceMode.Impulse);
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
        if (_playerState == PlayerState.Idle)
        {
            playerAnimation.OnIdle();
        }
        else if (_playerState == PlayerState.Moving)
        {
            //play move animation
            playerAnimation.OnMoving();
            //move the character
            playerMovement.UpdateLogic();
        }
        else if (_playerState == PlayerState.Falling)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            CollectKey(other.gameObject);
        }
        else if (other.gameObject.tag == "Door")
        {
            GameManager.Instance.OnEnteredDoor(other.GetComponentInParent<Door>());
        }
        else if (other.gameObject.tag == "Door_Range")
        {
            GameManager.Instance.OnEnteredDoorRange(other.GetComponent<Door>());
        }
        else if (other.gameObject.tag == "Gem")
        {
            Destroy(other.gameObject);
            GameManager.Instance.OnGemCollected();
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
        GameManager.Instance.OnKeyCollected();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up * groundDistance);
    }

    public void Crash()
    {
        _playerState = PlayerState.Crash;
        playerAnimation.OnCrash();
        GameManager.Instance.OnFallDown();
    }
}
