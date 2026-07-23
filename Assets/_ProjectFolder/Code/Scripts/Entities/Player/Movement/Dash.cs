using System;
using System.Collections;
using UnityEngine;

namespace Entity.Controller.Movement
{
    public class Dash : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float speed = 50f;
        [SerializeField, Min(0f)] private float duration = 1f, timeRecovery = 1f;
        [SerializeField] private LayerMask excludedLayers;

        public event Action<float> OnTimeoutUpdated;

        private ObjectAnimations _animator;
        private ObjectPhysics _physics;
        private bool _isSprinting;
        
        private void Awake()
        {
            _animator = GetComponent<ObjectAnimations>();
            _physics = GetComponent<ObjectPhysics>();
        }
        private void OnSprint()
        {
            if (_isSprinting) return;
            
            Vector2 direction = _physics.GetLinearVelocity();
            StartCoroutine(DashRoutine(direction));
        }
        
        private IEnumerator DashRoutine(Vector2 direction)
        {
            if (direction == Vector2.zero) yield break;
            
            _isSprinting = true;
            _physics.SetFreeze(true);
            _physics.ExcludeLayers(excludedLayers);
            
            direction = speed * direction.normalized;
            yield return TimeRoutine(duration, (_) => _physics.SetLinearVelocity(direction));
            
            _physics.SetFreeze(false);
            _physics.ResetExcludedLayers();
            yield return TimeRoutine(timeRecovery, OnTimeoutUpdated);
            
            _isSprinting = false;
        }
        private IEnumerator TimeRoutine(float maxTime, Action<float> onUpdate)
        {
            float time = 0f;
            
            while (time < maxTime)
            {
                yield return null;
                time += Time.deltaTime;
                onUpdate?.Invoke(time / maxTime);
            }
        }
    }
}