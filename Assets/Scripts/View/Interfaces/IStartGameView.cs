using System;

namespace View.Interfaces {
    public interface IStartGameView : IView {
        public event Action OnStarted;
    }
}