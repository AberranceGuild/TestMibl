using System.Collections;
using Characters;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons {
    public class RayCastWeapon : MonoBehaviour {
        [FormerlySerializedAs("firePoint"), SerializeField]
        private Transform _firePoint;
        [FormerlySerializedAs("damage"), SerializeField] private int _damage = 40;
        [FormerlySerializedAs("impactEffect"), SerializeField]
        private GameObject _impactEffect;
        [FormerlySerializedAs("lineRenderer"), SerializeField]
        private LineRenderer _lineRenderer;

        public void Shoot() =>
            StartCoroutine(ShootCoroutine());

        private IEnumerator ShootCoroutine() {
            RaycastHit2D hitInfo = Physics2D.Raycast(_firePoint.position, _firePoint.right);

            if (hitInfo) {
                Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                if (enemy != null)
                    enemy.TakeDamage(_damage);

                Instantiate(_impactEffect, hitInfo.point, Quaternion.identity);

                _lineRenderer.SetPosition(0, _firePoint.position);
                _lineRenderer.SetPosition(1, hitInfo.point);
            }
            else {
                _lineRenderer.SetPosition(0, _firePoint.position);
                _lineRenderer.SetPosition(1, _firePoint.position + _firePoint.right * 100);
            }

            _lineRenderer.enabled = true;

            yield return null;

            _lineRenderer.enabled = false;
        }
    }
}