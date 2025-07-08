using System;
using Characters.Interfaces;
using Infrastructure.Configs;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Characters {
    [Serializable, RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController2D : MonoBehaviour, IPlayer {
        private const float GROUNDED_RADIUS = .2f;
        private const float CEILING_RADIUS = .2f;
        private const float CROUCH_SPEED = .36f;
        private readonly Collider2D[] _colliders = new Collider2D[10];
        [SerializeField, Expandable] private PlayerConfigs _configs;
        [Range(0, .3f), SerializeField] private float _movementSmoothing = .05f;
        [SerializeField] private bool _airControl;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private Transform _ceilingCheck;
        [SerializeField] private Collider2D _crouchDisableCollider;
        [Header("Events"), Space] public UnityEvent OnLandEvent;
        public BoolEvent OnCrouchEvent;
        private bool _grounded;
        private Rigidbody2D _rigidbody2D;
        private bool _facingRight = true;
        private Vector3 _velocity = Vector3.zero;
        private bool _wasCrouching;
        public float RunSpeed =>
            _configs.RunSpeed;

        public event Action OnDeath = delegate { };

        private void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            if (OnLandEvent == null)
                OnLandEvent = new();

            if (OnCrouchEvent == null)
                OnCrouchEvent = new();
        }

        private void FixedUpdate() {
            bool wasGrounded = _grounded;
            _grounded = false;

            int size = Physics2D.OverlapCircleNonAlloc(_groundCheck.position, GROUNDED_RADIUS, _colliders, _whatIsGround);
            for (int i = 0; i < size; i++)
                if (_colliders[i].gameObject != gameObject) {
                    _grounded = true;
                    if (!wasGrounded)
                        OnLandEvent.Invoke();
                }
        }

        public void Move(float move, bool crouch, bool jump) {
            if (!crouch)
                if (Physics2D.OverlapCircle(_ceilingCheck.position, CEILING_RADIUS, _whatIsGround))
                    crouch = true;

            if (_grounded || _airControl) {
                if (crouch) {
                    if (!_wasCrouching) {
                        _wasCrouching = true;
                        OnCrouchEvent.Invoke(true);
                    }

                    move *= CROUCH_SPEED;

                    if (_crouchDisableCollider != null)
                        _crouchDisableCollider.enabled = false;
                }
                else {
                    if (_crouchDisableCollider != null)
                        _crouchDisableCollider.enabled = true;

                    if (_wasCrouching) {
                        _wasCrouching = false;
                        OnCrouchEvent.Invoke(false);
                    }
                }

                Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody2D.velocity.y);

                _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);

                if (move > 0 && !_facingRight)
                    Flip();

                else if (move < 0 && _facingRight)
                    Flip();
            }

            if (_grounded && jump) {
                _grounded = false;
                _rigidbody2D.AddForce(new(0f, _configs.JumpForce));
            }
        }

        public void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.TryGetComponent(out Enemy _))
                Die();
        }

        public void Die() {
            OnDeath();
            Instantiate(_configs.DeathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void Flip() {
            _facingRight = !_facingRight;

            transform.Rotate(0f, 180f, 0f);
        }

        [Serializable]
        public class BoolEvent : UnityEvent<bool> { }
    }
}