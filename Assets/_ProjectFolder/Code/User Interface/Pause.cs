using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    [SerializeReference] private InputActionReference pauseButton;
    [SerializeReference] private TweenCanvasGroup canvasGroup;

    public static bool IsPaused { get; private set; }
    
    private void OnEnable() => pauseButton.action.performed += OnPauseButtonDown;
    private void OnDisable() => pauseButton.action.performed -= OnPauseButtonDown;

    private void OnPauseButtonDown(InputAction.CallbackContext _)
    {
        if (!IsPaused) PauseButton();
        else ResumeButton();
    }
    private static void SetPauseStatus(bool isPaused)
    {
        IsPaused = isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void PauseButton()
    {
        canvasGroup?.FadeIn();
        SetPauseStatus(true);
    }
    public void ResumeButton()
    {
        canvasGroup?.FadeOut();
        SetPauseStatus(false);
    }
}