using Characters.Interfaces;
using Cysharp.Threading.Tasks;
using Services.Interfaces;
using StateMachines.Base;
using StateMachines.Base.Collection;
using View.Interfaces;
using Weapons.Interfaces;

namespace StateMachines {
    public class GameplayState : GameplayStateBase {
        private readonly IGameView _gameView;
        private readonly IMovementView _movementView;
        private readonly IPlayerMovement _playerMovement;
        private readonly IPlayerShoot _playerShoot;
        private readonly IGameAspect _gameAspect;
        private readonly IEnemy _enemy;
        private readonly IPlayer _player;
        private StateMachineBase _stateMachine;

        public GameplayState(IMovementView movementView, IGameView gameView, IPlayerMovement playerMovement, IGameAspect gameAspect, IPlayerShoot playerShoot, IEnemy enemy, IPlayer player) {
            _movementView = movementView;
            _gameView = gameView;
            _playerMovement = playerMovement;
            _gameAspect = gameAspect;
            _playerShoot = playerShoot;
            _enemy = enemy;
            _player = player;
        }

        public override UniTask Enter() {
            _gameView.Show();
            _gameView.OnPaused += OnPaused;
            _gameView.OnFireAction += OnFireAction;
            _gameView.OnJumpAction += OnJumpAction;
            _movementView.OnLeftStarted += OnLeftStarted;
            _movementView.OnRightStarted += OnRightStarted;
            _movementView.OnLeftEnded += OnLeftEnded;
            _movementView.OnRightEnded += OnRightEnded;
            _gameAspect.OnBulletChanged += _gameView.UpdateBulletView;
            _playerMovement.OnFall += OnFall;
            _enemy.OnDeath += OnEnemyDeath;
            _player.OnDeath += OnPlayerDeath;
            return UniTask.CompletedTask;
        }

        public override UniTask Exit() {
            _gameView.OnPaused -= OnPaused;
            _gameView.OnFireAction -= OnFireAction;
            _gameView.OnJumpAction -= OnJumpAction;
            _movementView.OnLeftStarted -= OnLeftStarted;
            _movementView.OnRightStarted -= OnRightStarted;
            _movementView.OnLeftEnded -= OnLeftEnded;
            _movementView.OnRightEnded -= OnRightEnded;
            _gameAspect.OnBulletChanged -= _gameView.UpdateBulletView;
            _playerMovement.OnFall -= OnFall;
            _enemy.OnDeath -= OnEnemyDeath;
            _player.OnDeath -= OnPlayerDeath;
            _gameView.Hide();
            return UniTask.CompletedTask;
        }

        public override void SetStateMachine(StateMachineBase stateMachine) =>
            _stateMachine = stateMachine;

        private void OnPlayerDeath() =>
            _stateMachine.Enter<GameOverStateBase>();

        private void OnEnemyDeath() =>
            _stateMachine.Enter<LevelCompletedStateBase>();

        private void OnFall() =>
            _stateMachine.Enter<GameOverStateBase>();

        private void OnPaused() =>
            _stateMachine.Enter<PauseStateBase>();

        private void OnRightEnded() =>
            _playerMovement.UpdateMove(0);

        private void OnLeftEnded() =>
            _playerMovement.UpdateMove(0);

        private void OnRightStarted() =>
            _playerMovement.UpdateMove(1f);

        private void OnLeftStarted() =>
            _playerMovement.UpdateMove(-1f);

        private void OnJumpAction() =>
            _playerMovement.Jump();

        private void OnFireAction() {
            if (_gameAspect.HasBullet()) {
                _gameAspect.DecreaseBullet();
                _playerShoot.Shoot();
            }
        }
    }
}