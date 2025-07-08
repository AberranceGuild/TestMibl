using Characters;
using Characters.Factories;
using Characters.Proxies;
using Cinemachine;
using Infrastructure;
using UnityEngine;
using Weapons;
using Weapons.Proxies;
using Zenject;

namespace Installers {
    [CreateAssetMenu(fileName = "CharacterInstaller", menuName = "Installers/CharacterInstaller")]
    public class CharacterInstaller : ScriptableObjectInstaller<ViewInstaller> {
        public CinemachineVirtualCamera CameraPrefab;
        public PlayerController2D PlayerPrefab;
        public Enemy EnemyPrefab;
        public Bullet BulletPrefab;
        public DamageImpactEffect DamageImpactPrefab;

        public override void InstallBindings() {
            Container.BindInstance(CameraPrefab)
                     .AsSingle();

            Container.BindInstance(BulletPrefab)
                     .AsSingle();

            Container.BindInstance(DamageImpactPrefab)
                     .AsSingle();

            Container.BindInstance(PlayerPrefab)
                     .AsSingle();

            Container.BindInstance(EnemyPrefab)
                     .AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerMovementProxy>()
                     .AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerShootProxy>()
                     .AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyProxy>()
                     .AsSingle();

            Container.BindInstance(BulletObjectPool.Instance)
                     .AsSingle();

            Container.BindInstance(DamageImpactEffectObjectPool.Instance)
                     .AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerProxy>()
                     .AsSingle();

            Container.BindInterfacesTo<CharacterFactory>()
                     .AsSingle();
        }
    }
}