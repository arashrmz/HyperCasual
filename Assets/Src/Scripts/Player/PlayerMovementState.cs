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