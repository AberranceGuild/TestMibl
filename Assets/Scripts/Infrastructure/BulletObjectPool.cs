using UnityEngine;

namespace Infrastructure {
    public class BulletObjectPool : ObjectPool<BulletObjectPool> {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Init() =>
            Instance.Clear();
    }

    public class DamageImpactEffectObjectPool : ObjectPool<DamageImpactEffectObjectPool> {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Init() =>
            Instance.Clear();
    }
}