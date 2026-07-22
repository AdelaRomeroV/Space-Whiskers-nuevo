using UnityEngine;

namespace Character.Controller.Shooter
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "Items/Bullet")]
    public class ScriptableBullet : ScriptableObject
    {
        [SerializeReference] private Sprite image;
        [SerializeField] private int damage;
        
        public Sprite Image => image;
        public int Damage => damage;
    }
}