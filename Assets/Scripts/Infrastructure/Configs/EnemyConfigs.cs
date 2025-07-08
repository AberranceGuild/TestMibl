using UnityEngine;

namespace Infrastructure.Configs {
    [CreateAssetMenu(menuName = "Configs/Enemy", fileName = "Enemy Config")]
    public class EnemyConfigs : ScriptableObject {
        [field: SerializeField]
        public int MaxHealth { get; private set; } = 100;
        [field: SerializeField]
        public float RunSpeed { get; private set; } = 5f;
        [field: SerializeField]
        public float RayDistance { get; private set; } = 10f;
        [field: SerializeField]
        public GameObject DeathEffect { get; private set; }
    }
}