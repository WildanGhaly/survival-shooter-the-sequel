using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject loadGameCanvas;
    public GameObject settingsCanvas;
    public GameObject statsCanvas;


    public void Start()
    {
        mainMenuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        loadGameCanvas.SetActive(false);
        statsCanvas.SetActive(false);
        
    }
    public void NewGame()
    {
        SceneManager.LoadSceneAsync("Ultra Difficult");
    }

    public void LoadGame()
    {
        loadGameCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);

        loadGameCanvas.GetComponent<ReadSaveFiles>().enabled = true; 
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
        loadGameCanvas.SetActive(false);

        // Disable
        loadGameCanvas.GetComponent<ReadSaveFiles>().enabled = false; 
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player quit da gem");
    }
}
