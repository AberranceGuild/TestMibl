using System;

namespace Services.Interfaces {
    public interface IGameAspect {
        event Action OnBulletChanged;
        int GetLevel();
        int GetBullet();
        int GetMaxBulletAmount();
        bool HasBullet();
        void DecreaseBullet();
        void IncreaseLevel();
    }
}