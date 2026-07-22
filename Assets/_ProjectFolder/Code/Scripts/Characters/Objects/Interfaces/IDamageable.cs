using UnityEngine;

namespace Character
{
    public interface IDamageable
    {
        void Damage(int amount, Vector2 direction = default);
    }
}