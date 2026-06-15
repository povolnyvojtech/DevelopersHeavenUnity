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
        int value = int.Parse(currentBetInput.text);
        if (value < 0) value *= -1;
        GlobalVariables.CurrentSlotBet = value;
        currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
    }

    private void LowerBet()
    {
        switch (type)
        {
            case 0:
            {
                if (GlobalVariables.CurrentSlotBet - 10 < 0 || GlobalVariables.Money == 0 || GlobalVariables.Money - 10 < 0 )
                {
                    GlobalVariables.CurrentSlotBet = 0;
                    currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
                    return;
                }
                GlobalVariables.CurrentSlotBet -= 10;
                currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
                break;
            }
            case 1:
            {
                if (GlobalVariables.CurrentBlackJackBet - 10 < 0 || GlobalVariables.Money == 0 || GlobalVariables.Money - 10 < 0)
                {
                    GlobalVariables.CurrentBlackJackBet = 0;
                    currentBetInput.text = GlobalVariables.CurrentBlackJackBet.ToString();
                    return;
                }
                GlobalVariables.CurrentBlackJackBet -= 10;
                currentBetInput.text = GlobalVariables.CurrentBlackJackBet.ToString();
                break;
            }
        }
    }

    private void RiseBet()
    {
        switch (type)
        {
            case 0:
            {
                if  (GlobalVariables.Money == 0)
                {
                    GlobalVariables.CurrentSlotBet = 0;
                    currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
                    return;
                }
                if (GlobalVariables.CurrentSlotBet + 10 > GlobalVariables.Money) return;
                GlobalVariables.CurrentSlotBet += 10;
                currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
                break;
            }
            case 1:
            {
                if  (GlobalVariables.Money == 0)
                {
                    GlobalVariables.CurrentBlackJackBet = 0;
                    currentBetInput.text = GlobalVariables.CurrentBlackJackBet.ToString();
                    return;
                }
                if (GlobalVariables.CurrentBlackJackBet + 10 > GlobalVariables.Money) return;
                GlobalVariables.CurrentBlackJackBet += 10;
                currentBetInput.text = GlobalVariables.CurrentBlackJackBet.ToString();
                break;
            }
        }
    }

    private void MaxBet()
    {
        switch (type)
        {
            case 0:
            {
                if (GlobalVariables.Money == 0)
                {
                    GlobalVariables.CurrentSlotBet = 0;
                    currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
                    return;
                }
                GlobalVariables.CurrentSlotBet = GlobalVariables.Money;
                currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
                break;
            }
            case 1:
            {
                if (GlobalVariables.Money == 0)
                {
                    GlobalVariables.CurrentBlackJackBet = 0;
                    currentBetInput.text = GlobalVariables.CurrentBlackJackBet.ToString();
                    return;
                }
                GlobalVariables.CurrentBlackJackBet = GlobalVariables.Money;
                currentBetInput.text = GlobalVariables.CurrentBlackJackBet.ToString();
                break;
            }
        }
    }

    private void ResetBet()
    {
        switch (type)
        {
            case 0:
            {
                if  (GlobalVariables.Money == 0)
                {
                    GlobalVariables.CurrentSlotBet = 0;
                    currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
                    return;
                }
                GlobalVariables.CurrentSlotBet = 10;
                currentBetInput.text = GlobalVariables.CurrentSlotBet.ToString();
                break;
            }
            case 1:
            {
                if  (GlobalVariables.Money == 0)
                {
                    GlobalVariables.CurrentBlackJackBet = 0;
                    currentBetInput.text = GlobalVariables.CurrentBlackJackBet.ToString();
                    return;
                }
                GlobalVariables.CurrentBlackJackBet = 10;
                currentBetInput.text = GlobalVariables.CurrentBlackJackBet.ToString();
                break;
            }
        }
    }
}
