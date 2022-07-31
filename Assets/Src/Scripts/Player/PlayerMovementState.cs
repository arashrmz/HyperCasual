using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

namespace HyperCasual.Assets.Src.Scripts.Player
{
    public class PlayerMovementState : StateBase
    {

        private Vector3 _rotation = Vector3.zero;


        private readonly PlayerManager _playerManager;

        public PlayerMovementState(PlayerManager playerManager) : base(needsExitTime: false)
        {
            _playerManager = playerManager;
        }

        public override void OnEnter()
        {
            //play move animation
            _playerManager.PlayerAnimation.PlayMoveAnimation();
        }

        public override void OnLogic()
        {
            Move();
            HandleRotation();
        }

        public override void OnLateLogic()
        {
            _playerManager.PlayerModel.transform.rotation = Quaternion.Slerp(_playerManager.PlayerModel.transform.rotation, Quaternion.Euler(_rotation), _playerManager.RotationSpeed * Time.deltaTime);
            _playerManager.PlayerModel.transform.position = _playerManager.transform.position;
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void HandleRotation()
        {
            var swipeDirection = InputManager.Instance.SwipeDirection;
            if (swipeDirection == SwipeDirection.None)
                return;

            var angle = 0f;
            if (swipeDirection == SwipeDirection.Up)
            {
                angle = 0f;
            }
            else if (swipeDirection == SwipeDirection.Down)
            {
                angle = 180f;
            }
            else if (swipeDirection == SwipeDirection.Left)
            {
                angle = -90f;
            }
            else if (swipeDirection == SwipeDirection.Right)
            {
                angle = 90f;
            }

            _rotation = new Vector3(0, angle, 0);
            _playerManager.transform.eulerAngles = _rotation;
        }

        private void Move()
        {
            //movement
            var movementDirection = _playerManager.transform.forward.normalized;
            _playerManager.CharacterController.Move(movementDirection * _playerManager.Speed * Time.deltaTime);
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
}