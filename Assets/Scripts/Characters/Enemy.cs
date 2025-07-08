using System;
using Characters.Interfaces;
using Infrastructure.Configs;
using NaughtyAttributes;
using UnityEngine;

namespace Characters {
    public class Enemy : MonoBehaviour, IEnemy {
        private static readonly int Speed = Animator.StringToHash("Speed");

        private static int PlayerLayer =>
            1 << 8;
        private const float GROUNDED_RADIUS = .2f;
        private readonly Collider2D[] _colliders = new Collider2D[10];
        [SerializeField, Expandable] private EnemyConfigs _enemyConfigs;
        [Range(0, .3f), SerializeField] private float _movementSmoothing = .05f;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private int _currentHealth = 100;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Animator _animator;

        private bool _grounded;
        private bool _facingLeft = true;
        private Vector3 _velocity = Vector3.zero;
        private float _startDirection = -1f;
        public event Action OnDeath = delegate { };

        private void Start() =>
            _currentHealth = _enemyConfigs.MaxHealth;

        private void FixedUpdate() {
            _grounded = false;

            int size = Physics2D.OverlapCircleNonAlloc(_groundCheck.position, GROUNDED_RADIUS, _colliders, _whatIsGround);
            for (int i = 0; i < size; i++)
                if (_colliders[i].gameObject != gameObject)
                    _grounded = true;

            if (_grounded is false)
                _startDirection *= -1f;

            bool playerInRight = Physics2D.Raycast(transform.position, Vector3.right, _enemyConfigs.RayDistance, PlayerLayer);
            bool playerInLeft = Physics2D.Raycast(transform.position, Vector3.left, _enemyConfigs.RayDistance, PlayerLayer);
#if UNITY_EDITOR
            Debug.DrawLine(transform.position, transform.position + Vector3.right * _enemyConfigs.RayDistance, Color.red, 1f);
            Debug.DrawLine(transform.position, transform.position + Vector3.left * _enemyConfigs.RayDistance, Color.blue, 1f);
#endif

            if (playerInRight)
                _startDirection = 1f;

            if (playerInLeft)
                _startDirection = -1f;

            float move = _startDirection;
            Vector3 targetVelocity = new Vector2(move * _enemyConfigs.RunSpeed, _rigidbody2D.velocity.y);

            _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);
            _animator.SetFloat(Speed, Mathf.Abs(_rigidbody2D.velocity.x));

            if (move < 0 && !_facingLeft)
                Flip();

            else if (move > 0 && _facingLeft)
                Flip();
        }

        public void TakeDamage(int damage) {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
                Die();
        }

        private void Flip() {
            _facingLeft = !_facingLeft;

            transform.Rotate(0f, 180f, 0f);
        }

        private void Die() {
            OnDeath();
            Instantiate(_enemyConfigs.DeathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}