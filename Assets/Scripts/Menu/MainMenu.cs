using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using Unity.VisualScripting;

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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SwitchCamera.Instance.SimpleFade(0, 0.5f);
        SetupGeneralSave();
    }
    
    public void NewGame()
    {
        GameManager.INSTANCE.resetGame();
        SceneManager.LoadScene(2);
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
        GeneralSave gs;

        if(!File.Exists(filePath))
        {
            gs = new GeneralSave("Player", 0, 0);
            File.WriteAllText(filePath, JsonUtility.ToJson(gs));
        }
        gs = JsonUtility.FromJson<GeneralSave>(File.ReadAllText(filePath));
        // Write to GameManager
        GameManager.INSTANCE.SetPlayerName(gs.playerName);
        GameManager.INSTANCE.SetVolume(gs.volume);
        GameManager.INSTANCE.UpdateDifficulty(gs.difficulty);

        // Write to Canvas
        settingsCanvas.GetComponentInChildren<TMP_InputField>().text = gs.playerName;
        settingsCanvas.GetComponentInChildren<Slider>().value = gs.volume;
        settingsCanvas.GetComponentInChildren<TMP_Dropdown>().value = gs.difficulty;
        
        // Write to Player Statistics
        PlayerStatistic.INSTANCE.setPlayerName(gs.playerData.playerName);
        PlayerStatistic.INSTANCE.setDistance(gs.playerData.distanceReached);
        PlayerStatistic.INSTANCE.setEnemiesKilled(gs.playerData.enemiesKilled);
        PlayerStatistic.INSTANCE.setTimePlayed(gs.playerData.time);
        PlayerStatistic.INSTANCE.setBulletFired(gs.playerData.bulletsShot);
        PlayerStatistic.INSTANCE.setBulletHit(gs.playerData.bulletsHit);
        PlayerStatistic.INSTANCE.setDeathCount(gs.playerData.deathCount);
        PlayerStatistic.INSTANCE.setOrbsCollected(gs.playerData.orbsCollected);
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

        GeneralSave gs;

        string filePath = Path.Combine(Application.persistentDataPath, "generalsave.json");

        if(File.Exists(filePath))
        {
            gs = JsonUtility.FromJson<GeneralSave>(File.ReadAllText(filePath));
            gs.playerName = rename.text;
            gs.volume = volume.value;
            gs.difficulty = difficulty.value;
        }else{
            gs = new GeneralSave(rename.text, volume.value, difficulty.value);
        }

        string json = JsonUtility.ToJson(gs);
        
        File.WriteAllText(filePath, json);

        AudioListener.volume = volume.value;

        GameManager.INSTANCE.SetPlayerName(rename.text);
        GameManager.INSTANCE.SetVolume(volume.value);
        GameManager.INSTANCE.UpdateDifficulty(difficulty.value);
    }

    public void LoadGameToScene(int id)
    {
        GameManager.INSTANCE.LoadGame(id);
    }
}

[Serializable]
class GeneralSave{
    public string playerName;
    public float volume;
    public int difficulty;
    public PlayerData playerData;

    public GeneralSave(string p, float v, int d)
    {
        playerName = p;
        volume = v;
        difficulty = d;
    }
}
