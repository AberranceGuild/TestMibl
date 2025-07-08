using Cysharp.Threading.Tasks;
using Services.Interfaces;
using StateMachines.Base;
using StateMachines.Base.Collection;
using View.Interfaces;

namespace StateMachines {
    public class PauseState : PauseStateBase {
        private readonly IPauseService _pauseService;
        private readonly IPauseView _pauseView;
        private readonly IQuiteService _quiteService;
        private StateMachineBase _gameStateMachine;

        public PauseState(IPauseView pauseView, IQuiteService quiteService, IPauseService pauseService) {
            _pauseView = pauseView;
            _quiteService = quiteService;
            _pauseService = pauseService;
        }

        public override UniTask Enter() {
            SwitchPause();
            _pauseView.Show();
            _pauseView.OnClosed += OnClosed;
            _pauseView.OnQuite += OnQuite;
            _pauseView.OnContinue += OnClosed;
            return UniTask.CompletedTask;
        }

        public override UniTask Exit() {
            SwitchPause();
            _pauseView.Hide();
            _pauseView.OnClosed -= OnClosed;
            _pauseView.OnQuite -= OnQuite;
            _pauseView.OnContinue -= OnClosed;
            return UniTask.CompletedTask;
        }

        public override void SetStateMachine(StateMachineBase stateMachine) =>
            _gameStateMachine = stateMachine;

        private void OnQuite() =>
            _quiteService.Quite();

        private void OnClosed() =>
            _gameStateMachine.Enter<GameplayStateBase>();

        private void SwitchPause() {
            if (_pauseService.IsPause) {
                _pauseService.Unpause();
                return;
            }

            _pauseService.Pause();
        }
    }
}