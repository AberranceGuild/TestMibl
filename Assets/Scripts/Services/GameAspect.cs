using System;
using Infrastructure;
using Infrastructure.Configs;
using Infrastructure.Data;
using Services.Interfaces;

namespace Services {
    public class GameAspect : IGameAspect {
        private readonly PlayerConfigs _playerConfigs;

        private GameData Game =>
            Locator<Saves>.Instance.GameData;

        private BulletData Bullet =>
            Locator<Saves>.Instance.BulletData;

        public event Action OnBulletChanged = delegate { };

        public GameAspect(PlayerConfigs playerConfigs) =>
            _playerConfigs = playerConfigs;

        public void IncreaseLevel() =>
            Locator<Saves>.Instance.GameData.Level++;

        public int GetLevel() =>
            Game.Level;

        public int GetBullet() =>
            Bullet.Amount;

        public int GetMaxBulletAmount() =>
            _playerConfigs.MaxBulletAmount;

        public bool HasBullet() =>
            GetBullet() > 0;

        public void DecreaseBullet() {
            if (Bullet.Amount <= 0)
                return;

            Bullet.Amount--;

            OnBulletChanged();
        }
    }
}