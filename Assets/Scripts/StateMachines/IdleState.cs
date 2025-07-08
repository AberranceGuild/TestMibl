using Characters.Factories.Interfaces;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Services.Interfaces;
using StateMachines.Base;
using StateMachines.Base.Collection;
using View.Factories.Interfaces;
using View.Interfaces;
using Weapons;

namespace StateMachines {
    public class IdleState : IdleStateBase {
        private readonly IViewFactory _viewFactory;
        private readonly IStartGameView _startGameView;
        private readonly ICharacterFactory _characterFactory;
        private readonly ISaveLoadService _saveLoadService;
        private readonly Bullet _bullet;
        private readonly DamageImpactEffect _damageImpactEffect;
        private readonly BulletObjectPool _bulletObjectPool;
        private readonly DamageImpactEffectObjectPool _damageImpactEffectObjectPool;
        private StateMachineBase _stateMachine;
        private bool _firstTime;

        public IdleState(IViewFactory viewFactory, IStartGameView startGameView, ICharacterFactory characterFactory, ISaveLoadService saveLoadService, BulletObjectPool bulletObjectPool, Bullet bullet,
                         DamageImpactEffectObjectPool damageImpactEffectObjectPool, DamageImpactEffect damageImpactEffect) {
            _viewFactory = viewFactory;
            _startGameView = startGameView;
            _characterFactory = characterFactory;
            _saveLoadService = saveLoadService;
            _bulletObjectPool = bulletObjectPool;
            _bullet = bullet;
            _damageImpactEffectObjectPool = damageImpactEffectObjectPool;
            _damageImpactEffect = damageImpactEffect;
        }

        public override async UniTask Enter() {
            if (!_firstTime) {
                await _saveLoadService.Load();
                _viewFactory.Create();
                _bulletObjectPool.SetPrefab(_bullet.gameObject);
                _damageImpactEffectObjectPool.SetPrefab(_damageImpactEffect.gameObject);
                _firstTime = true;
            }

            _startGameView.Show();
            _characterFactory.Cleanup();
            _startGameView.OnStarted += OnStarted;
        }

        public override UniTask Exit() {
            _startGameView.Hide();
            _startGameView.OnStarted -= OnStarted;
            return UniTask.CompletedTask;
        }

        public override void SetStateMachine(StateMachineBase stateMachine) =>
            _stateMachine = stateMachine;

        private void OnStarted() {
            _characterFactory.Create();
            _stateMachine.Enter<GameplayStateBase>();
        }
    }
}