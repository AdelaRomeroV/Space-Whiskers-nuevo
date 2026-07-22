using UnityEngine;

namespace Character.Controller
{
    public class Shoot : MonoBehaviour
    {
        [SerializeReference] private Transform aimDirection, attackPoint;
        [SerializeReference] private ScriptableGun gun;
        
        private ObjectAnimations _animator;
        private ObjectPhysics _physics;

        private void Awake()
        {
            _animator = GetComponent<ObjectAnimations>();
            _physics = GetComponent<ObjectPhysics>();
        }

        private void OnAttack()
        {
            if (!gun || Time.timeScale == 0f) return;

            Instantiate(gun.Bullet, attackPoint.position, aimDirection.rotation);
            
            //shoot knockback
            _physics.AddForce(gun.Knockback * -aimDirection.right, ForceMode2D.Impulse);
        }
    }
}