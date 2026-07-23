using UnityEngine;

namespace Entity.Controller.Aim
{
    public interface IAimSource
    {
        bool TryGetAimDirection(out Vector2 direction);
    }
}