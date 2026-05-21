using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DoJob : MonoBehaviour
{
    public GameObject imagePrefab;
    public Transform arrowsFieldTransform;
    public Sprite[] greenArrows;
    public GameObject doJobCanvas;
    public TextMeshProUGUI counterDisplayerText;
    private List<(GameObject, KeyCode, KeyCode)> _imagesOnCanvas = new();
    private readonly List<string> _keyStringsList = new(){"A", "S", "D"};
    private readonly KeyCode[] _letterKey = {KeyCode.A, KeyCode.S, KeyCode.D};
    private readonly KeyCode[] _arrowKeys = {KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow};
    private KeyCode _currentDirectionKeyCode;
    private int _currentDirection; //0 - left, 1 - down, 2 - up, 3 - right
    private KeyCode _currentKeyKeyCode;
    private string _currentKey;
    private int _currentCount;
    private KeyCode[] _lastCombo = {};

    private void OnEnable()
    {
        TimerManagerScript.JobFinished += EndJob;
        counterDisplayerText.text = _currentCount + "/" + GlobalVariables.NumberOfButtons;
        for (int i = 0; i < 2; i++)
        {
            Color buttonColor = i == 0 ? new Color(0,1,0) :  new Color(1,0,0);
            GameObject newImageObj = Instantiate(imagePrefab, arrowsFieldTransform);
            Image imageComponent = newImageObj.GetComponent<Image>();
            TextMeshProUGUI keyText = newImageObj?.GetComponentInChildren<TextMeshProUGUI>();
        
            Vector3 imgPos = new Vector3(Random.Range(-810, 810), Random.Range(-390,390), 1);
            int randomDirection = Random.Range(0, 4);
            string randomKey = _keyStringsList[Random.Range(0, 3)];
            while (randomDirection == _currentDirection)
            {
                randomDirection = Random.Range(0, 4);
            }
            while (randomKey == _currentKey)
            {
                randomKey = _keyStringsList[Random.Range(0, 3)];
            }
            _currentDirection = randomDirection;
            _currentKey = randomKey;
            keyText.text = _currentKey;
            DetermineKeys(_currentDirection, _currentKey);
            imageComponent.sprite = greenArrows[_currentDirection];
            imageComponent.color = buttonColor;
            newImageObj.transform.localPosition = imgPos;
            _imagesOnCanvas.Add((newImageObj, _currentDirectionKeyCode, _currentKeyKeyCode));
        }
        _lastCombo = new []{_imagesOnCanvas[0].Item2, _imagesOnCanvas[0].Item3};
    }

    private void OnDisable()
    {
        TimerManagerScript.JobFinished -= EndJob;
    }

    private void EndJob()
    {
        doJobCanvas.SetActive(false);
        GlobalVariables.CurrentJobTime = 0;
        _currentCount = 0;
    }

    private void Update()
    {
        if (_imagesOnCanvas.Count == 0) return;

        KeyCode correctKey1 = _imagesOnCanvas[0].Item2;
        KeyCode correctKey3 = _imagesOnCanvas[0].Item3;

        bool keyJustPressed = false;

        KeyCode pressedArrow = KeyCode.None;
        foreach (KeyCode arrow in _arrowKeys)
        {
            if (Input.GetKey(arrow))
            {
                pressedArrow = arrow;
                if (Input.GetKeyDown(arrow)) keyJustPressed = true;
                break;
            }
        }

        KeyCode pressedLetter = KeyCode.None;
        foreach (KeyCode letter in _letterKey)
        {
            if (Input.GetKey(letter))
            {
                pressedLetter = letter;
                if (Input.GetKeyDown(letter)) keyJustPressed = true;
                break;
            }
        }
        
        if (pressedArrow != KeyCode.None && pressedLetter != KeyCode.None && keyJustPressed)
        {
            if (pressedArrow == correctKey1 && pressedLetter == correctKey3)
            {
                Destroy(_imagesOnCanvas[0].Item1);
                _imagesOnCanvas.RemoveAt(0);

                GenerateNextImage();
                RefreshButtonColors();
                counterDisplayerText.text = _currentCount + "/" + GlobalVariables.NumberOfButtons;
            }
            else
            {
                DetermineTimePenalty();
            }
        }
    }

    private void RefreshButtonColors()
    {
        for (int i = 0; i < _imagesOnCanvas.Count; i++)
        {
            _imagesOnCanvas[i].Item1.GetComponent<Image>().color = (i == 0) ? new Color(0,1,0) : new Color(1,0,0);
        }
    }

    private static void DetermineTimePenalty()
    {
        GlobalVariables.CurrentJobTimerSliderValue += GlobalVariables.NumberOfButtons switch
        {
            10 => 1,
            15 => 2,
            20 => 3,
            25 => 4,
            30 => 5,
            35 => 6,
            40 => 7,
            45 => 8,
            50 => 9,
            _ => 0
        };
    }
    private static void DetermineNumberOfButtons()
    {
        Debug.Log(GlobalVariables.CurrentJobTime);
        GlobalVariables.NumberOfButtons = GlobalVariables.CurrentJobTime switch
        {
            >= 10 and <= 20 => 10,
            > 20 and <= 30 => 15,
            > 30 and <= 40 => 20,
            > 40 and <= 50 => 25,
            > 50 and <= 60 => 30,
            > 60 and <= 70 => 35,
            > 70 and <= 80 => 40,
            > 80 and <= 90 => 45,
            > 90 and <= 100 => 50,
            _ => 100
        };
    }
    
    public static void StartContract(int jobTime, int jobMoney, int jobXp)
    {
        if (GlobalVariables.HasJob) return;
        GlobalVariables.HasJob = true;
        GlobalVariables.CurrentJobMoney = jobMoney;
        GlobalVariables.CurrentJobTime = jobTime;
        DetermineNumberOfButtons();
        GlobalVariables.CurrentJobXp = jobXp;
    }


    private void GenerateNextImage()
    {
        if (_currentCount == GlobalVariables.NumberOfButtons-1)
        {
            TimerManagerScript.FinishJob(true);
            doJobCanvas.SetActive(false);
            GlobalVariables.CurrentJobTimerSliderValue = 0;
            _currentCount = 0;
            foreach (var img in _imagesOnCanvas)
            {
                Destroy(img.Item1);
            }
            _imagesOnCanvas.Clear();
            return;
        }
        GameObject newImageObj = Instantiate(imagePrefab, arrowsFieldTransform);
        Image imageComponent = newImageObj.GetComponent<Image>();
        TextMeshProUGUI keyText = newImageObj.GetComponentInChildren<TextMeshProUGUI>();

        Vector3 lastPos = _imagesOnCanvas[^1].Item1.GetComponent<RectTransform>().anchoredPosition;;
        int offset = 250;
        int x = Random.Range(-810, 810);
        int y = Random.Range(-390, 390);
        while (x >= (lastPos.x - offset) && x <= (lastPos.x + offset) && y >= (lastPos.y - offset) && y <= (lastPos.y + offset))
        {
            x = Random.Range(-810, 810);
            y = Random.Range(-390, 390);
        }
        Debug.Log("Last pos: " +  lastPos + " new pos: " +  x + ", " + y);

        var imgPos = new Vector3(x, y);
        int randomDirection = Random.Range(0, 4);
        string randomKey = _keyStringsList[Random.Range(0, 3)];
        while (randomDirection == _currentDirection)
        {
            randomDirection = Random.Range(0, 4);
        }
        while (randomKey == _currentKey)
        {
            randomKey = _keyStringsList[Random.Range(0, 3)];
        }
        _currentDirection = randomDirection;
        _currentKey = randomKey;
        keyText.text = _currentKey;
        DetermineKeys(_currentDirection, _currentKey);
        imageComponent.sprite = greenArrows[_currentDirection];
        newImageObj.transform.localPosition = imgPos;
        _imagesOnCanvas.Add((newImageObj, _currentDirectionKeyCode, _currentKeyKeyCode));
        _currentCount++;
    }

    private void DetermineKeys(int direction, string key)
    {
        _currentDirectionKeyCode = direction switch
        {
            0 => KeyCode.LeftArrow,
            1 => KeyCode.DownArrow,
            2 => KeyCode.UpArrow,
            3 => KeyCode.RightArrow,
            _ => _currentDirectionKeyCode
        };

        _currentKeyKeyCode = key switch
        {
            "A" => KeyCode.A,
            "S" => KeyCode.S,
            "D" => KeyCode.D,
            _ => _currentKeyKeyCode
        };
    }
}
