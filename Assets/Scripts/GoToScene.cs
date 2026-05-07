using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToHall : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");   
    }
}
