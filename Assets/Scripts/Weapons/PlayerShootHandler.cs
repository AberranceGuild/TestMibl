using Infrastructure.Configs;
using UnityEngine;

namespace Weapons {
    public class PlayerShootHandler : MonoBehaviour {
        [SerializeField] private PlayerConfigs _playerConfigs;
        [SerializeField] private PrefabWeapon _prefabWeapon;
        [SerializeField] private RayCastWeapon _rayCastWeapon;

        public void Shoot() {
            if (_playerConfigs.WeaponType == WeaponType.Gun) {
                _prefabWeapon.Shoot();
                return;
            }

            _rayCastWeapon.Shoot();
        }
    }
}