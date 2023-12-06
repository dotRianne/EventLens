
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    InteractionScript interactionScript;
    public GameObject menu;
    public GameObject HUD;
    public GameObject map;
    public GameObject exitMenu;
    public GameObject interactions;
    public GameObject informations;
    public TMP_Text interactionsText;
    public TMP_Text informationsText;

    public void OpenMenu()
    {
        interactionScript.canSendRaycasts = false;
        ResetEverything();
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        interactionScript.canSendRaycasts = true;
        ResetEverything();
        HUD.SetActive(true);
    }

    public void ExitMenu()
    {
        ResetEverything();
        exitMenu.SetActive(true);
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
        HUD.SetActive(true);
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
        HUD.SetActive(true);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

    public void ResetEverything()
    {
        map.SetActive(false);
        informations.SetActive(false);
        menu.SetActive(false);
        HUD.SetActive(false);
        exitMenu.SetActive(false);
        interactions.SetActive(false);
    }    
}
