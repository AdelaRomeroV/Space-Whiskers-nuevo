using UnityEngine;
using UnityEngine.Pool;

namespace Entity.Controller.Shooter
{
    public class Bullet : PoolObject
    {
        [SerializeReference] private SpriteRenderer render;
        private Rigidbody2D _rigidbody;

        private float _speed = 1f;
        private int _damage = 1;

        protected override void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Reset() => render = GetComponent<SpriteRenderer>();
        private void FixedUpdate() => _rigidbody.MovePosition(Time.fixedDeltaTime * _speed * Transform.right + Transform.position);
        private void OnBecameInvisible() => Release();
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
                ApplyDamage(damageable, other.transform.position);
            
            Release();
        }

        private void ApplyDamage(IDamageable damageable, Vector2 target)
        {
            Vector2 direction = target - (Vector2)Transform.position;
            damageable.Damage(_damage, direction.normalized);
        }

        public void Setup(ScriptableBullet bullet, float speed = 1f)
        {
            render.sprite = bullet.Image;
            _damage = bullet.Damage;
            _speed = speed;
        }
    }
}