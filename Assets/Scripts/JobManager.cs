using UnityEngine;
using TMPro;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UI;

public class JobManager : MonoBehaviour
{
    public static JobManager Instance { get; private set; }
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI jobRemainingTimeStatsText;
    public TextMeshProUGUI jobMoneyStatsText;
    public TextMeshProUGUI practiceRemainingTimeStatsText;
    public TextMeshProUGUI practiceRewardStatsText;
    public GameObject doJobCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        RefreshUI();
    }

    private void Update()
    {
        if (GlobalVariables.HasJob)
        {
            GlobalVariables.UpdateJobStats(TimerManagerScript.CurrentJobTimeLeft, jobRemainingTimeStatsText, jobMoneyStatsText, GlobalVariables.CurrentJobMoney);
        }

        if (GlobalVariables.IsPracticing)
        {
            GlobalVariables.UpdatePracticeStats(TimerManagerScript.PracticingTimeLeft, practiceRemainingTimeStatsText, practiceRewardStatsText, GlobalVariables.CurrentPracticingType);
        }
    }

    public void RefreshUI()
    {
        GlobalVariables.UpdateStats(levelText, xpText, moneyText);
        GlobalVariables.UpdateJobStats(TimerManagerScript.CurrentJobTimeLeft, jobRemainingTimeStatsText, jobMoneyStatsText, GlobalVariables.CurrentJobMoney);
        GlobalVariables.UpdatePracticeStats(TimerManagerScript.PracticingTimeLeft, practiceRemainingTimeStatsText, practiceRewardStatsText, GlobalVariables.CurrentPracticingType);
    }
}