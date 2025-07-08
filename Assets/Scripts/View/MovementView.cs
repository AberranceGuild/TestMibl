using System;
using UnityEngine;
using UnityEngine.EventSystems;
using View.Interfaces;

namespace View {
    public class MovementView : ViewBase, IMovementView {
        [SerializeField] private PointerClickListener _leftMove;
        [SerializeField] private PointerClickListener _rightMove;

        public event Action OnLeftStarted;
        public event Action OnLeftEnded;
        public event Action OnRightStarted;
        public event Action OnRightEnded;

        protected override void AddListeners() {
            _leftMove.transform.localScale = Vector3.one;
            _rightMove.transform.localScale = Vector3.one;

            _leftMove.OnUp += InvokeLeftEnded;
            _leftMove.OnDown += InvokeLeftStarted;

            _rightMove.OnUp += InvokeRightEnded;
            _rightMove.OnDown += InvokeRightStarted;
        }

        protected override void RemoveListeners() {
            _leftMove.OnUp -= InvokeLeftEnded;
            _leftMove.OnDown -= InvokeLeftStarted;

            _rightMove.OnUp -= InvokeRightEnded;
            _rightMove.OnDown -= InvokeRightStarted;
        }

        private void InvokeLeftEnded(PointerEventData eventData) {
            OnLeftEnded?.Invoke();
            _leftMove.transform.localScale = Vector3.one;
        }

        private void InvokeRightStarted(PointerEventData eventData) {
            OnRightStarted?.Invoke();
            _rightMove.transform.localScale = new(0.95f, 0.95f, 1f);
        }

        private void InvokeLeftStarted(PointerEventData eventData) {
            OnLeftStarted?.Invoke();
            _leftMove.transform.localScale = new(0.95f, 0.95f, 1f);
        }

        private void InvokeRightEnded(PointerEventData eventData) {
            OnRightEnded?.Invoke();
            _rightMove.transform.localScale = Vector3.one;
        }
    }
}