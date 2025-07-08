using System;

namespace Characters.Interfaces {
    public interface IPlayer {
        event Action OnDeath;
    }
}