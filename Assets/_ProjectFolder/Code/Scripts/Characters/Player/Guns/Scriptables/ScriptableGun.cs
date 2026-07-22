using UnityEngine;

namespace Character.Controller.Shooter
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Items/Gun")]
    public class ScriptableGun : ScriptableObject
    {
        [Header("Visual")]
        [SerializeReference] private Sprite gunImage;

        [Header("Bullet Type")]
        [SerializeReference] private ScriptableBullet bullet;
        [SerializeField] private float speed, knockback;

        public Sprite GunImage => gunImage;
        
        public ScriptableBullet Bullet => bullet;
        public float Speed => speed;
        public float Knockback => knockback;
    }
}