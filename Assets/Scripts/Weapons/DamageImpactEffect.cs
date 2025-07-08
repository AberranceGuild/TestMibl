using Cysharp.Threading.Tasks;
using Infrastructure;
using UnityEngine;

namespace Weapons {
    public class DamageImpactEffect : MonoBehaviour {
        public void Prepare() =>
            ReturnToPool()
                .Forget();

        private async UniTask ReturnToPool() {
            await UniTask.Delay(3000);
            BulletObjectPool.Instance.ReturnObject(gameObject);
        }
    }
}