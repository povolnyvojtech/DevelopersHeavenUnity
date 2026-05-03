using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScript : MonoBehaviour
{
    public GameObject interactionButton;
    public GameObject lockedText;
    public string targetScene;
    public bool locked;
    public bool powerSwitch;
    public GameObject powerSwitchOn;
    public GameObject powerSwitchOff;
    
    private bool _isPlayerInRange;

    private void Start()
    {
        targetScene ??= "none";
        interactionButton.SetActive(false);
    }

    private void Update()
    {
        if (_isPlayerInRange && !locked && Input.GetKeyDown(KeyCode.E))    
        {
            if (powerSwitch)
            { 
                switch (GlobalVariables.CurrentElectricityState)
                {
                    case true:
                    {
                        powerSwitchOn.SetActive(false);
                        powerSwitchOff.SetActive(true);
                        LightManager.Instance.TurnPowerOff();
                        break;
                    }
                    case false:
                    {
                        powerSwitchOn.SetActive(true);
                        powerSwitchOff.SetActive(false);
                        LightManager.Instance.TurnPowerOn();
                        break;
                    }
                }

                return;
            }
            SceneManager.LoadScene(targetScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!interactionButton) return;
        _isPlayerInRange = true; 

        if (locked)
        {
            lockedText.SetActive(true);
        }
        interactionButton.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!interactionButton) return;
        _isPlayerInRange = false;

        if (locked)
        {
            lockedText.SetActive(false);
        }
        interactionButton.SetActive(false);
    }
}