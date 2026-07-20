using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Controller
{
    public class WeaponAim : MonoBehaviour
    {
        [SerializeField] private Transform point;
        [SerializeField] private float distance;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }
    }
}