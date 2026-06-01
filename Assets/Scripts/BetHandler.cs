using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetHandler : MonoBehaviour
{
    public TMP_InputField currentBetInput;
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
                currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
                break;
            }
            case 1:
            {
                currentBetInput.text = GlobalVariables.CurrentBlackJackBet.ToString();
                break;
            }
        }
        lowerBetButton.onClick.AddListener(LowerBet);
        riseBetButton.onClick.AddListener(RiseBet);
        maxBetButton.onClick.AddListener(MaxBet);
        resetBetButton.onClick.AddListener(ResetBet);
        currentBetInput.onValueChanged.AddListener(delegate { ChangeBetByInput();});
    }

    private void ChangeBetByInput()
    {
        GlobalVariables.CurrentSlotBet = int.Parse(currentBetInput.text);
        currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
    }

    private void LowerBet()
    {
        if  (GlobalVariables.Money == 0)
        {
            GlobalVariables.CurrentSlotBet = 0;
            currentBetInput.text = "0";
            return;
        }
        switch (type)
        {
            case 0:
            {
                if (GlobalVariables.CurrentSlotBet - 10 < 0) return;
                GlobalVariables.CurrentSlotBet -= 10;
                currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
                break;
            }
            case 1:
            {
                if (GlobalVariables.CurrentBlackJackBet - 10 < 0) return;
                GlobalVariables.CurrentBlackJackBet -= 10;
                currentBetInput.text = GlobalVariables.CurrentBlackJackBet.ToString();
                break;
            }
        }
    }

    private void RiseBet()
    {
        if  (GlobalVariables.Money == 0)
        {
            GlobalVariables.CurrentSlotBet = 0;
            currentBetInput.text = "0";
            return;
        }
        switch (type)
        {
            case 0:
            {
                if (GlobalVariables.CurrentSlotBet + 10 > GlobalVariables.Money) return;
                GlobalVariables.CurrentSlotBet += 10;
                Debug.Log(GlobalVariables.CurrentSlotBet);
                currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
                break;
            }
            case 1:
            {
                if (GlobalVariables.CurrentBlackJackBet + 10 > GlobalVariables.Money) return;
                GlobalVariables.CurrentBlackJackBet += 10;
                currentBetInput.text = GlobalVariables.CurrentBlackJackBet.ToString();
                break;
            }
        }
    }

    private void MaxBet()
    {
        if (GlobalVariables.Money == 0)
        {
            GlobalVariables.CurrentSlotBet = 0;
            currentBetInput.text = "0";
            return;
        }

        switch (type)
        {
            case 0:
            {
                GlobalVariables.CurrentSlotBet = GlobalVariables.Money;
                currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
                break;
            }
            case 1:
            {
                GlobalVariables.CurrentBlackJackBet = GlobalVariables.Money;
                currentBetInput.text = GlobalVariables.CurrentBlackJackBet.ToString();
                break;
            }
        }
    }

    private void ResetBet()
    {
        if  (GlobalVariables.Money == 0)
        {
            GlobalVariables.CurrentSlotBet = 0;
            currentBetInput.text = "0";
        }
        else
        {
            GlobalVariables.CurrentSlotBet = 10;
            currentBetInput.text = "10";
        }
    }
}
