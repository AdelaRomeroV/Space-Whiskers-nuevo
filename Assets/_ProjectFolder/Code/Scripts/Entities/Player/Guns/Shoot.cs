using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Pool;

namespace Entity.Controller.Shooter
{
    public class Shoot : PoolBehaviour
    {
        [SerializeField] private CinemachinePositionComposer virtualCamera;
        
        [Header("Gun Handler")]
        [SerializeReference] private Transform aimDirection;
        [SerializeReference] private Transform attackPoint;
        [SerializeReference] private ScriptableGun currentGun;
        
        private ObjectAnimations _animator;

        protected override void Awake()
        {
            _animator = GetComponent<ObjectAnimations>();
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
            pooledObject?.Setup(currentGun.Bullet, currentGun.Velocity);
            
            //shoot recoil
            Vector2 position = virtualCamera.transform.position;
            Vector2 direction = currentGun.Recoil * -aimDirection.right;
            
            virtualCamera.ForceCameraPosition(position + direction, Quaternion.identity);
        }
    }
}