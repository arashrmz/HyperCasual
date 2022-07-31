using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSM;

namespace HyperCasual.Assets.Src.Scripts.Player
{
    public class PlayerCrashState : StateBase
    {
        private readonly PlayerManager _playerManager;

        public PlayerCrashState(PlayerManager playerManager) : base(needsExitTime: false)
        {
            this._playerManager = playerManager;
        }

        public override void OnEnter()
        {
            _playerManager.PlayerAnimation.PlayCrashAnimation();
            GameManager.Instance.FallDown();
        }
    }
}