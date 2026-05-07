using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public CanvasGroup canvasGroup;
    public Button resumeButton;
    public Button quitButton;

    private bool _isPaused;

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (_isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(ResumeGame);
        quitButton.onClick.RemoveListener(QuitGame);
    }

    private void PauseGame()
    {
        canvasGroup.alpha = 1;
        Time.timeScale = 0;
        _isPaused = true;
    }

    public void ResumeGame()
    {
        canvasGroup.alpha = 0;
        Time.timeScale = 1;
        _isPaused = false;
    }

    private static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif !UNITY_WEBGL
            Application.Quit();
#endif
    }
}