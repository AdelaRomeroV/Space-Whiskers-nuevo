using System.Collections;
using UnityEngine;

namespace Character.Controller
{
    public class AimSourceAutomatic : MonoBehaviour, IAimSource
    {
        [SerializeField, Min(0.02f)] private float refreshRate = 0.02f;
        [SerializeField] private float radius;
        [SerializeField] private ContactFilter2D contactFilter;

        private WaitForSeconds _waitRefreshRate;
        
        private readonly Collider2D[] _results = new Collider2D[10];
        private Collider2D _target;
        
        private void Awake() => _waitRefreshRate = new(refreshRate);
        private void OnEnable() => StartCoroutine(UpdateCheckStatus());
        private void OnDrawGizmosSelected()
        {
            if (!enabled) return;
            Gizmos.color = Color.limeGreen;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        private IEnumerator UpdateCheckStatus()
        {
            while (enabled) {
                yield return _waitRefreshRate;
                CheckProximity();
            }
        }
        private void CheckProximity()
        {
            Vector2 position = transform.position;
            Physics2D.OverlapCircle(position, radius, contactFilter, _results);
            
            float distance = float.MaxValue;
            _target = null;

            foreach (Collider2D c in _results)
            {
                if (!c) continue;
                Vector2 direction = (Vector2)c.transform.position - position;
                
                if (direction.magnitude < distance) {
                    distance = direction.magnitude;
                    _target = c;
                }
            }
        }

        public bool TryGetAimDirection(out Vector2 direction)
        {
            direction = _target ? (_target.transform.position - transform.position).normalized : Vector2.right;
            return true;
        }
    }
}