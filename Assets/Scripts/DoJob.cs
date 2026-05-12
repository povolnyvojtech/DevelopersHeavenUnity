using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DoJob : MonoBehaviour
{
    public Image[] images;
    public Sprite[] sprites;
    private List<int> _directions = new List<int>();

    private void Start()
    {
        GenerateImages();
    }


    public void GenerateImages()
    {
        foreach (Image img in images)
        {
            img.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}
