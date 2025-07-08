using Services.Interfaces;
using UnityEngine;
using View.Factories.Interfaces;
using View.Proxies;

namespace View.Factories {
    public class ViewFactory : IViewFactory {
        private readonly MainCanvas _mainCanvasPrefab;
        private readonly GameView _gameViewPrefab;
        private readonly GameOverView _gameOverPrefab;
        private readonly PauseView _pausePrefab;
        private readonly StartGameView _startGamePrefab;
        private readonly LevelCompletedView _levelCompletedPrefab;
        private readonly MovementView _movementPrefab;
        private readonly MainCanvasProxy _mainCanvasProxy;
        private readonly GameViewProxy _gameViewProxy;
        private readonly GameOverProxy _gameOverProxy;
        private readonly PauseProxy _pauseProxy;
        private readonly StartGameProxy _startGameProxy;
        private readonly LevelCompletedProxy _levelCompletedProxy;
        private readonly MovementViewProxy _movementProxy;
        private readonly IGameAspect _gameAspect;

        public ViewFactory(MainCanvas mainCanvasPrefab, GameView gameViewPrefab, GameOverView gameOverPrefab, PauseView pausePrefab, StartGameView startGamePrefab,
                           LevelCompletedView levelCompletedPrefab, MainCanvasProxy mainCanvasProxy, GameViewProxy gameViewProxy, GameOverProxy gameOverProxy, PauseProxy pauseProxy,
                           StartGameProxy startGameProxy, LevelCompletedProxy levelCompletedProxy, IGameAspect gameAspect, MovementViewProxy movementProxy, MovementView movementPrefab) {
            _mainCanvasPrefab = mainCanvasPrefab;
            _gameViewPrefab = gameViewPrefab;
            _gameOverPrefab = gameOverPrefab;
            _pausePrefab = pausePrefab;
            _startGamePrefab = startGamePrefab;
            _levelCompletedPrefab = levelCompletedPrefab;
            _mainCanvasProxy = mainCanvasProxy;
            _gameViewProxy = gameViewProxy;
            _gameOverProxy = gameOverProxy;
            _pauseProxy = pauseProxy;
            _startGameProxy = startGameProxy;
            _levelCompletedProxy = levelCompletedProxy;
            _gameAspect = gameAspect;
            _movementProxy = movementProxy;
            _movementPrefab = movementPrefab;
        }

        public void Create() {
            MainCanvas mainCanvas = Object.Instantiate(_mainCanvasPrefab);
            _mainCanvasProxy.SetSubject(mainCanvas);

            StartGameView startGame = Object.Instantiate(_startGamePrefab, mainCanvas.transform);
            startGame.SetDependencies(_gameAspect);
            startGame.Hide();
            _startGameProxy.SetSubject(startGame);

            GameView gameView = Object.Instantiate(_gameViewPrefab, mainCanvas.transform);
            MovementView movement = Object.Instantiate(_movementPrefab, gameView.transform);
            gameView.SetDependencies(_gameAspect, movement);
            _gameViewProxy.SetSubject(gameView);
            _movementProxy.SetSubject(movement);
            gameView.Hide();

            PauseView pause = Object.Instantiate(_pausePrefab, mainCanvas.transform);
            pause.SetDependencies(_gameAspect);
            pause.Hide();
            _pauseProxy.SetSubject(pause);

            LevelCompletedView levelCompleted = Object.Instantiate(_levelCompletedPrefab, mainCanvas.transform);
            levelCompleted.Hide();
            _levelCompletedProxy.SetSubject(levelCompleted);

            GameOverView gameOver = Object.Instantiate(_gameOverPrefab, mainCanvas.transform);
            gameOver.SetDependencies(_gameAspect);
            gameOver.Hide();
            _gameOverProxy.SetSubject(gameOver);
        }
    }
}