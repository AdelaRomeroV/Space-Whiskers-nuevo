using UnityEngine;

namespace Character.Controller
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Items/Gun")]
    public class ScriptableGun : ScriptableObject
    {
        [SerializeReference] private Sprite image;
        [SerializeField] private float damage;
        [SerializeField] private float knockback;
    }
}