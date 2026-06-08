using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DatingAppMeet : MonoBehaviour
{
    private List<string> _randomWomanProfileInfo;
    public Transform contentParent;
    public GameObject womanProfilePrefab;
    private bool _generatedWomanProfile;
    public GameObject panels;
    private WomanProfile _currentWomanProfile;
    public Transform inboxContent;
    public GameObject messagePrefab;
    private readonly List<string> _names = new List<string>()
    {
        "Emma", "Olivia", "Sophia", "Ava", "Isabella", "Mia", "Amelia", "Harper",
        "Evelyn", "Abigail", "Emily", "Elizabeth", "Mila", "Ella", "Avery"
    };
    private readonly List<string> _hobbies = new List<string>()
    {
        "Traveling", "Reading", "Sports", "Painting", "Photography", "Cooking", "Gardening", "Gaming",
        "Music", "Hiking", "Dancing", "Watching movies", "Sewing", "Yoga", "Swimming",
    };
    
    
    
    private void OnEnable()
    {
        if (!GlobalVariables.DatingHasRegistered) { panels.SetActive(false); return; }
        panels.SetActive(true);
        if (_generatedWomanProfile) return;
        GenerateWomenProfile();
        _generatedWomanProfile = true;
        DatingWomanProfile.GenerateNewWomanProfileAction += GenerateWomenProfile;
        DatingWomanProfile.SetupInboxMessageAction += InitiateInboxMessages;
    }

    private void InitiateInboxMessages()
    {
        ClearInbox();
        foreach (List<string> womanProfileMessage in GlobalVariables.InboxWomen)
        {
            GameObject newInboxMessage = Instantiate(messagePrefab, inboxContent);
            newInboxMessage.GetComponent<SetupInboxMessage>().SetupMessage(womanProfileMessage);   
        }

        StartCoroutine(TimerManagerScript.InboxMessageDestroyTimer(inboxContent));
    }

    private void ClearInbox()
    {
        foreach (Transform child in inboxContent)
        {
            Destroy(child.gameObject);
        }
    }
    
    private static IEnumerator SlideAndDestroy(GameObject profileToDestroy)
    {
        GameObject objectToAnimate = profileToDestroy;
        if (!objectToAnimate) yield break;

        RectTransform rt = objectToAnimate.GetComponent<RectTransform>();
    
        float offset = GlobalVariables.Way ? rt.rect.width : -rt.rect.width;
        Vector2 targetPos = new Vector2(rt.anchoredPosition.x + offset, rt.anchoredPosition.y);
    
        float speed = 1000f;

        while (rt && rt.anchoredPosition != targetPos)
        {
            rt.anchoredPosition = Vector2.MoveTowards(rt.anchoredPosition, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        if (objectToAnimate) 
        {
            Destroy(objectToAnimate);
        }
    }

    private void GenerateWomenProfile()
    {
        GameObject objectToAnimate = GlobalVariables.PreviousWomanProfile;
        if (_generatedWomanProfile && objectToAnimate)
        {
            StartCoroutine(SlideAndDestroy(objectToAnimate));
        }
        string womanName = _names[Random.Range(0, 15)];
        string age = Random.Range(15, 30).ToString();
        string hobbies = "";
        
        for (int i = 0; i < 5; i++)
        {
            if (i == 0)
            {
                hobbies += _hobbies[Random.Range(0, 15)];
            }
            else
            {
                hobbies += ", " + _hobbies[Random.Range(0, 15)];
            }
        }
        _currentWomanProfile = new WomanProfile(womanName, age, hobbies);
        InitiateWomenProfiles();
    }
    
    private void InitiateWomenProfiles()
    {
        GameObject newWomanProfile = Instantiate(womanProfilePrefab, contentParent);
        newWomanProfile.transform.SetAsFirstSibling();
        GlobalVariables.PreviousWomanProfile = newWomanProfile;
        newWomanProfile.GetComponent<DatingWomanProfile>().SetupDatingWomanProfile(_currentWomanProfile);
    }
}
