using UnityEngine;

namespace Character.Controller
{
    public class WeaponAim : MonoBehaviour
    {
        private Transform _transform;
        private IAimSource _aimSource;

        private void Awake()
        {
            _transform = transform;
            _aimSource = GetComponentInChildren<IAimSource>(false);
        }
        private void FixedUpdate()
        {
            if (_aimSource.TryGetAimDirection(out Vector2 direction))
                _transform.right = direction;
        }
    }
}