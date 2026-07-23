using UnityEngine;

namespace Entity.Controller.Shooter
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Items/Gun")]
    public class ScriptableGun : ScriptableObject
    {
        [Header("Visual")]
        [SerializeReference] private Sprite gunImage;

        [Header("Bullet Type")]
        [SerializeReference] private ScriptableBullet bullet;
        [SerializeField] private float velocity, recoil;

        public Sprite GunImage => gunImage;
        
        public ScriptableBullet Bullet => bullet;
        public float Velocity => velocity;
        public float Recoil => recoil;
    }
}