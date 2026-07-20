using UnityEngine;

namespace Character
{
    public class ObjectAnimations : MonoBehaviour
    {
        private ObjectPhysics _physics;
        private Animator _animator;
        
        private readonly string _speedHParam = "speed";
        
        private void Awake()
        {
            _physics = GetComponent<ObjectPhysics>();
            _animator = GetComponent<Animator>();
        }
        private void FixedUpdate()
        {
            SetFloat(_speedHParam, Mathf.Abs(_physics.GetLinearVelocityX()));
        }
        
        public void SetInt(string name, int value) => _animator.SetInteger(name, value);
        public void SetBool(string name, bool value) => _animator.SetBool(name, value);
        public void SetFloat(string name, float value) => _animator.SetFloat(name, value);
        public void SetTrigger(string name) => _animator.SetTrigger(name);
    }
}