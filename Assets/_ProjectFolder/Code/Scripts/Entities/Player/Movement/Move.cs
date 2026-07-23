using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Controller
{
    [RequireComponent(typeof(ObjectPhysics))]
    public class Move : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float speed = 10f;
        [SerializeField, Min(0f)] private float startAccelRate = 7f, stopAccelRate = 10f, turnAccelRate = 20f;
        
        private ObjectPhysics _physics;
        private float _accelRate;
        private Vector2 _input;
        
        private void Awake() => _physics = GetComponent<ObjectPhysics>();
        private void FixedUpdate()
        {
            if (_physics.IsFreeze()) return;
            
            Vector2 currentSpeed = _physics.GetLinearVelocity();
            Vector2 targetSpeed = _input * speed;
            Vector2 speedDif = targetSpeed - currentSpeed;
            
            Vector2 force = speedDif * GetAccelerationRate(currentSpeed, targetSpeed);
            _physics.AddForce(force, ForceMode2D.Force);
        }
        
        private void OnMove(InputValue input) => _input = input.Get<Vector2>();
        private float GetAccelerationRate(Vector2 currentSpeed, Vector2 targetSpeed)
        {
            if (_input == Vector2.zero) return stopAccelRate;
            
            bool isTurning = currentSpeed != Vector2.zero && Vector2.Dot(currentSpeed.normalized, targetSpeed.normalized) < 0f;
            return isTurning ? turnAccelRate : startAccelRate;
        }
    }
}