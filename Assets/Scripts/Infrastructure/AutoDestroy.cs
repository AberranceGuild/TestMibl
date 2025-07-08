using UnityEngine;

namespace Infrastructure {
    public class AutoDestroy : MonoBehaviour {
        [SerializeField] private float _lifeTime = 1f;

        private void Update() {
            _lifeTime -= Time.deltaTime;
            if (_lifeTime < 0)
                Destroy(gameObject);
        }
    }
}