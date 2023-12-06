using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    public InteractionScript interactionScript;
    public GameObject FAQ;
    public GameObject map;
    public GameObject menu;
    public GameObject info;
    public GameObject interactions;
    public GameObject informations;
    public TMP_Text interactionsText;
    public TMP_Text informationsText;

    private bool FAQ_on = false;
    private bool Info_on = false;
    private bool map_on = false;

    public GameObject intakeButton;
    public GameObject[] intakeText;
    public GameObject intakeBottom;
    public int intakeTextCount;

    public GameObject FAQ_StudyAssociation;
    public GameObject FAQ_Intake;
    public GameObject FAQ_English;
    public GameObject FAQ_Size;
    public GameObject FAQ_Structure;
    public GameObject FAQ_Curriculum;

    private void Awake()
    {
        interactionScript.canSendRaycasts = false;
    }
    public void FAQ_Toggle()
    {
        if(!FAQ_on)
        {
            interactionScript.canSendRaycasts = false;
            FAQ.SetActive(true);
            FAQ_on = true;
        }
        else if (FAQ_on)
        {
            interactionScript.canSendRaycasts = true;
            FAQ.SetActive(false);
            FAQ_on = false;
        }
    }
    public void Map_Toggle()
    {
        if (!map_on)
        {
            interactionScript.canSendRaycasts = false;
            map.SetActive(true);
            map_on = true;
        }
        else if (map_on)
        {
            interactionScript.canSendRaycasts = true;
            map.SetActive(false);
            map_on = false;
        }
    }
    public void Info_Toggle()
    {
        if (!Info_on)
        {
            interactionScript.canSendRaycasts = false;
            info.SetActive(true);
            Info_on = true;
        }
        else if (Info_on)
        {
            interactionScript.canSendRaycasts = true;
            info.SetActive(false);
            Info_on = false;
        }
    }
    public void Intake_Open()
    {
        ResetEverything();
        FAQ_Intake.SetActive(true);
        interactionScript.canSendRaycasts = false;
        intakeText[intakeTextCount].SetActive(true);
        intakeBottom.SetActive(true);
        intakeButton.SetActive(false);
    }
    public void Intake_Continue()
    {
        if (intakeTextCount < intakeText.Length -1)
        {
            intakeText[intakeTextCount].SetActive(false);
            intakeTextCount += 1;
            intakeText[intakeTextCount].SetActive(true);
        }
    }
    public void Intake_Back()
    {
        if (intakeTextCount > 0)
        {
            intakeText[intakeTextCount].SetActive(false);
            intakeTextCount -= 1;
            intakeText[intakeTextCount].SetActive(true);
        }
    }
    public void Intake_Close()
    {
        intakeText[intakeTextCount].SetActive(false);
        interactionScript.canSendRaycasts = true;
        intakeTextCount = 0;
        intakeBottom.SetActive(false);
        intakeButton.SetActive(true);
        FAQ_Curriculum.SetActive(true);
        FAQ_English.SetActive(true);
        FAQ_Size.SetActive(true);
        FAQ_Structure.SetActive(true);
        FAQ_StudyAssociation.SetActive(true);
    }


    public void OpenInteractions(string text)
    {
        interactionScript.canSendRaycasts = false;
        ResetEverything();
        interactions.SetActive(true);
        interactionsText.SetText(text);
    }
    public void CloseInteractions()
    {
        interactionScript.canSendRaycasts = true;
        ResetEverything();
        menu.SetActive(true);
    }
    public void OpenInformations(string text)
    {
        interactionScript.canSendRaycasts = false;
        ResetEverything();
        informations.SetActive(true);
        informationsText.SetText(text);
    }
    public void CloseInformations()
    {
        interactionScript.canSendRaycasts = true;
        ResetEverything();
        menu.SetActive(true);
    }
    public void CloseApplication()
    {
        Application.Quit();
    }
    public void ResetEverything()
    {
        map.SetActive(false);
        informations.SetActive(false);
        interactions.SetActive(false);

        FAQ_Curriculum.SetActive(false);
        FAQ_English.SetActive(false);
        FAQ_Intake.SetActive(false);
        FAQ_Size.SetActive(false);
        FAQ_Structure.SetActive(false);
        FAQ_StudyAssociation.SetActive(false);
    }
}
