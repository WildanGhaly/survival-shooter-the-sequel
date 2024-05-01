using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
        SceneManager.LoadSceneAsync("");
    }

    public void Settings()
    {
        // settings goes here
        SceneManager.LoadSceneAsync("");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player quit da gem");
    }
}
