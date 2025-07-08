using System;

namespace Services.Interfaces {
    public interface IPauseService {
        bool IsPause { get; }

        event Action<bool> OnPauseChanged;

        void Unpause();

        void Pause();
    }
}