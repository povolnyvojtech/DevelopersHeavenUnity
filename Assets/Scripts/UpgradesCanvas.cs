using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradesCanvas : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private Vector2 _offset;
    
    public GameObject houseManagerDisplay;
    public GameObject skillTreeDisplay;
    private bool _currentDisplayType; //true - houseManager, false - skillTree
    private bool _buttonPressed;
    public Button houseManagerButton;
    private Image _houseManagerButtonImageComponent;
    public Button skillTreeButton;
    private Image _skillTreeButtonImageComponent;
    private readonly Color32 _activeColor = new(255, 255, 255, 255);
    private readonly Color32 _inactiveColor = new(145, 153, 185, 255);
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _houseManagerButtonImageComponent = houseManagerButton.GetComponent<Image>();
        _skillTreeButtonImageComponent = skillTreeButton.GetComponent<Image>();
        houseManagerButton.onClick.AddListener((() => RefreshUI(true)));
        skillTreeButton.onClick.AddListener((() => RefreshUI(false)));
        RefreshUI(true);
    }

    public void RefreshUI(bool type)
    {
        if (type == _currentDisplayType) return;
    
        _currentDisplayType = type;

        if (_currentDisplayType)
        {
            houseManagerDisplay.SetActive(true);
            skillTreeDisplay.SetActive(false);
            _houseManagerButtonImageComponent.color = _activeColor;
            _skillTreeButtonImageComponent.color = _inactiveColor;
        }
        else
        {
            houseManagerDisplay.SetActive(false);
            skillTreeDisplay.SetActive(true); 
            _houseManagerButtonImageComponent.color = _inactiveColor;
            _skillTreeButtonImageComponent.color = _activeColor;
        }
    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out var localMousePos
        );
        
        _offset = localMousePos - _rectTransform.anchoredPosition;
    }
}