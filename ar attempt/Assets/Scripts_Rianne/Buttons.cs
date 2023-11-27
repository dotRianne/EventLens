
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
    public TMP_Text interactionsText;

    private void Start()
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
        Debug.Log("DISABLE INTERACTIONS!");
    }

    public void CloseApplication()
    {
        Application.Quit();
        Debug.Log("Closing app!");
    }

    public void ResetEverything()
    {
        menu.SetActive(false);
        HUD.SetActive(false);
        exitMenu.SetActive(false);
        interactions.SetActive(false);
    }    
}
