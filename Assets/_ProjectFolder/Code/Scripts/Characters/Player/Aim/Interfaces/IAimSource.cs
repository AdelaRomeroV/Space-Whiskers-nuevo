using UnityEngine;

namespace Character.Controller.Aim
{
    public interface IAimSource
    {
        bool TryGetAimDirection(out Vector2 direction);
    }
}