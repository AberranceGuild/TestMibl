using System;
using Services.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.Interfaces;

namespace View {
    public class GameOverView : ViewBase, IGameOverView {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _replayButton;
        [SerializeField] private Button _quitButton;
        private IGameAspect _gameAspect;
        public event Action OnClosed;
        public event Action OnReplay;
        public event Action OnQuite;

        public override void Show() {
            _levelText.text = $"Level {_gameAspect.GetLevel()}";
            base.Show();
        }

        protected override void AddListeners() {
            _closeButton.onClick.AddListener(InvokeClosed);
            _replayButton.onClick.AddListener(InvokeReply);
            _quitButton.onClick.AddListener(InvokeQuit);
        }

        protected override void RemoveListeners() {
            _closeButton.onClick.RemoveListener(InvokeClosed);
            _replayButton.onClick.RemoveListener(InvokeReply);
            _quitButton.onClick.RemoveListener(InvokeQuit);
        }

        public void SetDependencies(IGameAspect gameAspect) =>
            _gameAspect = gameAspect;

        private void InvokeReply() =>
            OnReplay?.Invoke();

        private void InvokeQuit() =>
            OnQuite?.Invoke();

        private void InvokeClosed() =>
            OnClosed?.Invoke();
    }
}