using Infrastructure;
using Infrastructure.Configs;
using Services;
using StateMachines;
using StateMachines.Base;
using StateMachines.Base.Collection;
using UnityEngine;
using Zenject;

namespace Installers {
    [CreateAssetMenu(fileName = "RootInstaller", menuName = "Installers/RootInstaller")]
    public class RootInstaller : ScriptableObjectInstaller<RootInstaller> {
        public PlayerConfigs PlayerConfigs;

        public override void InstallBindings() {
            Container.BindInstance(PlayerConfigs)
                     .AsSingle();

            Container.BindInterfacesTo<PauseService>()
                     .AsSingle();

            Container.BindInterfacesTo<SaveLoadService>()
                     .AsSingle();

            Container.BindInterfacesTo<QuitService>()
                     .AsSingle();

            Container.BindInterfacesTo<GameAspect>()
                     .AsSingle();

            Container.Bind<IdleStateBase>()
                     .To<IdleState>()
                     .AsSingle();

            Container.Bind<GameplayStateBase>()
                     .To<GameplayState>()
                     .AsSingle();

            Container.Bind<GameOverStateBase>()
                     .To<GameOverState>()
                     .AsSingle();

            Container.Bind<LevelCompletedStateBase>()
                     .To<LevelCompletedState>()
                     .AsSingle();

            Container.Bind<PauseStateBase>()
                     .To<PauseState>()
                     .AsSingle();

            Container.Bind<StateMachineBase>()
                     .FromMethod(GetGameStateMachine)
                     .AsSingle();

            Container.BindInterfacesAndSelfTo<EntryPoint>()
                     .FromComponentsInHierarchy()
                     .AsCached();
        }

        private StateMachineBase GetGameStateMachine(InjectContext context) {
            IdleStateBase idleStateBase = context.Container.Resolve<IdleStateBase>();
            GameplayStateBase gameplayStateBase = context.Container.Resolve<GameplayStateBase>();
            PauseStateBase pauseStateBase = context.Container.Resolve<PauseStateBase>();
            GameOverStateBase gameOverStateBase = context.Container.Resolve<GameOverStateBase>();
            LevelCompletedStateBase levelCompletedStateBase = context.Container.Resolve<LevelCompletedStateBase>();
            return new GameStateMachine(new() {
                [typeof(IdleStateBase)] = idleStateBase,
                [typeof(GameplayStateBase)] = gameplayStateBase,
                [typeof(PauseStateBase)] = pauseStateBase,
                [typeof(GameOverStateBase)] = gameOverStateBase,
                [typeof(LevelCompletedStateBase)] = levelCompletedStateBase
            });
        }
    }
}