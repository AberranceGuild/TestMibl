using UnityEngine;

namespace Infrastructure {
    public abstract class MonoSingleton<TInstance> : MonoBehaviour where TInstance : Component {
        private static TInstance _instance;

        public static TInstance Instance {
            get {
                if (_instance is not null)
                    return _instance;

                _instance = FindFirstObjectByType<TInstance>(FindObjectsInactive.Include) ?? new GameObject(typeof(TInstance).Name).AddComponent<TInstance>();
                return _instance;
            }
        }
        private bool _canDestroyInstance;

        protected virtual void Awake() {
            if (_instance != null && _canDestroyInstance) {
                Destroy(gameObject);
                return;
            }

            _instance = this as TInstance;

            _canDestroyInstance = true;

            DontDestroyOnLoad(gameObject);
        }
    }
}