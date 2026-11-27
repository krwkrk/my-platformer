using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField, Range(0f, 1f)] private float pausedVolume = 0.3f;

    public static bool IsPaused { get; private set; }

    private bool paused;

    private void Start() => SetPaused(false);

    private void OnDisable()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
        IsPaused = false;
    }

    private void Update()
    {
        bool escPressed = false;
#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            escPressed = true;
#endif
#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetKeyDown(KeyCode.Escape))
            escPressed = true;
#endif
        if (escPressed)
            SetPaused(!paused);
    }

    public void Resume() => SetPaused(false);
    public void Restart() => Flow.Restart();
    public void MainMenu() => Flow.Go(Flow.MainMenuScene);

    private void SetPaused(bool value)
    {
        paused = value;
        IsPaused = paused;
        popup.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f;
        AudioListener.volume = paused ? pausedVolume : 1f;
    }
}