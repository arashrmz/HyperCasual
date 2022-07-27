using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 _rotation = Vector3.zero;

    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float rotationSpeed = 120.0f;

    public CharacterController CharacterController { get => _characterController; }

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    public void UpdateLogic()
    {
        Move();
        HandleRotation();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_rotation), rotationSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        //detect swipe here
        var left = Input.GetKeyDown(KeyCode.LeftArrow);
        var right = Input.GetKeyDown(KeyCode.RightArrow);
        var up = Input.GetKeyDown(KeyCode.UpArrow);
        var down = Input.GetKeyDown(KeyCode.DownArrow);

        if (!left && !right && !up && !down)
        {
            return;
        }

        var angle = 0f;


        if (left)
        {
            angle = -90f;
        }
        else if (right)
        {
            angle = 90f;
        }
        else if (up)
        {
            angle = 0f;
        }
        else if (down)
        {
            angle = 180f;
        }

        _rotation = new Vector3(0, angle, 0);
    }

    private void Move()
    {
        //movement
        var movementDirection = transform.forward.normalized;
        _characterController.Move(movementDirection * speed * Time.deltaTime);
    }

    public void Reverse()
    {
        if (_rotation.y == -90f)
        {
            _rotation.y = 90f;
        }
        else if (_rotation.y == 90f)
        {
            _rotation.y = -90f;
        }
        else if (_rotation.y == 0f)
        {
            _rotation.y = 180f;
        }
        else if (_rotation.y == 180f)
        {
            _rotation.y = 0f;
        }
    }
}
