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
}
