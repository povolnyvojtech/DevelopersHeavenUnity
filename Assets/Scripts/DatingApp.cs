using TMPro;
using UnityEngine;

public class DatingApp : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI surnameText;
    public GameObject meetButton;
    public GameObject registrationDisplay;
    public GameObject profileDisplay;
    
    void Start()
    {
        if (!GlobalVariables.DatingHasRegistered)
        {
            DatingRegistration.UserHasRegistered += UpdateUserInfo;
        }
        else
        {
            registrationDisplay.SetActive(false);
            profileDisplay.SetActive(true);
            UpdateUserInfo();
        }
    }

    private void UpdateUserInfo()
    {
        meetButton.SetActive(true);
        if (GlobalVariables.DatingName != "" || GlobalVariables.DatingSurname != "")
        {
            nameText.text = GlobalVariables.DatingName;
            surnameText.text = GlobalVariables.DatingSurname;
        }
        else
        {
            nameText.text = "";
            surnameText.text = "";
        }
    }
}
