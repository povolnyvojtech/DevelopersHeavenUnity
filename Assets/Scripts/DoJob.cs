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
    private readonly List<string> _keyStringsList = new(){"A", "B", "C"};
    private int _currentDirection; //0 - left, 1 - down, 2 - up, 3 - right
    private string _currentKey;

    private void Start()
    {
        GenerateImages();
    }


    private void GenerateImages()
    {
        GameObject newImageObj = Instantiate(imagePrefab, canvasTransform);
        Image imageComponent = newImageObj.GetComponent<Image>();
        
        Vector3 imgPos = new Vector3(Random.Range(-810, 810), Random.Range(-390,390), 1);
        _currentDirection = Random.Range(0, 4);
        _currentKey = _keyStringsList[Random.Range(0, 3)];
        
        imageComponent.sprite = sprites[_currentDirection];
        newImageObj.transform.localPosition = imgPos;
        
        StartCoroutine(DestroyImage(newImageObj));
        Debug.Log(imgPos);
    }
    
    private static IEnumerator DestroyImage(GameObject img)
    {
        yield return new WaitForSeconds(2f);
        if (img)
        {
            Destroy(img.gameObject);
            Debug.Log("Job failed");
        }
    }
}
