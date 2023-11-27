
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class Buttons : MonoBehaviour
{
    public GameObject menu;
    public GameObject nonMenu;
    public GameObject exitMenu;
    public void OpenMenu()
    {
        ResetEverything();
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        ResetEverything();
        nonMenu.SetActive(true);
    }

    public void ExitMenu()
    {
        ResetEverything();
        exitMenu.SetActive(true);
    }

    public void CloseApplication()
    {
        Application.Quit();
        Debug.Log("Closing app!");
    }

    public void ResetEverything()
    {
        menu.SetActive(false);
        nonMenu.SetActive(false);
        exitMenu.SetActive(false);
    }    
}
