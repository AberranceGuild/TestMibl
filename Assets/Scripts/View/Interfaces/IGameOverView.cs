using System;

namespace View.Interfaces {
    public interface IGameOverView : IView {
        public event Action OnClosed;
        public event Action OnReplay;
        public event Action OnQuite;
    }
}