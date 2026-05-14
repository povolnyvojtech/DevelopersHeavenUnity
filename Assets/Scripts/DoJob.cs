using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DoJob : MonoBehaviour
{
    public GameObject imagePrefab;
    public Transform canvasTransform;
    public Sprite[] sprites;
    public GameObject doJobCanvas;
    public TextMeshProUGUI counterDisplayerText;
    private List<(GameObject, KeyCode, KeyCode)> _imagesOnCanvas = new();
    private readonly List<string> _keyStringsList = new(){"A", "S", "D"};
    private KeyCode _currentDirectionKeyCode;
    private int _currentDirection; //0 - left, 1 - down, 2 - up, 3 - right
    private KeyCode _currentKeyKeyCode;
    private string _currentKey;
    private int _currentCount;

    private void Start()
    {
        TimerManagerScript.JobFinished += () =>
        {
            doJobCanvas.SetActive(false);
            GlobalVariables.CurrentJobTime = 0;
            _currentCount = 0;
        };
        counterDisplayerText.text = _currentCount + "/" + GlobalVariables.NumberOfButtons;
        for (int i = 0; i < 2; i++)
        {
            Color buttonColor = (i == 0) ? new Color(0, 1, 0) : new Color(1, 0, 0);
            GameObject newImageObj = Instantiate(imagePrefab, canvasTransform);
            Image imageComponent = newImageObj.GetComponent<Image>();
            TextMeshProUGUI keyText = newImageObj?.GetComponentInChildren<TextMeshProUGUI>();
        
            Vector3 imgPos = new Vector3(Random.Range(-810, 810), Random.Range(-390,390), 1);
            _currentDirection = Random.Range(0, 4);
            _currentKey = _keyStringsList[Random.Range(0, 3)];
            keyText.text = _currentKey;
            DetermineKeys(_currentDirection, _currentKey);
            imageComponent.sprite = sprites[_currentDirection];
            imageComponent.color = buttonColor;
            newImageObj.transform.localPosition = imgPos;
            _imagesOnCanvas.Add((newImageObj, _currentDirectionKeyCode, _currentKeyKeyCode));
        }
    }

    private void Update()
    {
        if (Input.GetKey(_imagesOnCanvas[0].Item2) && Input.GetKey(_imagesOnCanvas[0].Item3))
        {
            Destroy(_imagesOnCanvas[0].Item1);
            _imagesOnCanvas.RemoveAt(0);
            GenerateNextImage();
            RefreshButtonColors();
            counterDisplayerText.text = _currentCount + "/" + GlobalVariables.NumberOfButtons;
        }
    }

    private void RefreshButtonColors()
    {
        for (int i = 0; i < _imagesOnCanvas.Count; i++)
        {
            _imagesOnCanvas[i].Item1.GetComponent<Image>().color = (i == 0) ? new Color(0,1,0) : new Color(1,0,0);
        }
    }
    private static void DetermineNumberOfButtons()
    {
        if (10 <= GlobalVariables.CurrentJobTime && GlobalVariables.CurrentJobTime <= 20)
        {
            GlobalVariables.NumberOfButtons = 10;
        }
        else if (30 <= GlobalVariables.CurrentJobTime && GlobalVariables.CurrentJobTime <= 40)
        {
            GlobalVariables.NumberOfButtons = 15;
        }
        else if (50 <= GlobalVariables.CurrentJobTime && GlobalVariables.CurrentJobTime <= 60)
        {
            GlobalVariables.NumberOfButtons = 20;
        }
        else if (70 <= GlobalVariables.CurrentJobTime && GlobalVariables.CurrentJobTime <= 80)
        {
            GlobalVariables.NumberOfButtons = 25;
        }
        else if (90 <= GlobalVariables.CurrentJobTime && GlobalVariables.CurrentJobTime <= 100)
        {
            GlobalVariables.NumberOfButtons = 30;
        }
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
        if (_currentCount > GlobalVariables.NumberOfButtons)
        {
            TimerManagerScript.FinishJob();
            doJobCanvas.SetActive(false);
            GlobalVariables.CurrentJobTimerSliderValue = 0;
            _currentCount = 0;
            return;
        }

        Debug.Log(GlobalVariables.NumberOfButtons);
        Debug.Log(_currentCount);
        GameObject newImageObj = Instantiate(imagePrefab, canvasTransform);
        Image imageComponent = newImageObj.GetComponent<Image>();
        TextMeshProUGUI keyText = newImageObj.GetComponentInChildren<TextMeshProUGUI>();
        
        Vector3 imgPos = new Vector3(Random.Range(-810, 810), Random.Range(-390,390), 1);
        _currentDirection = Random.Range(0, 4);
        _currentKey = _keyStringsList[Random.Range(0, 3)];
        keyText.text = _currentKey;
        DetermineKeys(_currentDirection, _currentKey);
        imageComponent.sprite = sprites[_currentDirection];
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
