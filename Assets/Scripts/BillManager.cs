using System;
using TMPro;
using UnityEngine;

public class BillManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI electricityCostText;
    public TextMeshProUGUI rentCostText;

    private void OnEnable()
    {
        electricityCostText.text = "Cost: " + GlobalVariables.ElectricityCost;
        rentCostText.text = "Cost: " +  GlobalVariables.RentCost;
    }

public void RestartElectricityTimer()
    {
        if (GlobalVariables.Money - GlobalVariables.ElectricityCost < 0) return;
        if (GlobalVariables.ElectricityCoroutine != null)
        {
            HallTimerManager.Instance.StopCoroutine(GlobalVariables.ElectricityCoroutine);
        }

        GlobalVariables.ElectricityTimerDuration = 180f+GlobalVariables.CurrentElectricityDuration;
        GlobalVariables.CurrentElectricityDuration = GlobalVariables.ElectricityTimerDuration;
        GlobalVariables.CurrentElectricitySliderValue = 0;
        GlobalVariables.Money -= GlobalVariables.ElectricityCost;
        moneyText.text = GlobalVariables.Money.ToString();
        GlobalVariables.ElectricityCoroutine = HallTimerManager.Instance.StartCoroutine(HallTimerManager.Instance.ElectricityTimer(true));
    }

    public void RestartRentTimer()
    {
        if (GlobalVariables.Money - GlobalVariables.RentCost < 0) return;
        if (GlobalVariables.RentCoroutine != null)
        {
            HallTimerManager.Instance.StopCoroutine(GlobalVariables.RentCoroutine);
        }
        GlobalVariables.RentTimerDuration = 360f+GlobalVariables.CurrentRentDuration;
        GlobalVariables.CurrentRentDuration = GlobalVariables.RentTimerDuration;
        GlobalVariables.CurrentRentSliderValue = 0;
        GlobalVariables.Money -= GlobalVariables.RentCost;
        moneyText.text = GlobalVariables.Money.ToString();
        GlobalVariables.RentCoroutine = HallTimerManager.Instance.StartCoroutine(HallTimerManager.Instance.RentTimer());
    }
}