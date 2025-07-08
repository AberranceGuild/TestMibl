using System.Collections.Generic;
using Characters.Factories.Interfaces;
using Characters.Proxies;
using Cinemachine;
using UnityEngine;
using Weapons;
using Weapons.Proxies;

namespace Characters.Factories {
    public class CharacterFactory : ICharacterFactory {
        private readonly Enemy _enemyPrefab;
        private readonly CinemachineVirtualCamera _followCameraPrefab;
        private readonly PlayerController2D _playerControllerPrefab;
        private readonly PlayerMovementProxy _playerMovementProxy;
        private readonly PlayerShootProxy _playerShootProxy;
        private readonly EnemyProxy _enemyProxy;
        private readonly PlayerProxy _playerProxy;
        private readonly List<GameObject> _objects = new();

        public CharacterFactory(Enemy enemyPrefab, CinemachineVirtualCamera followCameraPrefab, PlayerController2D playerControllerPrefab, PlayerMovementProxy playerMovementProxy,
                                PlayerShootProxy playerShootProxy, EnemyProxy enemyProxy, PlayerProxy playerProxy) {
            _enemyPrefab = enemyPrefab;
            _followCameraPrefab = followCameraPrefab;
            _playerControllerPrefab = playerControllerPrefab;
            _playerMovementProxy = playerMovementProxy;
            _playerShootProxy = playerShootProxy;
            _enemyProxy = enemyProxy;
            _playerProxy = playerProxy;
        }

        public void Create() {
            Enemy enemy = Object.Instantiate(_enemyPrefab);
            _enemyProxy.SetSubject(enemy);

            PlayerController2D player = Object.Instantiate(_playerControllerPrefab);
            _playerProxy.SetSubject(player);
            _playerMovementProxy.SetSubject(player.GetComponent<PlayerMovement>());
            _playerShootProxy.SetSubject(player.GetComponent<PlayerShootHandler>());

            CinemachineVirtualCamera cvc = Object.Instantiate(_followCameraPrefab);
            cvc.Follow = player.transform;

            _objects.Add(enemy.gameObject);
            _objects.Add(player.gameObject);
            _objects.Add(cvc.gameObject);
        }

        public void Cleanup() {
            foreach (GameObject obj in _objects) {
                if (obj == null)
                    continue;

                Object.Destroy(obj);
            }

            _objects.Clear();
        }
    }
}