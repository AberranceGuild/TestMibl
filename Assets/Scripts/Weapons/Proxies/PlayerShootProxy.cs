using Infrastructure;
using Weapons.Interfaces;

namespace Weapons.Proxies {
    public class PlayerShootProxy : Proxy<PlayerShootHandler>, IPlayerShoot {
        public void Shoot() =>
            Subject?.Shoot();
    }
}