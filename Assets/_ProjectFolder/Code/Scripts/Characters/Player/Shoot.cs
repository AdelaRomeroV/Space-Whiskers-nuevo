using UnityEngine;

namespace Character.Controller
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private Transform aimDirection, attackPoint;

        private ObjectAnimations _animator;
        private ObjectPhysics _physics;

        private void Awake()
        {
            _animator = GetComponent<ObjectAnimations>();
            _physics = GetComponent<ObjectPhysics>();
        }

        private void OnAttack()
        {
            //shoot knockback
            _physics.AddForce(10 * -aimDirection.right, ForceMode2D.Impulse);
        }
    }
}