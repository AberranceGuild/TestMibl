using System;
using Characters.Interfaces;
using UnityEngine;

namespace Characters {
    public class PlayerMovement : MonoBehaviour, IPlayerMovement {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int IsCrouching = Animator.StringToHash("IsCrouching");
        private const float FALL_DEPTH = -10f;
        [SerializeField] private PlayerController2D _controller2D;
        [SerializeField] private Animator _animator;
        private float _horizontalMove;
        private bool _jump;
        private bool _crouch;

        public event Action OnFall = delegate { };

        private void FixedUpdate() {
            _controller2D.Move(_horizontalMove * Time.fixedDeltaTime, _crouch, _jump);
            _jump = false;
            if (transform.position.y < FALL_DEPTH)
                OnFall();
        }

        public void UpdateMove(float horizontalMove) {
            _horizontalMove = horizontalMove * _controller2D.RunSpeed;
            _animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));
        }

        public void Jump() {
            _jump = true;
            _animator.SetBool(IsJumping, true);
        }

        public void OnCrouching(bool isCrouching) =>
            _animator.SetBool(IsCrouching, isCrouching);

        public void OnLanding() =>
            _animator.SetBool(IsJumping, false);
    }
}