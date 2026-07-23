using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Controller.Aim
{
    public class AimSourceJoyStick : MonoBehaviour, IAimSource
    {
        [SerializeReference] private InputActionReference stickReference;
        [SerializeField] private float deathZone = 0.15f;
        
        private Vector2 _input;
        
        private void OnEnable() => stickReference.action.performed += OnUpdateInput;
        private void OnDisable() => stickReference.action.performed -= OnUpdateInput;
        private void OnUpdateInput(InputAction.CallbackContext context) => _input = context.ReadValue<Vector2>();

        public bool TryGetAimDirection(out Vector2 direction)
        {
            if (_input.magnitude < deathZone) {
                direction = Vector2.zero;
                return false;
            }
            
            direction = _input.normalized;
            return true;
        }
    }
}