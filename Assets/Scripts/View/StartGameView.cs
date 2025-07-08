using System;
using Services.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.Interfaces;

namespace View {
    public class StartGameView : ViewBase, IStartGameView {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _startButton;
        private IGameAspect _gameAspect;
        public event Action OnStarted;

        public override void Show() {
            _levelText.text = $"Level {_gameAspect.GetLevel()}";
            base.Show();
        }

        protected override void AddListeners() =>
            _startButton.onClick.AddListener(InvokeStarted);

        protected override void RemoveListeners() =>
            _startButton.onClick.RemoveListener(InvokeStarted);

        public void SetDependencies(IGameAspect gameAspect) =>
            _gameAspect = gameAspect;

        private void InvokeStarted() =>
            OnStarted?.Invoke();
    }
}