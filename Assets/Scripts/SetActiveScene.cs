using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetActiveScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalVariables.ActiveScene = "Desktop";
    }
}
