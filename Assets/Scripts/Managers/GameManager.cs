using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
    public int point = 0;
    public int coin = 0;

    void Awake()
    {
        if(INSTANCE == null){
            INSTANCE = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // saveButton.AddListener(() => {SaveGame();});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatePointCoin(){
        point++;
        coin = (int) ((float) point * 0.8);
    }

    public void SaveGame()
    {
        string playerStat = "\"player\": " + JsonUtility.ToJson(PlayerStatistic.INSTANCE);
        string scene = "\"scene\": {\"name\":\""+ SceneManager.GetActiveScene().name + "\", \"index\": " + SceneManager.GetActiveScene().buildIndex.ToString() + "}"; 
        string pointCoint = "\"point\": " + this.point + ", \"coin\":" + this.coin;

        string json = "{"+ pointCoint + ", " + playerStat + "," + scene + "}";

        string path = Path.Combine(Application.persistentDataPath, "savefile.json");

        File.WriteAllText(path, json);

        Debug.Log(path);
    }

    public void LoadGame(string path)
    {
        // Get the path
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);

            // Deserialize the JSON
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            // Update some Class
            // Update GameManager
            point = gameData.point;
            coin = gameData.coin;

            // Update Player Statistics
            PlayerStatistic.INSTANCE.setPlayerName(gameData.player.playerName);
            PlayerStatistic.INSTANCE.setDistance(gameData.player.distanceReached);
            PlayerStatistic.INSTANCE.setEnemiesKilled(gameData.player.enemiesKilled);
            PlayerStatistic.INSTANCE.setTimePlayed(gameData.player.time);
            PlayerStatistic.INSTANCE.setBulletFired(gameData.player.bulletsShot);
            PlayerStatistic.INSTANCE.setBulletHit(gameData.player.bulletsHit);

            // Update Scene (TBD)
            SceneManager.LoadScene(gameData.scene.index); 
        }

    }
}

[Serializable]
public class GameData
{
    public int point;
    public int coin;
    public PlayerData player;
    public SceneData scene;
}

[Serializable]
public class PlayerData
{
    public string playerName;
    public int distanceReached;
    public int enemiesKilled;
    public float time;
    public int bulletsShot;
    public int bulletsHit;
}

[Serializable]
public class SceneData
{
    public string name;
    public int index;
}
