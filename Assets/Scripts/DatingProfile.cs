using System;
using TMPro;
using UnityEngine;

public class DatingProfile : MonoBehaviour
{
    public TextMeshProUGUI username;

    private void OnEnable()
    {
        username.text = GlobalVariables.DatingName + " " + GlobalVariables.DatingSurname;
    }
}
