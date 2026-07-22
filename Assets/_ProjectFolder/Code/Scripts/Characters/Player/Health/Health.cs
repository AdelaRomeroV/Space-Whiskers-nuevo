using System;
using UnityEngine;

namespace Character.Controller.Life
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField, Min(1)] private int maxHealth = 1;
        [SerializeField, Min(0f)] private float damageKnockback = 1f;
        
        [SerializeField] private Invulnerability invulnerability;
        
        public event Action<float> OnValueChanged;
        
        private ObjectAnimations _animator;
        private ObjectPhysics _physics;
        private int _currentHealth;

        private void Awake()
        {
            _animator = GetComponent<ObjectAnimations>();
            _physics = GetComponent<ObjectPhysics>();
            _currentHealth = maxHealth;
        }
        private void Start() => UpdateHealth();
        private void UpdateHealth() => OnValueChanged?.Invoke(_currentHealth / (float)maxHealth);
        
        public void Damage(int amount, Vector2 direction)
        {
            if (invulnerability.IsInvulnerable) return;
            
            _physics.AddForce(damageKnockback * direction, ForceMode2D.Impulse);
            _currentHealth -= amount;
            
            UpdateHealth();
            StartCoroutine(invulnerability.InvulnerabilityEffect());
        }
    }
}