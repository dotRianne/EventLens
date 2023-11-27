
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class Buttons : MonoBehaviour
{
    public GameObject menu;
    public GameObject HUD;
    public GameObject exitMenu;
    public GameObject interactions;
    public void OpenMenu()
    {
        ResetEverything();
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        ResetEverything();
        HUD.SetActive(true);
    }

    public void ExitMenu()
    {
        ResetEverything();
        exitMenu.SetActive(true);
    }
    public void CloseInteractions()
    {
        ResetEverything();
        Debug.Log("DISABLE INTERACTIONS!");
        HUD.SetActive(true);
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
