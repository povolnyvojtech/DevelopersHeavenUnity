using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HowToPopUp : MonoBehaviour
{
    public Button popUpButton;
    public GameObject exitButton;
    public GameObject popUpImage;
    public TextMeshProUGUI howToText;
    [TextArea(3, 50)]
    public string text;
    
    void Start()
    {
        popUpButton.onClick.AddListener(ShowPopUp);
    }

    private void ShowPopUp()
    {
        popUpImage.SetActive(true);
        exitButton.SetActive(true);
        howToText.text = text;
    }
}
