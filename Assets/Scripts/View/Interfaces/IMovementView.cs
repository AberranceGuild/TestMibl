using System;

namespace View.Interfaces {
    public interface IMovementView : IView {
        public event Action OnLeftStarted;
        public event Action OnLeftEnded;
        public event Action OnRightStarted;
        public event Action OnRightEnded;
    }
}