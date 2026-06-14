using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PracticeCanvasManager : MonoBehaviour
{
    public TextMeshProUGUI qualityLevelText;
    public TextMeshProUGUI speedLevelText;

    public GameObject finishedCanvas;
    public static PracticeCanvasManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        qualityLevelText.text = "Level " + (GlobalVariables.QualityLevel);
        speedLevelText.text = "Level " + (GlobalVariables.SpeedLevel);
    }

    private void OnEnable()
    {
        RefreshLevelText(qualityLevelText, 0);
        RefreshLevelText(speedLevelText, 1);
    }

    public void RefreshLevelText(TextMeshProUGUI levelText, int type)
    {
        switch (type)
        {
            case 0: levelText.text = "Level " + (GlobalVariables.QualityLevel);break;
            case 1: levelText.text = "Level " + (GlobalVariables.SpeedLevel); break;
        }
    }
    
    public void RaiseQualityMultiplier()
    {
        if (GlobalVariables.IsPracticing || GlobalVariables.QualityLevel == 10)
        {
            Debug.Log("Is already practicing");
            return;
        };
        Debug.Log("Practicing beginning");
        GlobalVariables.IsPracticing = true;
        GlobalVariables.CurrentPracticingType = 0;
        StartCoroutine(TimerManagerScript.PracticingTimer(0));
        StartCoroutine(QualityPractisingTimer());
    }
    
    public void RaiseSpeedMultiplier()
    {
        if (GlobalVariables.IsPracticing || GlobalVariables.SpeedLevel == 10) return;
        GlobalVariables.IsPracticing = true;
        GlobalVariables.CurrentPracticingType = 1;
        StartCoroutine(TimerManagerScript.PracticingTimer(1));
        StartCoroutine(SpeedPractisingTimer());
    }
    
    private IEnumerator QualityPractisingTimer()
    {
        yield return new WaitForSeconds(GlobalVariables.QualityPractisingTime);
        GlobalVariables.QualityLevel += 1;
        RefreshLevelText(qualityLevelText, 0);
        GlobalVariables.CurrentPracticingType = 2;
        
        //TODO qualityLevelText je null protože corutina, to se vztahuje i na qualityButtonText - z nějakýho důvodu to funguje
        if (GlobalVariables.ActiveScene == "Desktop" && qualityLevelText)
        {
            qualityLevelText.text = "Level " + (GlobalVariables.QualityLevel);
        }
        
        GlobalVariables.QualityMultiplier += 0.1f;
        --GlobalVariables.QualityPractisingTime;
        GlobalVariables.IsPracticing = false;
        if (finishedCanvas)
        {
            finishedCanvas.SetActive(true);
        }
        
    }

    private IEnumerator SpeedPractisingTimer(){
        yield return new WaitForSeconds(GlobalVariables.SpeedPractisingTime);
        GlobalVariables.SpeedLevel += 1;
        RefreshLevelText(speedLevelText, 1);
        GlobalVariables.CurrentPracticingType = 2;
        speedLevelText.text = "Level " + (GlobalVariables.SpeedLevel);
        if ((GlobalVariables.SpeedMultiplier -= 0.07f) > 0) GlobalVariables.SpeedMultiplier -= 0.07f;
        --GlobalVariables.SpeedPractisingTime;
        GlobalVariables.IsPracticing = false;
        if (finishedCanvas)
        {
            finishedCanvas.SetActive(true);
        }
    }
}
