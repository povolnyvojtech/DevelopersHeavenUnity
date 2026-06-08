using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DatingRegistration : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField surnameInput;
    private Button _saveButton;
    
    public GameObject registrationDisplay;
    public GameObject profileDisplay;

    public static event Action UserHasRegistered;
    

    void Start()
    {
        _saveButton = GetComponentInChildren<Button>();
        _saveButton.onClick.AddListener(GetUserInformation);
    }

    private void GetUserInformation()
    {
        if (nameInput.text == "" || surnameInput.text == "") return;
        GlobalVariables.DatingName = nameInput.text;
        GlobalVariables.DatingSurname = surnameInput.text;
        GlobalVariables.DatingHasRegistered = true;
        registrationDisplay.SetActive(false);
        profileDisplay.SetActive(true);
        UserHasRegistered?.Invoke();
    }
}
