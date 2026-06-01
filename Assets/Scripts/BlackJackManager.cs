using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BlackJackManager : MonoBehaviour
{
    public TextMeshProUGUI playerNumber1;
    public TextMeshProUGUI playerNumber2;
    public TextMeshProUGUI dealerNumber1;
    public TextMeshProUGUI dealerNumber2;
    public TextMeshProUGUI playerCountDisplayer;
    public TextMeshProUGUI playerDealtCardsDisplayer;
    public TextMeshProUGUI dealerCountDisplayer;
    public TextMeshProUGUI dealerDealtCardsDisplayer;
    public GameObject hasWonImage;
    public Button hitButton;
    public Button stayButton;
    public Button startGameButton;
    public TextMeshProUGUI hasWonText;
    public TextMeshProUGUI wonMoneyText;
    public GameObject notEnoughMoneyText;
    public TextMeshProUGUI globalMoneyText;
    private int _playerCounter;
    private int _dealerCounter;
    private bool _isStaying;
    private string _dealtCardsString;
    private readonly Dictionary<string, int> _cardsValue = new Dictionary<string, int>()
    {
        {"2", 2}, {"3", 3}, {"4", 4}, {"5", 5},
        {"6", 6}, {"7", 7}, {"8", 8}, {"9", 9}, {"10", 10},
        {"Jack", 11}, {"Queen", 12}, {"King", 13},
        {"Ace", 14}
    };
    private List<int> _playerDealtCards = new List<int>();
    private List<int> _dealerDealtCards = new List<int>();
    private bool _isShowingNotEnoughMoney = false;

    private void OnEnable()
    {
        _playerDealtCards.Clear();
        _dealerDealtCards.Clear();
        _playerCounter = 0;
        _dealerCounter = 0;
        playerDealtCardsDisplayer.text = "";
        dealerDealtCardsDisplayer.text = "";
        playerCountDisplayer.text = "0";
        dealerCountDisplayer.text = "0";
        playerNumber1.text = "6";
        playerNumber2.text = "6";
        dealerNumber1.text = "7";
        dealerNumber2.text = "7";
        startGameButton.gameObject.SetActive(true);
        hitButton.gameObject.SetActive(false);
        stayButton.gameObject.SetActive(false);
    }

    private void PrepareForNewRound()
    {
        _playerDealtCards.Clear();
        _dealerDealtCards.Clear();
        playerDealtCardsDisplayer.text = "";
        dealerDealtCardsDisplayer.text = "";
        playerCountDisplayer.text = "0";
        dealerCountDisplayer.text = "0";
        startGameButton.gameObject.SetActive(true);
        hitButton.gameObject.SetActive(false);
        stayButton.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        if (GlobalVariables.CurrentBlackJackBet > GlobalVariables.Money)
        {
            notEnoughMoneyText.SetActive(true);
            if (_isShowingNotEnoughMoney) return;
            StartCoroutine(HideText(false));
            return;
        }

        GlobalVariables.Money -= GlobalVariables.CurrentBlackJackBet;   
        globalMoneyText.text = GlobalVariables.Money.ToString();
        startGameButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        stayButton.gameObject.SetActive(true);
        StartCoroutine(DealCards());
    }

    private IEnumerator DealCards()
    {
        yield return new WaitForSeconds(1f);
        hitButton.interactable = false;
        stayButton.interactable = false;
        
        List<string> cardNames = _cardsValue.Keys.ToList();
        
        //deal to player
        for (int i = 0; i < 2; i++)
        {
            int value = _cardsValue[cardNames[Random.Range(0, cardNames.Count)]];
            string valueOnCard = "";
        
            if (value > 10)
            {
                switch (value)
                {
                    case 11: valueOnCard = "J"; _playerCounter += 10; _playerDealtCards.Add(10); break;
                    case 12: valueOnCard = "Q"; _playerCounter += 10; _playerDealtCards.Add(10); break;
                    case 13: valueOnCard = "K"; _playerCounter += 10; _playerDealtCards.Add(10); break;
                    case 14: valueOnCard = "A"; _playerCounter += 10; _playerDealtCards.Add(10); break;
                }
            }
            else
            {
                _playerCounter += value;
                _playerDealtCards.Add(value);
                valueOnCard = value.ToString();
            }
            playerNumber1.text = valueOnCard;
            playerNumber2.text = valueOnCard;
            PrintCards(true, false);
            yield return new WaitForSeconds(1f);

        }
        if (_playerCounter > 21)
        {
            Check();
            yield break;
        }
    
        //deal to dealer
        for (int i = 0; i < 2; i++)
        {
            int value = _cardsValue[cardNames[Random.Range(0, cardNames.Count)]];
            string valueOnCard = "";
        
            if (value > 10)
            {
                switch (value)
                {
                    case 11: valueOnCard = "J"; _dealerCounter += 10; _dealerDealtCards.Add(10); break;
                    case 12: valueOnCard = "Q"; _dealerCounter += 10; _dealerDealtCards.Add(10); break;
                    case 13: valueOnCard = "K"; _dealerCounter += 10; _dealerDealtCards.Add(10); break;
                    case 14: valueOnCard = "A"; _dealerCounter += 10; _dealerDealtCards.Add(10); break;
                }
            }
            else
            {
                _dealerCounter += value;
                valueOnCard = value.ToString();
                _dealerDealtCards.Add(value);
            }

            dealerNumber1.text = valueOnCard;
            dealerNumber2.text = valueOnCard;
            PrintCards(false, true);
            
            yield return new WaitForSeconds(1f);
        }
        if (_dealerCounter > 21)
        {
            PrintCards(false, false);
            Check();
            yield break;
        }

        hitButton.interactable = true;
        stayButton.interactable = true;
        
    }

    public void Hit()
    {
        hitButton.interactable = false;
        stayButton.interactable = false;
        List<string> cardNames = _cardsValue.Keys.ToList();
        int value = _cardsValue[cardNames[Random.Range(0, cardNames.Count)]];
        string finalValue = "";
        
        if (value > 10)
        {
            switch (value)
            {
                case 11: finalValue = "J"; _playerCounter += 10; _playerDealtCards.Add(10); break;
                case 12: finalValue = "Q"; _playerCounter += 10; _playerDealtCards.Add(10); break;
                case 13: finalValue = "K"; _playerCounter += 10; _playerDealtCards.Add(10); break;
                case 14: finalValue = "A"; _playerCounter += 10; _playerDealtCards.Add(10); break;
            }
        }
        else
        {
            _playerCounter += value;
            _playerDealtCards.Add(value);
            finalValue = value.ToString();
        }
        
        PrintCards(true, false);
        playerNumber1.text = finalValue;
        playerNumber2.text = finalValue;
        playerCountDisplayer.text = _playerCounter.ToString();
        hitButton.interactable = true;
        stayButton.interactable = true;
        if (_playerCounter > 21)
        {
            Check();
        }
    }

    public void Stay()
    {
        _isStaying = true;
        while(_dealerCounter <= 16)
        {
            List<string> cardNames = _cardsValue.Keys.ToList();
            int value = _cardsValue[cardNames[Random.Range(0, cardNames.Count)]];
            _dealerCounter += value;
            _dealerDealtCards.Add(value);
            PrintCards(false, false);
            dealerNumber1.text = value.ToString();
            dealerNumber2.text = value.ToString();
        }
        PrintCards(false, false);
        Check();
        _isStaying = false;
    }
    
    private IEnumerator HideText(bool type)
    {
        switch (type)
        {
            case true:
            {
                yield return new WaitForSeconds(1f);
                hasWonImage.SetActive(true);
                yield return new WaitForSeconds(2f);
                hasWonImage.SetActive(false);
                PrepareForNewRound();
                break;
            }
            case false:
            {
                _isShowingNotEnoughMoney = true;
                yield return new WaitForSeconds(2f);
                notEnoughMoneyText.SetActive(false);
                _isShowingNotEnoughMoney = false;
                PrepareForNewRound();
                break;
            }
        }
    }

    private void PrintCards(bool type, bool dealerType) //true - player, false - dealer -> true - otazniky, false - everything
    {
        switch (type)
        {
            case true:
            {
                for (int j = 0; j < _playerDealtCards.Count; j++)
                {
                    if (j == 0)
                    {
                        _dealtCardsString += _playerDealtCards[j].ToString();
                    }
                    else
                    {
                        _dealtCardsString += " + " + _playerDealtCards[j];
                    }
                }

                playerCountDisplayer.text = _playerCounter.ToString();
                playerDealtCardsDisplayer.text = _dealtCardsString;
                _dealtCardsString = "";
                break;
            }
            case false:
            {
                switch (dealerType)
                {
                    case true:
                    {
                        for (int j = 0; j < _dealerDealtCards.Count; j++)
                        {
                            if (j == 0)
                            {
                                _dealtCardsString += _dealerDealtCards[j].ToString();
                            }
                            else
                            {
                                dealerNumber1.text = "?";
                                dealerNumber2.text = "?";
                                _dealtCardsString += " + ?";
                            }
                        }
                        dealerCountDisplayer.text = _dealerDealtCards[0].ToString();
                        dealerDealtCardsDisplayer.text = _dealtCardsString;
                        _dealtCardsString = "";
                        break;
                    }
                    case false:
                    {
                        for (int i = 0; i < _dealerDealtCards.Count; i++)
                        {
                            if (i == 0)
                            {
                                _dealtCardsString += _dealerDealtCards[i].ToString();
                            }
                            else
                            {
                                _dealtCardsString += " + " + _dealerDealtCards[i];
                            }
                            dealerCountDisplayer.text = _dealerCounter.ToString();
                            dealerDealtCardsDisplayer.text = _dealtCardsString;
                        }
                        _dealtCardsString = "";
                        break;
                    }
                }
                break;
            }
        }
    }

    private void Check() 
    {
        if (_playerCounter > 21)
        {
            
            HandleGameEnd("You lost", 0);
            return;
        }

        if (_dealerCounter > 21)
        {
            HandleGameEnd("You win", GlobalVariables.CurrentBlackJackBet * 2);
            return;
        }

        if (_playerCounter == 21 || _dealerCounter == 21)
        {
            if (_playerCounter == 21 && _dealerCounter == 21)
            {
                HandleGameEnd("Draw", GlobalVariables.CurrentBlackJackBet);
            }
            else if (_playerCounter == 21)
            {
                HandleGameEnd("You win", GlobalVariables.CurrentBlackJackBet * 2);
            }
            else
            {
                HandleGameEnd("You lost", 0);
            }
            return;
        }

        if (_isStaying)
        {
            if (_playerCounter > _dealerCounter)
            {
                HandleGameEnd("You win", GlobalVariables.CurrentBlackJackBet * 2);
            }
            else if (_dealerCounter > _playerCounter)
            {
                HandleGameEnd("You lost", 0);
            }
            else
            {
                HandleGameEnd("Draw", GlobalVariables.CurrentBlackJackBet);
            }
        }
    }

    private void HandleGameEnd(string resultText, int wonMoney)
    {
        hasWonText.text = resultText;
        
        if (wonMoney > 0)
        {
            wonMoneyText.text = wonMoney.ToString();
            GlobalVariables.Money += wonMoney;
            globalMoneyText.text = GlobalVariables.Money.ToString();
        }
        else
        {
            wonMoneyText.text = "";
        }

        _playerCounter = 0;
        _dealerCounter = 0;
        StartCoroutine(HideText(true));
    }
}
