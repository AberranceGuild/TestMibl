using System;
using Infrastructure;
using View.Interfaces;

namespace View.Proxies {
    public class StartGameProxy : Proxy<StartGameView>, IStartGameView {
        public event Action OnStarted {
            add =>
                Subject.OnStarted += value;
            remove =>
                Subject.OnStarted -= value;
        }

        public void Show() =>
            Subject?.Show();

        public void Hide() =>
            Subject?.Hide();
    }
}