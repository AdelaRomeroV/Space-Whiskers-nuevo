using UnityEngine;
using UnityEngine.Pool;

namespace Character.Controller.Shooter
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
        private void FixedUpdate() => _rigidbody.MovePosition(Time.fixedDeltaTime * _speed * transform.right + transform.position);
        private void OnBecameInvisible() => Release();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
                damageable.Damage(_damage);
            
            Release();
        }

        public void Setup(ScriptableBullet bullet, float speed = 1f)
        {
            render.sprite = bullet.Image;
            _damage = bullet.Damage;
            _speed = speed;
        }
    }
}