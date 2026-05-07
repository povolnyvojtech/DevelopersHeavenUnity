using System;
using UnityEngine;

public class PowerSwitchManager : MonoBehaviour
{
    public GameObject powerSwitchOn;
    public GameObject powerSwitchOff;

    private void Start()
    {
        if (GlobalVariables.NewGame)
        {
            GlobalVariables.ElectricityCoroutine = HallTimerManager.Instance.StartCoroutine(HallTimerManager.Instance.ElectricityTimer(true));
            GlobalVariables.RentCoroutine = HallTimerManager.Instance.StartCoroutine(HallTimerManager.Instance.RentTimer());
            GlobalVariables.NewGame = false;
        }
        if (GlobalVariables.CurrentElectricityState)
        {
            powerSwitchOn.SetActive(true);
            powerSwitchOff.SetActive(false);
            return;
        }
        powerSwitchOn.SetActive(false);
        powerSwitchOff.SetActive(true);
    }
}
