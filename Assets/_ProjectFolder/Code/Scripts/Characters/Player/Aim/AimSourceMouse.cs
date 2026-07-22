using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Controller
{
    public class AimSourceMouse : MonoBehaviour, IAimSource
    {
        [SerializeReference] private Transform origin;
        private Camera _camera;

        private void Awake() => _camera = Camera.main;

        public bool TryGetAimDirection(out Vector2 direction)
        {
            if (origin == null || _camera == null) {
                direction = Vector2.zero;
                return false;
            }
            
            Vector2 screenPoint = Mouse.current.position.ReadValue();
            Vector2 worldPosition = _camera.ScreenToWorldPoint(screenPoint);
            
            direction = (worldPosition - (Vector2)origin.position).normalized;
            return true;
        }
    }
}