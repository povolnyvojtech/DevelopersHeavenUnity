using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVariables : MonoBehaviour
{
    public static string ActiveScene;
    
    //BG variables
    public static int HallBgLevel = 0;
    public static int CurrentHallBgUpgradeCost = 600;
    public static int BedroomBgLevel = 0;
    public static int CurrentBedroomBgUpgradeCost = 500;

    //practice variables
    public static int QualityLevel = 1;
    public static float QualityMultiplier = 1f;
    public static int SpeedLevel = 1;
    public static float SpeedMultiplier = 1f;
    public static bool IsPracticing = false;
    public static int QualityPractisingTime = 20;
    public static int SpeedPractisingTime = 20;
    public static int CurrentPracticingType = 2;
    
    //career variables
    public static bool HasCareer = false;
    public static string CareerPath = "None"; //vždy formát - TYPPROFESEzamereni - GDgodot 

    public static int Money = 5000;
    
    public static int HourRate = 10; //- hodinova sazba (cim vetsi Level, tim vetsi sazba)
    public static int Level = 1;
    public static int Xp;
    
    //job variables
    public static List<Job> JobOffers =  new List<Job>();

    public static bool HasJob = false;
    public static Job CurrentJob;
    public static GameObject JobGameObject;
    public static int CurrentJobMoney;
    public static int CurrentJobTime = 5;
    public static int CurrentJobXp;
    public static float CurrentJobTimerSliderValue;
    public static float CurrentPracticeTimerSliderValue;
    

    //dating variables
    public static float GymLevel = 1; 
    public static float OverallLook = 1; 
    public static float ChanceToGetHoes;
    public static string DatingName;
    public static string DatingSurname;
    public static bool DatingHasRegistered = false;
    public static GameObject PreviousWomanProfile;
    public static bool Way;
    public static WomanProfile CurrentWomanProfile;
    public static List<List<string>> InboxWomen = new List<List<string>>(); 
    public static List<Texture> PhotosTextures =  new List<Texture>();
    
    //gambling
    public static int CurrentSlotBet = 10;
    public static int CurrentBlackJackBet = 10;
    
    //bills
    public static float CurrentElectricityDuration = 180f;
    public static float ElectricityTimerDuration = 180f;
    public static bool HasPaidElectricity = true; //true - eletkrika jede, false - elektrika nejede
    public static bool CurrentElectricityState = true;
    public static float CurrentElectricitySliderValue;
    public static Coroutine ElectricityCoroutine;
    
    public static Coroutine RentCoroutine;
    public static float CurrentRentDuration = 360f;
    public static float RentTimerDuration = 360f;
    public static float CurrentRentSliderValue;

    public static int ElectricityCost = 200;
    public static int RentCost = 1800;
    
    public static bool NewGame;
    
    //DoJob
    public static int NumberOfButtons = 0;


    public static void CalculateChanceToGetGirls()
    {
        ChanceToGetHoes = Mathf.Clamp(GymLevel + OverallLook + (HasJob ? 1 : 0) + Money / 100 + HallBgLevel + BedroomBgLevel, 0, 100);
    }
    
    public static void LevelUp()
    {
        Level = (Xp / 150) < 1 ? 1 : Xp / 150;
        HourRate += (Level % 10 == 0) ? 5 : 0;
    }
    
    public static void UpdateStats(TextMeshProUGUI levelText, TextMeshProUGUI xpText, TextMeshProUGUI moneyText)
    {
        levelText.text = Level.ToString();
        xpText.text = Xp.ToString();
        moneyText.text = Money.ToString();
    }
    
    public static void UpdateJobStats(float currentJobTimeLeft, TextMeshProUGUI jobRemainingTimeStatsText, TextMeshProUGUI jobMoneyStatsText, int currentJobMoney)
    {
        if (currentJobTimeLeft > 0 && jobRemainingTimeStatsText && jobMoneyStatsText && HasJob)
        {
            //CalcTime((int)Mathf.Round(currentJobTimeLeft), jobRemainingTimeStatsText, 1);
            jobRemainingTimeStatsText.text = "";
            jobMoneyStatsText.text = "Money: " + currentJobMoney;
            return;
        }
        jobRemainingTimeStatsText.text = "You have no current job";
        jobMoneyStatsText.text = "";
    }
    
    public static void UpdatePracticeStats(float practiceTimeLeft, TextMeshProUGUI practiceRemainingTimeStatsText, TextMeshProUGUI practiceRewardStatsText, int practiceType) //type - 0 - quality, 1 - speed, 2 - nothing 
    {
        if (practiceTimeLeft > 0 && practiceRemainingTimeStatsText && IsPracticing)
        {
            practiceRemainingTimeStatsText.text = "";
            practiceRewardStatsText.text = practiceType switch
            {
                0 => "Reward: Quality multiplier -> " + (QualityMultiplier + 0.1f),
                1 => "Reward: Speed multiplier -> " + (SpeedMultiplier - 0.07f),
                2 => "",
                _ => throw new ArgumentOutOfRangeException(nameof(practiceType), practiceType, null)
            };
            return;
        }
        practiceRemainingTimeStatsText.text = "You are not practicing at the moment";
        practiceRewardStatsText.text = "";
    }
    
    
    
    public static void CalcTime(int seconds, TextMeshProUGUI timeTillReset, int type) //type - 0 reset, 1 jobTimer
    {
        if (seconds > 60 && seconds % 60 != 0)
        {
            int minutes = seconds / 60;
            int remainingSeconds = seconds % 60;
            timeTillReset.text = type == 0 ? "Reset in: " + minutes + " minutes " + remainingSeconds + " seconds" : "Remaining: " + minutes + " minutes " + remainingSeconds + " seconds";
        }
        else if (seconds % 60 == 0 && seconds != 0)
        {
            int minutes = seconds / 60;
            timeTillReset.text = type == 0 ? "Reset in: " + minutes + " minutes" : "Remaining: " + minutes + " minutes";
        }
        else
        {
            timeTillReset.text =  type == 0 ? "Reset in: " + seconds + " seconds" : "Remaining: " + seconds + " seconds";
        }
    }
    
    public static void RestartGame()
    {
        HallBgLevel = 0;
        CurrentHallBgUpgradeCost = 600;
        BedroomBgLevel = 0;
        CurrentBedroomBgUpgradeCost = 500;
        QualityLevel = 1;
        QualityMultiplier = 1f;
        SpeedLevel = 1;
        SpeedMultiplier = 1f;
        QualityPractisingTime = 20;
        SpeedPractisingTime = 20;
        HasCareer = false;
        CareerPath = "";
        Money = 500;
        Level = 1;
        Xp = 0;
        GymLevel = 1;
        OverallLook = 1;
        ChanceToGetHoes = 0f;
        DatingName = "";
        DatingSurname = "";
        if (ElectricityCoroutine != null) HallTimerManager.Instance.StopCoroutine(ElectricityCoroutine);
        if (RentCoroutine != null) HallTimerManager.Instance.StopCoroutine(RentCoroutine);
        DatingHasRegistered = false;
        RentCoroutine = null;
        RentTimerDuration = 360f;
        CurrentRentDuration = 360f;
        CurrentRentSliderValue = 0f;
        ElectricityCoroutine = null;
        ElectricityTimerDuration = 180f;
        CurrentElectricityDuration = 180f;
        CurrentElectricitySliderValue = 0f;
        HasPaidElectricity = true;
        CurrentElectricityState = true;
        NewGame = true;
    }
}
