using UnityEngine;

namespace Infrastructure.Configs {
    [CreateAssetMenu(menuName = "Configs/Bullet", fileName = "Bullet Config")]
    public class BulletConfigs : ScriptableObject {
        [field: SerializeField]
        public float Speed { get; private set; } = 40f;
        [field: SerializeField]
        public int Damage { get; private set; } = 40;
    }
}