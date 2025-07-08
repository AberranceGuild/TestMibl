using System;

namespace Characters.Interfaces {
    public interface IEnemy {
        event Action OnDeath;
    }
}