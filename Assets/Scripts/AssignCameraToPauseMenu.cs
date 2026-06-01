using UnityEngine;

public class AssignCameraToPauseMenu : MonoBehaviour
{
    void Start()
    {
        PauseMenuManager pauseMenu = FindAnyObjectByType<PauseMenuManager>();
        if (!pauseMenu) return;
        Canvas menuCanvas = pauseMenu.GetComponent<Canvas>();
        Camera cam = GetComponent<Camera>();
        if (!menuCanvas || !cam) return;
        menuCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        menuCanvas.worldCamera = cam;
        Debug.Log("Cam has been assigned");
    }
}
