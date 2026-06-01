using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public CanvasGroup canvasGroup;
    public Button resumeButton;
    public Button quitButton;
    public static PauseMenuManager Instance;

    private void Start()
    {
        if (!Instance)
        {
            Instance = this;
        }
        pauseCanvas.GetComponent<CanvasGroup>().alpha = 0;
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

    public void ResumeGame()
    {
        canvasGroup.alpha = 0;
        Time.timeScale = 1;
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