using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SlotsManager : MonoBehaviour
{
    public GameObject hasWonImage;
    public TextMeshProUGUI hasWonText;
    public TextMeshProUGUI wonMoneyText;
    public Button spinButton;
    public Button raiseBetButton;
    public Button lowerBetButton;
    public Button maxBetButton;
    public GameObject[] images = new GameObject[3]; 
    public Sprite[] icons = new Sprite[7];
    /*
    0 = Banana
    1 = Cherry
    2 = Melon
    3 = Orange
    4 = Plum
    5 = Seven
    6 = Lemon 
    */
    
    private readonly List<int> _chosenIndexes = new List<int>() {-1,-1,-1};
    private readonly List<int> _spinCount = new List<int>() {10,14,18};

    private event Action FinishedSpinning;
    private int _finishedSlots;
    
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI moneyText;
    public GameObject notEnoughMoneyText;
    public TextMeshProUGUI betDisplayer;

    private string _typeOfWin;
    
    private void Start()
    {
        foreach (var image in images)
        {
            Image imgComponent = image.GetComponent<Image>();
            imgComponent.sprite = icons[Random.Range(0, icons.Length)];
        }

        FinishedSpinning += WinCheck;
    }

    public void Spin()
    {
        if (GlobalVariables.IsSpinning) return;
        if (GlobalVariables.Money < GlobalVariables.CurrentSlotBet || GlobalVariables.Money == 0)
        {
            betDisplayer.text = GlobalVariables.CurrentSlotBet.ToString();
            StartCoroutine(ShowNotEnoughMoneyText());
            return;
        }
        GlobalVariables.Money -= GlobalVariables.CurrentSlotBet;
        GlobalVariables.UpdateStats(levelText, xpText, moneyText);
        GlobalVariables.IsSpinning = true;
        
        for (int i = 0; i < images.Length; i++)
        {
            StartCoroutine(ChooseFruits(images[i], i));
        }
    }

    private IEnumerator ShowNotEnoughMoneyText()
    {
        notEnoughMoneyText.SetActive(true);
        yield return new WaitForSeconds(2f);
        notEnoughMoneyText.SetActive(false);
    }

    private IEnumerator ChooseFruits(GameObject slot, int currentImageIndex)
    {
        int chosenIndex = 0;
        Image imgComponent = slot.GetComponent<Image>();
        raiseBetButton.interactable = false;
        lowerBetButton.interactable = false;
        maxBetButton.interactable = false;
        spinButton.interactable = false;
        for (int i = 0; i < _spinCount[currentImageIndex]; i++)
        {
            chosenIndex = Random.Range(0, icons.Length); 
            imgComponent.sprite = icons[chosenIndex]; 
            yield return new WaitForSeconds(0.1f);
        }
        _chosenIndexes[currentImageIndex] = chosenIndex;
        FinishedSpinning?.Invoke();
    }

    private void WinCheck()
    {
        ++_finishedSlots;
        if (_finishedSlots != 3) return;
        raiseBetButton.interactable = true;
        lowerBetButton.interactable = true;
        maxBetButton.interactable = true;
        spinButton.interactable = true;
        _finishedSlots = 0;
        if (_chosenIndexes[0] == _chosenIndexes[1] && _chosenIndexes[1] == _chosenIndexes[2])
        {
            switch (_chosenIndexes[0])
            {
                case 0:
                {
                    hasWonText.text = "Big Win";
                    wonMoneyText.text = (10 * GlobalVariables.CurrentSlotBet).ToString();
                    break; //banana
                }
                case 1:
                {
                    hasWonText.text = "";
                    wonMoneyText.text = (GlobalVariables.Money += 2 * GlobalVariables.CurrentSlotBet).ToString();
                    break; //cherry
                }
                case 2:
                {
                    hasWonText.text = "Big Win";
                    wonMoneyText.text = (12 * GlobalVariables.CurrentSlotBet).ToString();
                    break; //melon
                }
                case 3:
                {
                    hasWonText.text = "Win";
                    wonMoneyText.text = (7 * GlobalVariables.CurrentSlotBet).ToString();
                    break; //orange
                }
                case 4:
                {
                    hasWonText.text = "Big Win";
                    wonMoneyText.text = (11 * GlobalVariables.CurrentSlotBet).ToString();
                    break; //plum
                }
                case 5:
                {
                    hasWonText.text = "MAX WIN!!!";
                    wonMoneyText.text = (20 * GlobalVariables.CurrentSlotBet).ToString();
                    break; //seven
                }
                case 6:
                {
                    hasWonText.text = "Win";
                    wonMoneyText.text = (6 * GlobalVariables.CurrentSlotBet).ToString();
                    break; //Lemon
                }
            }
            
            //číslo výhry - třešně, win - citron, pomeranč, big win - meloun, banán, švestky, max win - (6)7
            hasWonImage.SetActive(true);
            StartCoroutine(HideHasWonText());
            GlobalVariables.Money += 30*GlobalVariables.CurrentSlotBet;
            GlobalVariables.UpdateStats(levelText, xpText, moneyText);
        }
        GlobalVariables.IsSpinning = false;
    }

    private IEnumerator HideHasWonText()
    {
        spinButton.interactable = false;
        yield return new WaitForSeconds(2f);
        spinButton.interactable = true; 
        hasWonImage.SetActive(false);
    }
}