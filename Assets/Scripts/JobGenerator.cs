using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class JobGenerator : MonoBehaviour
{
    public static JobGenerator Instance;
    public GameObject jobPrefab;
    public Transform contentParent;
    private string _jobType = "GDgodot"; //GDgodot is for developing TODO smazat pro build
    public TextMeshProUGUI timeTillReset;
    private readonly List<string> _gameDevJobTypes = new List<string>() {"Rockstar Games", "Naughty Dog", "CD Projekt", "FromSoftware", "Bethesda Game Studios", "Larian Studios", "Valve", "Remedy Entertainment", "Guerrilla Games"};
    private readonly List<string> _webDevSoftwareEngineerJobTypes = new List<string>() {"Google", "Meta", "Shopify", "Stripe", "Netflix", "Spotify", "Vercel", "GitHub", "Airbnb"};

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        StartCoroutine(RestartJobOfferTimer());
    }

    private string ChooseRandomJobType(List<string> types)
    {
        return types[Random.Range(0, types.Count)];
    }
    
    public void RefreshAllJobsUI()
    {
        foreach (Transform child in contentParent)
        {
            SetupJob setupJob = child.GetComponent<SetupJob>();
            if (setupJob != null)
            {
                setupJob.RefreshUI();
            }
        }
    }
    
    private void GenerateJobs(int count)
    {
        if (GlobalVariables.JobOffers.Count == 0)
        {
            for (int i = 0; i < count; i++)
            {
                _jobType = GlobalVariables.CareerPath switch
                {
                    "GDgodot" => ChooseRandomJobType(_gameDevJobTypes),
                    "GDunity" => ChooseRandomJobType(_gameDevJobTypes),
                    "GDue" => ChooseRandomJobType(_gameDevJobTypes),
                    "WDfrontend" => ChooseRandomJobType(_webDevSoftwareEngineerJobTypes),
                    "WDbackend" => ChooseRandomJobType(_webDevSoftwareEngineerJobTypes),
                    "SEpython" => ChooseRandomJobType(_webDevSoftwareEngineerJobTypes),
                    "SEjava" => ChooseRandomJobType(_webDevSoftwareEngineerJobTypes),
                    _ => _jobType //pokud _jobType není nic z uvedenýho tak tam nechá co tam bylo
                };
                if (_jobType != null)
                {
                    int time = Random.Range(10, 100); // v hodinach
                    int money = time * GlobalVariables.HourRate;
                    int xp = time * 3; //3 je random konstanta na násobení času k získání xp TODO
                    
                    
                    GlobalVariables.JobOffers.Add(new Job(_jobType, time, money, xp));
                }
            }
        }
    }

    private void InitiateJobs()
    {
        foreach (Job job in GlobalVariables.JobOffers)
        {
            GameObject newJob = Instantiate(jobPrefab, contentParent);
            newJob.GetComponent<SetupJob>().Setup(job);
        }
    }
    
    
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator RestartJobOfferTimer()
    {
        while (true)
        {
            GenerateJobs(10);
            InitiateJobs();

            while (TimerManagerScript.JobOffersTimeLeft > 0)
            {
                float timeLeft = TimerManagerScript.JobOffersTimeLeft;
                if (timeTillReset != null)
                {
                    GlobalVariables.CalcTime((int)Mathf.Round(timeLeft), timeTillReset, 0);
                }
                yield return null;
            }

            ClearJobs();
            yield return new WaitUntil(() => TimerManagerScript.JobOffersTimeLeft > 0);
        }
    }

    private void ClearJobs()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
        GlobalVariables.JobOffers.Clear();
    }
}
