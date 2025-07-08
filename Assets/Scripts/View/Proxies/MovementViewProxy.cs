using System;
using Infrastructure;
using View.Interfaces;

namespace View.Proxies {
    public class MovementViewProxy : Proxy<MovementView>, IMovementView {
        public event Action OnLeftStarted {
            add =>
                Subject.OnLeftStarted += value;
            remove =>
                Subject.OnLeftStarted -= value;
        }
        public event Action OnLeftEnded {
            add =>
                Subject.OnLeftEnded += value;
            remove =>
                Subject.OnLeftEnded -= value;
        }
        public event Action OnRightStarted {
            add =>
                Subject.OnRightStarted += value;
            remove =>
                Subject.OnRightStarted -= value;
        }
        public event Action OnRightEnded {
            add =>
                Subject.OnRightEnded += value;
            remove =>
                Subject.OnRightEnded -= value;
        }

        public void Show() =>
            Subject?.Show();

        public void Hide() =>
            Subject?.Hide();
    }
}