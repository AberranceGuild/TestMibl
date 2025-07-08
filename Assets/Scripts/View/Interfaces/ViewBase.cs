using UnityEngine;

namespace View.Interfaces {
    public abstract class ViewBase : MonoBehaviour, IView {
        public virtual void Show() {
            gameObject.SetActive(true);
            AddListeners();
        }

        public virtual void Hide() {
            RemoveListeners();
            gameObject.SetActive(false);
        }

        protected virtual void AddListeners() { }

        protected virtual void RemoveListeners() { }
    }
}