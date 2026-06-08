using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetupInboxMessage : MonoBehaviour
{
    public TextMeshProUGUI womanNameText;
    public TextMeshProUGUI messageText;

    public void SetupMessage(List<string> inboxWomenMessages)
    {
        womanNameText.text = inboxWomenMessages[0] + " - " +  inboxWomenMessages[1];
        messageText.text = inboxWomenMessages[2];
    }
}
