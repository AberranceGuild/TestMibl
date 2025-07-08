using System;
using UnityEngine;
using UnityEngine.UI;
using View.Interfaces;

namespace View {
    public class LevelCompletedView : ViewBase, ILevelCompletedView {
        [SerializeField] private Button _next;
        [SerializeField] private Button _quit;
        public event Action OnNext;
        public event Action OnQuit;

        protected override void AddListeners() {
            _next.onClick.AddListener(InvokeNext);
            _quit.onClick.AddListener(InvokeQuit);
        }

        protected override void RemoveListeners() {
            _next.onClick.RemoveListener(InvokeNext);
            _quit.onClick.RemoveListener(InvokeQuit);
        }

        private void InvokeQuit() =>
            OnQuit?.Invoke();

        private void InvokeNext() =>
            OnNext?.Invoke();
    }
}