using System;

namespace View.Interfaces {
    public interface IPauseView : IView {
        public event Action OnClosed;
        public event Action OnContinue;
        public event Action OnQuite;
    }
}