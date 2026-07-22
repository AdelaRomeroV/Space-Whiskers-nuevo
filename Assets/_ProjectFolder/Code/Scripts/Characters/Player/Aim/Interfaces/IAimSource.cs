using UnityEngine;

namespace Character.Controller
{
    public interface IAimSource
    {
        bool TryGetAimDirection(out Vector2 direction);
    }
}