using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextCreateCall : MonoBehaviour
{
    public int practiceType;
    public Button practiceButton;
    private void Awake()
    {
        switch (practiceType)
        {
            case 0: PracticeCanvasManager.Instance.qualityLevelText = GetComponent<TextMeshProUGUI>();
                PracticeCanvasManager.Instance.RefreshLevelText(GetComponent<TextMeshProUGUI>(), practiceType);
                practiceButton.onClick.AddListener(PracticeCanvasManager.Instance.RaiseQualityMultiplier);
                break;
            case 1: PracticeCanvasManager.Instance.speedLevelText = GetComponent<TextMeshProUGUI>();
                PracticeCanvasManager.Instance.RefreshLevelText(GetComponent<TextMeshProUGUI>(), practiceType); 
                practiceButton.onClick.AddListener(PracticeCanvasManager.Instance.RaiseQualityMultiplier);
                break;
        }
    }
}
