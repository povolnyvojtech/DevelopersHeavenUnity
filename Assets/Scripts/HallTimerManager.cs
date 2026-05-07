using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HallTimerManager : MonoBehaviour
{
    public static HallTimerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            GlobalVariables.ElectricityCoroutine = StartCoroutine(ElectricityTimer(true));
            GlobalVariables.RentCoroutine = StartCoroutine(RentTimer());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator ElectricityTimer(bool type) //true - hasPaidElectricity, false - electricity if off
    {
        switch (type)
        {
            case true:
            {
                while (GlobalVariables.CurrentElectricityDuration > 0)
                {
                    yield return null;
                    GlobalVariables.CurrentElectricityDuration -= Time.deltaTime;
                    GlobalVariables.CurrentElectricitySliderValue += (200/GlobalVariables.ElectricityTimerDuration) * Time.deltaTime;
                }
                GlobalVariables.HasPaidElectricity = false;
                GlobalVariables.CurrentElectricityState = false;
                if (SceneManager.GetActiveScene().name == "Desktop")
                {
                    SceneManager.LoadScene("Bedroom");
                }
                break;
            }
            case false:
            {
                yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Desktop");
                yield return new WaitForSeconds(5f);
                if (!GlobalVariables.HasPaidElectricity)
                {
                    SceneManager.LoadScene("Bedroom");
                    GlobalVariables.CurrentElectricityState = false;
                    break;
                }
                GlobalVariables.CurrentElectricityState = true;
                break;
            }
        }
        GlobalVariables.ElectricityCoroutine = null;
    }

    public IEnumerator RentTimer()
    {
        while (GlobalVariables.CurrentRentDuration > 0)
        {
            yield return null;
            GlobalVariables.CurrentRentDuration -= Time.deltaTime;
            GlobalVariables.CurrentRentSliderValue += (200/GlobalVariables.RentTimerDuration) * Time.deltaTime;
        }
        GlobalVariables.RestartGame();
        SceneManager.LoadScene("GameOver");
    }
}
