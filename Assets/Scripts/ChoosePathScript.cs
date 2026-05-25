using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePathScript : MonoBehaviour
{
    public GameObject underscore;
    public GameObject choosePathCanvas;
    public GameObject gameDevCanvas;
    public GameObject webDevCanvas;
    public GameObject softwareEngineerCanvas;
    private int _stage; //0 - choose career, 1 - choose focus
    private int _focus; //0 - Game dev, 1 - web dev, 2 - software en. 
    
    private void Start()
    {
        StartCoroutine(UnderscoreBlinking());
    }

    private void Update()
    {
        switch (_stage)
        {
            case 0:
                HandlePathSelection();
                break;
            case 1:
                HandleSpecializationSelection();
                break;
        }
    }

    private void HandlePathSelection()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectPath(gameDevCanvas, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectPath(webDevCanvas, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectPath(softwareEngineerCanvas, 2);
        }
    }

    private void SelectPath(GameObject targetCanvas, int focusIndex)
    {
        choosePathCanvas.SetActive(false);
        targetCanvas.SetActive(true);
        _focus = focusIndex;
        _stage++;
    }

    private void HandleSpecializationSelection()
    {
        string selectedCareer = null;
        switch (_focus)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) selectedCareer = "GDgodot";
                else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) selectedCareer = "GDunity";
                else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) selectedCareer = "GDue";
                break;

            case 1:
                if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) selectedCareer = "WDfrontend";
                else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) selectedCareer = "WDbackend";
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) selectedCareer = "SEpython";
                else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) selectedCareer = "SEjava";
                break;
        }

        if (!string.IsNullOrEmpty(selectedCareer))
        {
            FinalizeCareerSelection(selectedCareer);
        }
    }

    private void FinalizeCareerSelection(string careerPath)
    {
        GlobalVariables.CareerPath = careerPath;
        GlobalVariables.HasCareer = true;
        SceneManager.LoadScene("Desktop");
    }

    private IEnumerator UnderscoreBlinking()
    {
        underscore.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        underscore.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(UnderscoreBlinking());
    }
}
