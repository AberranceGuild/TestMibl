using System;
using Services.Interfaces;
using UnityEngine;

namespace Services {
    public class PauseService : IPauseService {
        private bool _isPause;
        public bool IsPause {
            get =>
                _isPause;
            protected set {
                if (_isPause == value)
                    return;

                _isPause = value;
                OnPauseChanged?.Invoke(_isPause);
            }
        }
        public event Action<bool> OnPauseChanged;

        public void Unpause() {
            IsPause = false;
            Time.timeScale = 1f;
        }

        public void Pause() {
            IsPause = true;
            Time.timeScale = 0f;
        }
    }
}