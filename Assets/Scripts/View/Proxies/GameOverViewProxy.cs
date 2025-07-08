using System;
using Infrastructure;
using View.Interfaces;

namespace View.Proxies {
    public class GameOverProxy : Proxy<GameOverView>, IGameOverView {
        public event Action OnClosed {
            add =>
                Subject.OnClosed += value;
            remove =>
                Subject.OnClosed -= value;
        }
        public event Action OnReplay {
            add =>
                Subject.OnReplay += value;
            remove =>
                Subject.OnReplay -= value;
        }
        public event Action OnQuite {
            add =>
                Subject.OnQuite += value;
            remove =>
                Subject.OnQuite -= value;
        }

        public void Show() =>
            Subject?.Show();

        public void Hide() =>
            Subject?.Hide();
    }
}