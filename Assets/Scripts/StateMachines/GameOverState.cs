using Characters.Factories.Interfaces;
using Cysharp.Threading.Tasks;
using Services.Interfaces;
using StateMachines.Base;
using StateMachines.Base.Collection;
using View.Interfaces;

namespace StateMachines {
    public class GameOverState : GameOverStateBase {
        private readonly IGameOverView _gameOverView;
        private readonly IQuiteService _quitService;
        private readonly ICharacterFactory _characterFactory;
        private StateMachineBase _gameStateMachine;

        public GameOverState(IGameOverView gameOverView, IQuiteService quitService, ICharacterFactory characterFactory) {
            _gameOverView = gameOverView;
            _quitService = quitService;
            _characterFactory = characterFactory;
        }

        public override UniTask Enter() {
            _characterFactory.Cleanup();
            _gameOverView.Show();
            _gameOverView.OnQuite += OnQuite;
            _gameOverView.OnClosed += OnClose;
            _gameOverView.OnReplay += OnReplay;
            return UniTask.CompletedTask;
        }

        public override UniTask Exit() {
            _gameOverView.Hide();
            _gameOverView.OnQuite -= OnQuite;
            _gameOverView.OnClosed -= OnClose;
            _gameOverView.OnReplay -= OnReplay;
            return UniTask.CompletedTask;
        }

        public override void SetStateMachine(StateMachineBase stateMachine) =>
            _gameStateMachine = stateMachine;

        private void OnReplay() {
            _characterFactory.Create();
            _gameStateMachine.Enter<GameplayStateBase>();
        }

        private void OnClose() =>
            _gameStateMachine.Enter<IdleStateBase>()
                             .Forget();

        private void OnQuite() =>
            _quitService.Quite();
    }
}