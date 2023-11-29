
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;

public class Buttons : MonoBehaviour
{
    InteractionScript interactionScript;
    public GameObject menu;
    public GameObject HUD;
    public GameObject exitMenu;
    public GameObject interactions;
    public GameObject map;
    public GameObject informations;
    public TMP_Text interactionsText;
    public TMP_Text informationsText;

    private void Awake()
    {
        interactionScript = GetComponent<InteractionScript>();
    }
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

    public void OpenMap()
    {
        Debug.Log("Opening map!");
        interactionScript.canSendRaycasts = false;
        ResetEverything();
        map.SetActive(true);
    }
    public void CloseMap()
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
        Debug.Log("Closing informations!");
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
