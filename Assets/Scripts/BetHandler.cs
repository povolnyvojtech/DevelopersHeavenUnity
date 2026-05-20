using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetHandler : MonoBehaviour
{
    public TextMeshProUGUI currentBetText;
    public Button lowerBetButton;
    public Button riseBetButton;
    public Button maxBetButton;
    public Button resetBetButton;
    public int type;

    private void OnEnable()
    {
        switch (type)
        {
            case 0:
            {
                currentBetText.text = GlobalVariables.CurrentSlotBet.ToString();
                break;
            }
            case 1:
            {
                currentBetText.text = GlobalVariables.CurrentBlackJackBet.ToString();
                break;
            }
        }
        lowerBetButton.onClick.AddListener(LowerBet);
        riseBetButton.onClick.AddListener(RiseBet);
        maxBetButton.onClick.AddListener(MaxBet);
        resetBetButton.onClick.AddListener(ResetBet);
    }

    private void LowerBet()
    {
        if  (GlobalVariables.Money == 0)
        {
            GlobalVariables.CurrentSlotBet = 0;
            currentBetText.text = "0";
            return;
        }
        switch (type)
        {
            case 0:
            {
                if (GlobalVariables.CurrentSlotBet - 10 < 0) return;
                GlobalVariables.CurrentSlotBet -= 10;
                currentBetText.text = GlobalVariables.CurrentSlotBet.ToString();
                break;
            }
            case 1:
            {
                if (GlobalVariables.CurrentBlackJackBet - 10 < 0) return;
                GlobalVariables.CurrentBlackJackBet -= 10;
                currentBetText.text = GlobalVariables.CurrentBlackJackBet.ToString();
                break;
            }
        }
    }

    private void RiseBet()
    {
        if  (GlobalVariables.Money == 0)
        {
            GlobalVariables.CurrentSlotBet = 0;
            currentBetText.text = "0";
            return;
        }
        switch (type)
        {
            case 0:
            {
                if (GlobalVariables.CurrentSlotBet + 10 > GlobalVariables.Money) return;
                GlobalVariables.CurrentSlotBet += 10;
                currentBetText.text = GlobalVariables.CurrentSlotBet.ToString();
                break;
            }
            case 1:
            {
                if (GlobalVariables.CurrentBlackJackBet + 10 > GlobalVariables.Money) return;
                GlobalVariables.CurrentBlackJackBet += 10;
                currentBetText.text = GlobalVariables.CurrentBlackJackBet.ToString();
                break;
            }
        }
    }

    private void MaxBet()
    {
        if (GlobalVariables.Money == 0)
        {
            GlobalVariables.CurrentSlotBet = 0;
            currentBetText.text = "0";
            return;
        }

        switch (type)
        {
            case 0:
            {
                GlobalVariables.CurrentSlotBet = GlobalVariables.Money;
                currentBetText.text = GlobalVariables.CurrentSlotBet.ToString();
                break;
            }
            case 1:
            {
                GlobalVariables.CurrentBlackJackBet = GlobalVariables.Money;
                currentBetText.text = GlobalVariables.CurrentBlackJackBet.ToString();
                break;
            }
        }
    }

    private void ResetBet()
    {
        if  (GlobalVariables.Money == 0)
        {
            GlobalVariables.CurrentSlotBet = 0;
            currentBetText.text = "0";
        }
        else
        {
            GlobalVariables.CurrentSlotBet = 10;
            currentBetText.text = "10";
        }
    }
}
