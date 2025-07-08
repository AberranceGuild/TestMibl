using System;
using Services.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.Interfaces;

namespace View {
    public class GameView : ViewBase, IGameView {
        [SerializeField] private TextMeshProUGUI _bullets;
        [SerializeField] private Button _jumpAction;
        [SerializeField] private Button _fireAction;
        [SerializeField] private Button _pauseAction;
        private IMovementView _movementView;
        private IGameAspect _gameAspect;
        public event Action OnJumpAction;
        public event Action OnFireAction;
        public event Action OnPaused;

        public override void Show() {
            UpdateBulletView();
            _movementView.Show();
            base.Show();
        }

        public void UpdateBulletView() =>
            _bullets.text = $"{_gameAspect.GetBullet()}/{_gameAspect.GetMaxBulletAmount()}";

        public override void Hide() {
            _movementView.Hide();
            base.Hide();
        }

        protected override void AddListeners() {
            _jumpAction.onClick.AddListener(InvokeJump);
            _fireAction.onClick.AddListener(InvokeFire);
            _pauseAction.onClick.AddListener(InvokePause);
        }

        protected override void RemoveListeners() {
            _jumpAction.onClick.RemoveListener(InvokeJump);
            _fireAction.onClick.RemoveListener(InvokeFire);
            _pauseAction.onClick.RemoveListener(InvokePause);
        }

        public void SetDependencies(IGameAspect gameAspect, IMovementView movementView) {
            _gameAspect = gameAspect;
            _movementView = movementView;
        }

        private void InvokeFire() =>
            OnFireAction?.Invoke();

        private void InvokePause() =>
            OnPaused?.Invoke();

        private void InvokeJump() =>
            OnJumpAction?.Invoke();
    }
}