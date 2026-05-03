using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SwitchChooseXDesktop : MonoBehaviour
{
    public GameObject interactionButton;
    public GameObject electricityOffText;
    private string _targetScene;
    
    private bool _isPlayerInRange;
    

    private void Start()
    {
        interactionButton.SetActive(false);
    }

    private void Update()
    {
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E) && _targetScene != "none")
        {
            if(GlobalVariables.CurrentElectricityState)
            {
                SceneManager.LoadScene(_targetScene);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!interactionButton) return;
        if (!GlobalVariables.CurrentElectricityState)
        {
            electricityOffText.SetActive(true);
            return;
        };
        _targetScene = GlobalVariables.HasCareer ? "Desktop" : "PcChoosePath";
        interactionButton.SetActive(true);
        _isPlayerInRange = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!interactionButton) return;
        if (!GlobalVariables.CurrentElectricityState)
        {
            electricityOffText.SetActive(false);
            _isPlayerInRange = false;
            return;
        };
        interactionButton.SetActive(false);
        _isPlayerInRange = false;
    }
}