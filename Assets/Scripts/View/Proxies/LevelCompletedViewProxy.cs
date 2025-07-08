using System;
using Infrastructure;
using View.Interfaces;

namespace View.Proxies {
    public class LevelCompletedProxy : Proxy<LevelCompletedView>, ILevelCompletedView {
        public event Action OnNext {
            add =>
                Subject.OnNext += value;
            remove =>
                Subject.OnNext -= value;
        }
        public event Action OnQuit {
            add =>
                Subject.OnQuit += value;
            remove =>
                Subject.OnQuit -= value;
        }

        public void Show() =>
            Subject?.Show();

        public void Hide() =>
            Subject?.Hide();
    }
}