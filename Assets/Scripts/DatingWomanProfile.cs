using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DatingWomanProfile : MonoBehaviour
{
    //toto všechno se provádí pouze na WomenProfile prefabu
    
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI ageText;
    public TextMeshProUGUI hobbiesText;
    public Image womanProfilePictureImage;
    public Texture2D[] profilePictures15To18;
    public Texture2D[] profilePictures19To24;
    public Texture2D[] profilePictures25To30;
    private Texture2D _myTexture;
    private WomanProfile _womanProfileData;
    public static event Action GenerateNewWomanProfileAction;
    public static event Action SetupInboxMessageAction;
    private readonly List<string> _inboxWomenMessages = new List<string>()
    {
        "Mám doma nový nůž a ještě jsem ho na nikom nezkoušela. Přijdeš mi ho ‚pokřtít‘?",
        "Chci tě svázat, nacpat ti hadici do krku a sledovat, jak se ti mění barva obličeje. Budeš můj nejoblíbenější experiment.",
        "Sejdeme se v lese za městem. Přines si lopatu, ať nemusím nosit dvě.",
        "Ráda sbírám trofeje. Tentokrát bych chtěla levou ledvinu. Přijdeš dobrovolně, nebo tě mám omámit?",
        "Přijď v černém. Nechci, aby bylo vidět krev, až tě budu řezat pomalu.",
        "Mám sklep, kde nikdo neslyší křičet. Chceš si vyzkoušet, jak dlouho vydržíš, než začneš prosit o smrt?",
        "Napiš mi svou krevní skupinu a jestli máš alergii na chloroform. Potřebuju vědět, jak tě nejlépe připravit.",
        "Chci tě rozřezat na kousky a uvařit si z tebe polévku. Ale nejdřív tě pomalu olíznu… všude.",
        "Sejdeme se u mě. Přines si zubní kartáček… ať máš co žvýkat, až ti vytrhám zuby kleštěmi.",
        "Jsem tvoje poslední rande. Po mně už nikdy nikoho nepotkáš. Přijdeš, nebo mám přijít já pro tebe?"
    }; 
    

    public void GenerateNextWomanProfile(bool way) //way - false - dislike, true - like
    {
        GlobalVariables.Way = way;
       
        GlobalVariables.CalculateChanceToGetGirls();
        if (way && GlobalVariables.ChanceToGetHoes >= 0 && UnityEngine.Random.Range(0,2) == 0 && GlobalVariables.InboxWomen.Count < 2)
        {
            WomanProfile previousWomanProfile = GlobalVariables.CurrentWomanProfile;
            GlobalVariables.InboxWomen.Add(new List<string> {previousWomanProfile.ProfileName, previousWomanProfile.Age.ToString(), _inboxWomenMessages[UnityEngine.Random.Range(0,_inboxWomenMessages.Count)]});
            SetupInboxMessageAction?.Invoke();
        }
        GenerateNewWomanProfileAction?.Invoke();
    }

    public void SetupDatingWomanProfile(WomanProfile womanProfile)
    {
        _womanProfileData = womanProfile;
        GlobalVariables.CurrentWomanProfile = womanProfile;
        RefreshUI();
    }
    
    private void RefreshUI()
    {
        if (_womanProfileData == null) return;
        _myTexture = _womanProfileData.Age switch
        {
            >= 15 and <= 18 => profilePictures15To18[Random.Range(0,12)],
            > 18 and <= 24 => profilePictures19To24[Random.Range(0,12)],
            > 24 and <= 30 => profilePictures25To30[Random.Range(0,12)],
            _ => _myTexture
        };
        Rect rect = new Rect(0, 0, _myTexture.width, _myTexture.height);
        Sprite profilePictureSprite = Sprite.Create(_myTexture, rect, new Vector2(0.5f, 0.5f));
        womanProfilePictureImage.sprite = profilePictureSprite;
        nameText.text = _womanProfileData.ProfileName;
        ageText.text = _womanProfileData.Age.ToString();
        hobbiesText.text = _womanProfileData.Hobbies;
    }
}
