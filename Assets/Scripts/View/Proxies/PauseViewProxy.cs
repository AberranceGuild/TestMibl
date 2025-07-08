using System;
using Infrastructure;
using View.Interfaces;

namespace View.Proxies {
    public class PauseProxy : Proxy<PauseView>, IPauseView {
        public event Action OnClosed {
            add =>
                Subject.OnClosed += value;
            remove =>
                Subject.OnClosed -= value;
        }
        public event Action OnContinue {
            add =>
                Subject.OnContinue += value;
            remove =>
                Subject.OnContinue -= value;
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