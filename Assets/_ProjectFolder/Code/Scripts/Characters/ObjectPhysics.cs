using UnityEngine;

namespace Character
{
    [SelectionBase]
    public class ObjectPhysics : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody2D _rigidbody2D;
        
        private float _defaultGravityScale;
        private RigidbodyConstraints2D _defaultConstraints;
        
        private bool _freezeHorizontal, _freezeVertical;
        private bool _isGrounded;

        private void Awake()
        {
            _transform = transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            _defaultGravityScale = _rigidbody2D.gravityScale;
            _defaultConstraints = _rigidbody2D.constraints;
        }
        
        //Control de transforms
        public void SetHorizontalDirection(float value) => _transform.localScale = new Vector3(value, 1f, 1f);
        public Vector2 GetHorizontalDirection() => new Vector2(_transform.localScale.x, 0f);
        public void FlipHorizontalDirection() => SetHorizontalDirection(-_transform.localScale.x);
        
        //Control de gravedad
        public void SetGravityScale(float value) => _rigidbody2D.gravityScale = value;
        public void SetGravityScaleZero() => SetGravityScale(0f);
        public void ResetGravityScale() => SetGravityScale(_defaultGravityScale);
        
        //Restriccion de fisicas
        public void SetConstrain(RigidbodyConstraints2D value) => _rigidbody2D.constraints = value;
        public void ResetConstrain() => _rigidbody2D.constraints = _defaultConstraints;
        
        //Condiciones de fisicas
        public bool IsGrounded() => _isGrounded;
        public void SetIsGrounded(bool value) => _isGrounded = value;
        
        public bool IsFreezeHorizontal() => _freezeHorizontal;
        public void SetFreezeHorizontal(bool value) => _freezeHorizontal = value;
        
        public bool IsFreezeVertical() => _freezeVertical;
        public void SetFreezeVertical(bool value) => _freezeVertical = value;

        public bool IsFreeze() => _freezeHorizontal && _freezeVertical;
        public void SetFreeze(bool value)
        {
            SetFreezeHorizontal(value);
            SetFreezeVertical(value);
        }
        
        //Manejo de fuerzas
        public void AddForce(Vector2 force, ForceMode2D mode) => _rigidbody2D.AddForce(force, mode);
        public void AddForceX(float force, ForceMode2D mode) => _rigidbody2D.AddForceX(force, mode);
        public void AddForceY(float force, ForceMode2D mode) => _rigidbody2D.AddForceY(force, mode);
        
        //Manejo de velocidades
        public void SetLinearVelocity(Vector2 direction) => _rigidbody2D.linearVelocity = direction;
        public void SetLinearVelocityX(float value) => _rigidbody2D.linearVelocityX = value;
        public void SetLinearVelocityY(float value) => _rigidbody2D.linearVelocityY = value;
        
        public Vector2 GetLinearVelocity() => _rigidbody2D.linearVelocity;
        public float GetLinearVelocityX() => _rigidbody2D.linearVelocityX;
        public float GetLinearVelocityY() => _rigidbody2D.linearVelocityY;
        
        public bool CompareLayer(GameObject obj, LayerMask mask) => ((1 << obj.gameObject.layer) & mask) != 0;
        public bool IsContactInAngle(Collision2D collision, Vector2 direction, float maxAngle)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                float angle = Vector2.Angle(collision.GetContact(i).normal, direction);
                if (angle <= maxAngle)
                    return true;
            }
            
            return false;
        }

        public RaycastHit2D RayCast2D(Vector2 direction, Vector2 offset, float distance, LayerMask mask)
        {
            Vector2 origin = _rigidbody2D.position + offset;
            return Physics2D.Raycast(origin, direction, distance, mask);
        }
        public RaycastHit2D CircleCast2D(Vector2 direction, Vector2 offset, float radius, float distance, LayerMask mask)
        {
            Vector2 origin = _rigidbody2D.position + offset;
            return Physics2D.CircleCast(origin, radius, direction, distance, mask);
        }
    }
}