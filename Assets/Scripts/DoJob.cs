using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DoJob : MonoBehaviour
{
    public GameObject imagePrefab;
    public Transform canvasTransform;
    public Sprite[] sprites;
    private List<(GameObject, KeyCode, KeyCode)> _imagesOnCanvas = new();
    private readonly List<string> _keyStringsList = new(){"A", "S", "D"};
    private KeyCode _currentDirectionKeyCode;
    private int _currentDirection; //0 - left, 1 - down, 2 - up, 3 - right
    private KeyCode _currentKeyKeyCode;
    private string _currentKey;

    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            Color buttonColor = (i == 0) ? new Color(0, 1, 0) : new Color(1, 0, 0);
            GameObject newImageObj = Instantiate(imagePrefab, canvasTransform);
            Image imageComponent = newImageObj.GetComponent<Image>();
        
            Vector3 imgPos = new Vector3(Random.Range(-810, 810), Random.Range(-390,390), 1);
            _currentDirection = Random.Range(0, 4);
            _currentKey = _keyStringsList[Random.Range(0, 3)];
            DetermineKeys(_currentDirection, _currentKey);
            imageComponent.sprite = sprites[_currentDirection];
            imageComponent.color = buttonColor;
            newImageObj.transform.localPosition = imgPos;
            _imagesOnCanvas.Add((newImageObj, _currentDirectionKeyCode, _currentKeyKeyCode));
            Debug.Log("Direction: " + _imagesOnCanvas[i].Item2 + ", key:" + _imagesOnCanvas[i].Item3);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(_imagesOnCanvas[0].Item2) && Input.GetKeyDown(_imagesOnCanvas[0].Item3))
        {
            Destroy(_imagesOnCanvas[0].Item1);
        }
    }


    private void GenerateImages()
    {
        GameObject newImageObj = Instantiate(imagePrefab, canvasTransform);
        Image imageComponent = newImageObj.GetComponent<Image>();
        
        Vector3 imgPos = new Vector3(Random.Range(-810, 810), Random.Range(-390,390), 1);
        _currentDirection = Random.Range(0, 4);
        _currentKey = _keyStringsList[Random.Range(0, 3)];
        DetermineKeys(_currentDirection, _currentKey);
        imageComponent.sprite = sprites[_currentDirection];
        newImageObj.transform.localPosition = imgPos;
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
