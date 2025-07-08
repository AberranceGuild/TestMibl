using UnityEngine;
using Weapons;

namespace Infrastructure.Configs {
    [CreateAssetMenu(menuName = "Configs/Player", fileName = "Player Config")]
    public class PlayerConfigs : ScriptableObject {
        [field: SerializeField]
        public int MaxBulletAmount { get; private set; } = 50;
        [field: SerializeField]
        public float RunSpeed { get; private set; } = 40f;
        [field: SerializeField]
        public float JumpForce { get; private set; } = 400f;
        [field: SerializeField]
        public GameObject DeathEffect { get; private set; }
        [field: SerializeField]
        public WeaponType WeaponType { get; private set; }
    }
}