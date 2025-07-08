using System;

namespace View.Interfaces {
    public interface IGameView : IView {
        public event Action OnJumpAction;
        public event Action OnFireAction;
        public event Action OnPaused;
        void UpdateBulletView();
    }
}