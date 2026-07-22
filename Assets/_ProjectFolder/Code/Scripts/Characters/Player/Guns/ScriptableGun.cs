using UnityEngine;

namespace Character.Controller
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Items/Gun")]
    public class ScriptableGun : ScriptableObject
    {
        [Header("Visual")]
        [SerializeReference] private Sprite gunImage;

        [Header("Bullet Type")]
        [SerializeReference] private Bullet bullet;
        [SerializeField] private float knockback;

        public Bullet Bullet => bullet;
        public float Knockback => knockback;
    }
}