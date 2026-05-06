using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerManagerScript : MonoBehaviour
{
    public static TimerManagerScript Instance {get; private set;}
    private const float RefreshInterval = 20f;
    public static float JobOffersTimeLeft;
    public static float CurrentJobTimeLeft;
    public static float PracticingTimeLeft;
    public static GameObject JobTimerImageBg;
    public static Image JobTimerImageFg;
    public static RectTransform JobRt;
    public static GameObject PracticeTimerImageBg;
    public static Image PracticeTimerImageFg;
    public static RectTransform PracticeRt;
    public static Image ElectricityImage;
    public static Image RentImage;
    
    
    public static event Action JobFinished;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(RestartJobOfferTimer());
        }
        else
        {
            Destroy(gameObject);
        }
    }


        
    private static IEnumerator RestartJobOfferTimer()
    {
        while (true)
        {
            JobOffersTimeLeft = RefreshInterval;
        
            while (JobOffersTimeLeft > 0)
            {
                yield return null;
                JobOffersTimeLeft -= Time.deltaTime;
            }
            yield return new WaitForSeconds(0.2f); 
        }
    }

    public static IEnumerator CurrentJobTimer(int jobTime, int jobMoney, int jobXp)
    {
        JobTimerImageBg.SetActive(true);
        float totalToGrow = 200f;
        float duration = jobTime;
        float growthPerSecond = totalToGrow / duration;
        CurrentJobTimeLeft = jobTime * GlobalVariables.SpeedMultiplier;
        
        while (CurrentJobTimeLeft > 0)
        {
            yield return null;
            CurrentJobTimeLeft -= Time.deltaTime;
            GlobalVariables.CurrentJobTimerSliderValue += growthPerSecond * Time.deltaTime;

            if (!JobTimerImageBg || !JobTimerImageFg || !JobRt) continue;
            
            if (GlobalVariables.HasJob && GlobalVariables.ActiveScene == "Desktop")
            {
                JobTimerImageBg.SetActive(true);
                JobRt.sizeDelta = new Vector2(GlobalVariables.CurrentJobTimerSliderValue, 30);
            }
        }
        
        GlobalVariables.HasJob = false;
        JobFinished?.Invoke();
        JobTimerImageBg.SetActive(false);
        GlobalVariables.CurrentJobTimerSliderValue = 0;
        GlobalVariables.Money += (int)Mathf.Round(jobMoney * GlobalVariables.QualityMultiplier);
        GlobalVariables.Xp += jobXp;
        GlobalVariables.LevelUp();
        
        if (JobManager.Instance)
        {
            JobManager.Instance.RefreshUI();
        }
    }

    public static IEnumerator PracticingTimer(int practiceType) //0 - quality, 1 - speed
    {
        PracticeTimerImageBg.SetActive(true);
        switch (practiceType)
        {
            case 0: PracticingTimeLeft = GlobalVariables.QualityPractisingTime; break;
            case 1: PracticingTimeLeft = GlobalVariables.SpeedPractisingTime; break;
        }
        
        float totalToGrow = 200f;
        float duration = PracticingTimeLeft;
        float growthPerSecond = totalToGrow / duration;

        while (PracticingTimeLeft > 0)
        {
            yield return null;
            PracticingTimeLeft -= Time.deltaTime;
            GlobalVariables.CurrentPracticeTimerSliderValue += growthPerSecond * Time.deltaTime;

            if (!PracticeTimerImageBg || !PracticeTimerImageFg || !PracticeRt) continue;

            if (GlobalVariables.IsPracticing && GlobalVariables.ActiveScene == "Desktop")
            {
                PracticeTimerImageBg.SetActive(true);
                PracticeRt.sizeDelta = new Vector2(GlobalVariables.CurrentPracticeTimerSliderValue, 30);
            }
        }
        
        PracticeTimerImageBg.SetActive(false);
        GlobalVariables.CurrentPracticeTimerSliderValue = 0;
        GlobalVariables.IsPracticing = false;
        
        if (JobManager.Instance)
        {
            JobManager.Instance.RefreshUI();
        }
    }

    public static IEnumerator InboxMessageDestroyTimer(Transform inboxContent)
    {
        yield return new WaitForSeconds(10f);
        foreach (Transform message in inboxContent)
        {
            Destroy(message.gameObject);
        }
        GlobalVariables.InboxWomen.Clear();
    }
}