using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

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

        SetupGeneralSave();
        
    }
    public void NewGame()
    {
        SceneManager.LoadScene(1);
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

        SetupGeneralSave();
    }

    private void SetupGeneralSave()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "generalsave.json");

        if(File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            GeneralSave gs = JsonUtility.FromJson<GeneralSave>(json);

            // Write to GameManager
            GameManager.INSTANCE.SetPlayerName(gs.playerName);
            GameManager.INSTANCE.SetVolume(gs.volume);
            GameManager.INSTANCE.UpdateDifficulty(gs.difficulty);

            // Write to Canvas
            settingsCanvas.GetComponentInChildren<TMP_InputField>().text = gs.playerName;
            settingsCanvas.GetComponentInChildren<Slider>().value = gs.volume;
            settingsCanvas.GetComponentInChildren<TMP_Dropdown>().value = gs.difficulty;
        }
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

    public void SaveSettings()
    {
        TMP_InputField rename = settingsCanvas.GetComponentInChildren<TMP_InputField>();
        Slider volume = settingsCanvas.GetComponentInChildren<Slider>();
        TMP_Dropdown difficulty = settingsCanvas.GetComponentInChildren<TMP_Dropdown>();

        GeneralSave gs = new GeneralSave(rename.text, volume.value, difficulty.value);

        string json = JsonUtility.ToJson(gs);

        string filePath = Path.Combine(Application.persistentDataPath, "generalsave.json");
        
        File.WriteAllText(filePath, json);

        GameManager.INSTANCE.SetPlayerName(rename.text);
        GameManager.INSTANCE.SetVolume(volume.value);
        GameManager.INSTANCE.UpdateDifficulty(difficulty.value);
    }
}

[Serializable]
class GeneralSave{
    public string playerName;
    public float volume;
    public int difficulty;

    public GeneralSave(string p, float v, int d)
    {
        playerName = p;
        volume = v;
        difficulty = d;
    }
}
