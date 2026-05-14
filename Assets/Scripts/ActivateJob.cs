using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivateJob : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public Button button;
    public GameObject doJobCanvas;

    private void Start()
    {
        button.onClick.AddListener(ActivateJobFun);
        if (!GlobalVariables.HasJob)
        {
            buttonText.text = "Start Contract";
            buttonText.GetComponent<TextMeshProUGUI>().color = Color.black;
        }
        else
        {
            buttonText.text = "Ongoing job";
            buttonText.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
    }

    private void UpdateButtonText()
    {
        if (!GlobalVariables.HasJob)
        {
            buttonText.text = "Start Contract";
            buttonText.GetComponent<TextMeshProUGUI>().color = Color.black;
        }
        else
        {
            buttonText.text = "Ongoing job";
            buttonText.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
    }
    
    private void OnEnable()
    {
        UpdateButtonText();
        TimerManagerScript.JobFinished += UpdateButtonText;
    }

    private void OnDisable()
    {
        TimerManagerScript.JobFinished -= UpdateButtonText;
    }

    private void ActivateJobFun()
    {
        if (GlobalVariables.HasJob) return;
        buttonText.text = "Ongoing job";
        buttonText.GetComponent<TextMeshProUGUI>().color = Color.red;
        DoJob.StartContract(GlobalVariables.CurrentJob.JobTime, GlobalVariables.CurrentJob.JobMoney, GlobalVariables.CurrentJob.JobXp);
        Destroy(GlobalVariables.JobGameObject);
        DisplayContractInfo.Instance.ClearJobInfo();
        doJobCanvas.SetActive(true);
    }
}
