using System;
using TMPro;
using UnityEngine;

public class BillManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public void RestartElectricityTimer()
    {
        if (GlobalVariables.Money - 200 < 0) return;
        if (GlobalVariables.ElectricityCoroutine != null)
        {
            HallTimerManager.Instance.StopCoroutine(GlobalVariables.ElectricityCoroutine);
        }
        GlobalVariables.CurrentElectricitySliderValue = 0;
        GlobalVariables.ElectricityDuration += 5f;
        GlobalVariables.Money -= 200;
        moneyText.text = GlobalVariables.Money.ToString();
        GlobalVariables.ElectricityCoroutine = HallTimerManager.Instance.StartCoroutine(HallTimerManager.Instance.ElectricityTimer(true));
    }
}