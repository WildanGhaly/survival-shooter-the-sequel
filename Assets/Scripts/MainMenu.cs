using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject settingsCanvas;
    public GameObject statsCanvas;

    void Start()
    {
        mainMenuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        statsCanvas.SetActive(false);
    }
    public void NewGame()
    {
        // new game here
        SceneManager.LoadSceneAsync("Ultra Difficult");
    }

    public void LoadGame()
    {
        // TODO: load game here
        SceneManager.LoadSceneAsync("");
    }

    public void Stats()
    {
        // stats goes here
        mainMenuCanvas.SetActive(false);
        statsCanvas.SetActive(true);

    }

    public void Settings()
    {
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void ToMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        statsCanvas.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player quit da gem");
    }
}
