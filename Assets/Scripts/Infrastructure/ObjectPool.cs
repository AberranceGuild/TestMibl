using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure {
    public class ObjectPool<T> : MonoSingleton<T> where T : Component {
        protected readonly List<GameObject> _pool = new();
        protected GameObject _prefab;

        public void SetPrefab(GameObject prefab) =>
            _prefab = prefab;

        public void Clear() {
            foreach (GameObject o in _pool)
                if (o)
                    Destroy(o);

            _pool.Clear();
        }

        public GameObject GetObject() {
            foreach (GameObject obj in _pool)
                if (!obj.activeInHierarchy) {
                    obj.SetActive(true);
                    obj.transform.SetParent(null);
                    return obj;
                }

            GameObject newObj = Instantiate(_prefab);
            _pool.Add(newObj);
            return newObj;
        }

        public void ReturnObject(GameObject obj) {
            obj.SetActive(false);
            obj.transform.SetParent(transform);
        }
    }
}