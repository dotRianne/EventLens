
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
        menu.SetActive(true);
        nonMenu.SetActive(false);
        exitMenu.SetActive(false);
        Debug.Log("Opening menu!");
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        nonMenu.SetActive(true);
        exitMenu.SetActive(false);
        Debug.Log("Closing menu!");
    }

    public void ExitMenu()
    {
        menu.SetActive(false);
        nonMenu.SetActive(false);
        exitMenu.SetActive(true);
        Debug.Log("Exit game?");
    }

    public void CloseApplication()
    {
        Application.Quit();
        Debug.Log("Closing app!");
    }
}
