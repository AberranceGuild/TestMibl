using Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons {
    public class PrefabWeapon : MonoBehaviour {
        [FormerlySerializedAs("firePoint"), SerializeField]
        private Transform _firePoint;
        [FormerlySerializedAs("bulletPrefab"), SerializeField]
        private GameObject _bulletPrefab;

        public void Shoot() {
            GameObject bullet = BulletObjectPool.Instance.GetObject();
            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation;
            bullet.GetComponent<Bullet>()
                  .Prepare();
        }
    }
}