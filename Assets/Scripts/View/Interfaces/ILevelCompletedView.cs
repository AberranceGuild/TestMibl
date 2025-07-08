using System;

namespace View.Interfaces {
    public interface ILevelCompletedView : IView {
        public event Action OnNext;
        public event Action OnQuit;
    }
}