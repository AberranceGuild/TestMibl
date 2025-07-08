using System;
using Infrastructure;
using View.Interfaces;

namespace View.Proxies {
    public class GameViewProxy : Proxy<GameView>, IGameView {
        public event Action OnJumpAction {
            add =>
                Subject.OnJumpAction += value;
            remove =>
                Subject.OnJumpAction -= value;
        }
        public event Action OnFireAction {
            add =>
                Subject.OnFireAction += value;
            remove =>
                Subject.OnFireAction -= value;
        }
        public event Action OnPaused {
            add =>
                Subject.OnPaused += value;
            remove =>
                Subject.OnPaused -= value;
        }

        public void UpdateBulletView() =>
            Subject?.UpdateBulletView();

        public void Show() =>
            Subject?.Show();

        public void Hide() =>
            Subject?.Hide();
    }
}