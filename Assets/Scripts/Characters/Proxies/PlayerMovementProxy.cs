using System;
using Characters.Interfaces;
using Infrastructure;

namespace Characters.Proxies {
    public class PlayerMovementProxy : Proxy<PlayerMovement>, IPlayerMovement {
        public event Action OnFall {
            add =>
                Subject.OnFall += value;
            remove =>
                Subject.OnFall -= value;
        }

        public void Jump() =>
            Subject?.Jump();

        public void UpdateMove(float horizontalMove) =>
            Subject?.UpdateMove(horizontalMove);
    }
}