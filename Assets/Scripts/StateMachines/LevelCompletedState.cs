using Characters.Factories.Interfaces;
using Cysharp.Threading.Tasks;
using Services.Interfaces;
using StateMachines.Base;
using StateMachines.Base.Collection;
using View.Interfaces;

namespace StateMachines {
    public class LevelCompletedState : LevelCompletedStateBase {
        private readonly ILevelCompletedView _levelCompletedView;
        private readonly IQuiteService _quitService;
        private readonly ICharacterFactory _characterFactory;
        private readonly IGameAspect _gameAspect;
        private StateMachineBase _gameStateMachine;

        public LevelCompletedState(ILevelCompletedView levelCompletedView, IQuiteService quitService, ICharacterFactory characterFactory, IGameAspect gameAspect) {
            _levelCompletedView = levelCompletedView;
            _quitService = quitService;
            _characterFactory = characterFactory;
            _gameAspect = gameAspect;
        }

        public override UniTask Enter() {
            _gameAspect.IncreaseLevel();
            _characterFactory.Cleanup();
            _levelCompletedView.Show();
            _levelCompletedView.OnQuit += OnQuit;
            _levelCompletedView.OnNext += OnNext;
            return UniTask.CompletedTask;
        }

        public override UniTask Exit() {
            _levelCompletedView.OnQuit -= OnQuit;
            _levelCompletedView.OnNext -= OnNext;
            _levelCompletedView.Hide();
            return UniTask.CompletedTask;
        }

        public override void SetStateMachine(StateMachineBase stateMachine) =>
            _gameStateMachine = stateMachine;

        private void OnNext() {
            _characterFactory.Create();
            _gameStateMachine.Enter<GameplayStateBase>();
        }

        private void OnQuit() =>
            _quitService.Quite();
    }
}