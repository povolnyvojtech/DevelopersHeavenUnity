using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipDisplayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string buttonStatsText;
    public int buttonType; //gym, vzhled, invest money, rizz ..... TODO dodělat další vlastnosti
    public float value;
    private int _state; //0 - zamceny, 1 - nesplněný, 2 - hotový
    public bool firstButton;
    public bool lastButton;
    public TooltipDisplayer previousButton;
    public TooltipDisplayer nextButton;
    public Image currentButtonImage;
    public Sprite[] sprites;

    private void Start()
    {
        if (firstButton)
        {
            _state = 1;
        }
        ChangeSprite();
    }
    
    public void UnlockNext()
    {
        if (!previousButton || previousButton._state != 2) return;
        if (lastButton && _state != 2)
        {
            _state = 1;
            ChangeSprite();
            return;
        }
        _state = nextButton._state != 0 ? 2 : 1;
        ChangeSprite();
    }
    
    public void ChangeState()
    {
        if ((!previousButton || previousButton._state != 2) && !firstButton ) return;
        _state = Math.Clamp(++_state, 0, 2);
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        currentButtonImage.sprite = sprites[_state];
    }
    public void UpgradeSkill()
    {
        if (_state == 0) return;
        switch (buttonType)
        {
            case 0:
            {
                GlobalVariables.GymLevel += value;
                GlobalVariables.CalculateChanceToGetGirls();
                break;
            }
            case 1:
            {
                GlobalVariables.OverallLook += value;
                GlobalVariables.CalculateChanceToGetGirls();
                break;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_state == 0) return;
        TooltipManager.Instance.Show(buttonStatsText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_state == 0) return;
        TooltipManager.Instance.Hide();
    }
}
