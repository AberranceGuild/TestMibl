using Characters;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Infrastructure.Configs;
using NaughtyAttributes;
using UnityEngine;

namespace Weapons {
    public class Bullet : MonoBehaviour {
        [SerializeField, Expandable] private BulletConfigs _configs;
        [SerializeField] private Rigidbody2D _rigidbody;

        public void Prepare() {
            _rigidbody.velocity = transform.right * _configs.Speed;
            ReturnToPool()
                .Forget();
        }

        private void OnTriggerEnter2D(Collider2D hitInfo) {
            if (hitInfo.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(_configs.Damage);

            GameObject effect = DamageImpactEffectObjectPool.Instance.GetObject();
            effect.transform.position = transform.position;
            effect.transform.rotation = transform.rotation;
            effect.GetComponent<DamageImpactEffect>()
                  .Prepare();

            BulletObjectPool.Instance.ReturnObject(gameObject);
        }

        private async UniTask ReturnToPool() {
            await UniTask.Delay(3000);
            _rigidbody.velocity = Vector3.zero;
            BulletObjectPool.Instance.ReturnObject(gameObject);
        }
    }
}