using UnityEngine;
using UnityEngine.Pool;

namespace Character.Controller.Shooter
{
    public class Shoot : PoolBehaviour
    {
        [Header("Gun Handler")]
        [SerializeReference] private Transform aimDirection;
        [SerializeReference] private Transform attackPoint;
        [SerializeReference] private ScriptableGun currentGun;
        
        private ObjectAnimations _animator;
        private ObjectPhysics _physics;

        protected override void Awake()
        {
            _animator = GetComponent<ObjectAnimations>();
            _physics = GetComponent<ObjectPhysics>();
            base.Awake();
        }
        protected override void OnGet(IObjectPooled @object)
        {
            @object.Transform.SetPositionAndRotation(attackPoint.position, aimDirection.rotation);
            base.OnGet(@object);
        }

        private void OnAttack()
        {
            if (!currentGun || Time.timeScale == 0f) return;

            Bullet pooledObject = GetPrefab() as Bullet;
            pooledObject?.Setup(currentGun.Bullet, currentGun.Speed);
            
            //shoot knockback
            _physics.AddForce(currentGun.Knockback * -aimDirection.right, ForceMode2D.Impulse);
        }
    }
}