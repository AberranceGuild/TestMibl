using UnityEngine;
using View;
using View.Factories;
using View.Proxies;
using Zenject;

namespace Installers {
    [CreateAssetMenu(fileName = "ViewInstaller", menuName = "Installers/ViewInstaller")]
    public class ViewInstaller : ScriptableObjectInstaller<ViewInstaller> {
        public MainCanvas MainCanvasPrefab;
        public GameView GameViewPrefab;
        public GameOverView GameOverViewPrefab;
        public StartGameView StartGameViewPrefab;
        public LevelCompletedView LevelCompletedViewPrefab;
        public PauseView PauseViewPrefab;
        public MovementView MovementPrefab;

        public override void InstallBindings() {
            Container.BindInstance(MainCanvasPrefab)
                     .AsSingle();

            Container.BindInstance(MovementPrefab)
                     .AsSingle();

            Container.BindInstance(GameViewPrefab)
                     .AsSingle();

            Container.BindInstance(GameOverViewPrefab)
                     .AsSingle();

            Container.BindInstance(StartGameViewPrefab)
                     .AsSingle();

            Container.BindInstance(LevelCompletedViewPrefab)
                     .AsSingle();

            Container.BindInstance(PauseViewPrefab)
                     .AsSingle();

            Container.BindInterfacesAndSelfTo<MainCanvasProxy>()
                     .AsSingle();

            Container.BindInterfacesAndSelfTo<GameViewProxy>()
                     .AsSingle();

            Container.BindInterfacesAndSelfTo<GameOverProxy>()
                     .AsSingle();

            Container.BindInterfacesAndSelfTo<StartGameProxy>()
                     .AsSingle();

            Container.BindInterfacesAndSelfTo<LevelCompletedProxy>()
                     .AsSingle();

            Container.BindInterfacesAndSelfTo<PauseProxy>()
                     .AsSingle();

            Container.BindInterfacesAndSelfTo<MovementViewProxy>()
                     .AsSingle();

            Container.BindInterfacesTo<ViewFactory>()
                     .AsSingle();
        }
    }
}