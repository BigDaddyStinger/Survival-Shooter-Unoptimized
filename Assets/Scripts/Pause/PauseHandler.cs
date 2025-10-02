using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{
    public InputActionReference pauseAction;

    public GameObject pauseMenuUI;

    public UnityEvent onPaused;
    public UnityEvent onResumed;

    private bool isPaused;

    void OnEnable()
    {
        if (pauseAction != null)
        {
            pauseAction.action.Enable();
            pauseAction.action.performed += OnPausePerformed;
        }
    }

    void OnDisable()
    {
        if (pauseAction != null)
            pauseAction.action.performed -= OnPausePerformed;
    }

    private void OnPausePerformed(InputAction.CallbackContext ctx)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        if (pauseMenuUI) pauseMenuUI.SetActive(isPaused);
        if (isPaused) onPaused?.Invoke(); else onResumed?.Invoke();

        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }
}