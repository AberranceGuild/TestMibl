using System;

namespace Characters.Interfaces {
    public interface IPlayerMovement {
        event Action OnFall;
        void Jump();
        void UpdateMove(float horizontalMove);
    }
}