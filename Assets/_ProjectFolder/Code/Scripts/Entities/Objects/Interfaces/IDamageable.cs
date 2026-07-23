using UnityEngine;

namespace Entity
{
    public interface IDamageable
    {
        void Damage(int amount, Vector2 direction = default);
    }
}