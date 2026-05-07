using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AssignToTimerManager : MonoBehaviour
{
    public int type;
    public Image electricityImage;
    public Image rentTimerImage;
    
    
    private void Start()
    {
        switch (type)
        {
            case 0:
            {
                TimerManagerScript.ElectricityImage = electricityImage;
                break;
            }
            case 1:
            {
                TimerManagerScript.RentImage = rentTimerImage;
                break;
            }
        }
    }

    private void Update()
    {
        switch (type)
        {
            case 0:
            {
                if (!electricityImage) return;
                if (GlobalVariables.CurrentElectricitySliderValue >= 200f)
                {
                    electricityImage.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 30);
                    break;
                }
                electricityImage.GetComponent<RectTransform>().sizeDelta = new Vector2(GlobalVariables.CurrentElectricitySliderValue, 30);
                break;
            }
            case 1:
            {
                if (!rentTimerImage) return;
                if (GlobalVariables.CurrentRentSliderValue >= 200f)
                {
                    rentTimerImage.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 30);    
                }
                rentTimerImage.GetComponent<RectTransform>().sizeDelta = new Vector2(GlobalVariables.CurrentRentSliderValue, 30);
                break;
            }
        }
    }
}
