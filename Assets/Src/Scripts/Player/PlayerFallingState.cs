using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSM;
using UnityEngine;

namespace HyperCasual.Assets.Src.Scripts.Player
{
    public class PlayerFallingState : StateBase
    {
        private readonly PlayerManager _playerManager;

        public PlayerFallingState(PlayerManager playerManager) : base(needsExitTime: false)
        {
            this._playerManager = playerManager;
        }

        public override void OnEnter()
        {
            GameManager.Instance.FallDown();
            _playerManager.PlayerAnimation.PlayFallAnimation();
            //the initial force towards player's direction
            _playerManager.Rigidbody.AddForce(_playerManager.transform.forward * _playerManager.FallingForwardForce, ForceMode.Impulse);
        }

        public override void OnLogic()
        {
            _playerManager.transform.Translate(Vector3.down * Time.deltaTime * 5f);
        }

        public override void OnLateLogic()
        {
            _playerManager.PlayerModel.transform.position = _playerManager.transform.position;
        }
    }
}