using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSM;


namespace HyperCasual.Assets.Src.Scripts.Player
{
    public class PlayerStartState : StateBase
    {
        private readonly PlayerManager _playerManager;

        public PlayerStartState(PlayerManager playerManager) : base(needsExitTime: false)
        {
            this._playerManager = playerManager;
        }

        public override async void OnEnter()
        {
            _playerManager.PlayerAnimation.PlayStartAnimation();
            await StartGameCoroutine();
        }

        private async Task StartGameCoroutine()
        {
            await Task.Delay(1000);
            fsm.RequestStateChange("Moving");
        }
    }
}