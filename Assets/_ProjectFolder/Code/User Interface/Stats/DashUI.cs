using UnityEngine;
using UnityEngine.UI;
using Entity.Controller.Movement;

namespace UserInterface.Stats
{
    public class DashUI : MonoBehaviour
    {
        [SerializeReference] private Dash dash;
        [SerializeReference] private Image fillImage;

        private void OnEnable() => dash.OnTimeoutUpdated += OnUpdateTimer;
        private void OnDisable() => dash.OnTimeoutUpdated -= OnUpdateTimer;

        private void OnUpdateTimer(float value)
        {
            fillImage.fillAmount = value;
        }
    }
}