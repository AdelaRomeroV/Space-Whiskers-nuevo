using UnityEngine;
using Character.Controller.Life;

namespace UserInterface.Stats
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeReference] private Health health;

        private void OnEnable() => health.OnValueChanged += OnValueChanged;
        private void OnDisable() => health.OnValueChanged -= OnValueChanged;

        private void OnValueChanged(float value)
        {
            
        }
    }
}