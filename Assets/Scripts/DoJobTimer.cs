using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class DoJobTimer : MonoBehaviour
{

    public Image jobImage;
    private RectTransform _jobRt;
    
    private void OnEnable()
    {
        _jobRt = jobImage.GetComponent<RectTransform>();
        StartCoroutine(JobTimer(_jobRt));
    }

    private static IEnumerator JobTimer(RectTransform rt)
    {
        float totalToGrow = 1920f;
        float duration = GlobalVariables.CurrentJobTime*GlobalVariables.SpeedMultiplier;
        float growthPerSecond = totalToGrow / duration;
        float currentJobTimeLeft = duration * GlobalVariables.SpeedMultiplier;
        
        while (currentJobTimeLeft > 0)
        {
            yield return null;
            currentJobTimeLeft -= Time.deltaTime;
            GlobalVariables.CurrentJobTimerSliderValue += growthPerSecond * Time.deltaTime;
            
            rt.sizeDelta = new Vector2(GlobalVariables.CurrentJobTimerSliderValue, 50);
        }
        TimerManagerScript.FinishJob();
    }
}
