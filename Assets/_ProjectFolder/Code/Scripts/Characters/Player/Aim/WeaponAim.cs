using UnityEngine;

namespace Character.Controller.Aim
{
    public class WeaponAim : MonoBehaviour
    {
        private Transform _transform;
        private IAimSource _aimSource;

        private void Awake()
        {
            _transform = transform;
            _aimSource = GetComponent<IAimSource>();
        }
        private void FixedUpdate()
        {
            if (_aimSource.TryGetAimDirection(out Vector2 direction))
                _transform.right = direction;
        }
    }
}