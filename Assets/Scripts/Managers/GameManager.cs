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
    public int currentQuestID = 0;
    public int ultimateCount = 0;
    
    public string playerName;
    public float volume;

    public float multiplier = 1f;
    public int difficulty = 2;
    
    public void resetGame(){
        coin = 0;
        point = 0;
        currentQuestID = 0;
        ultimateCount = 0;
        multiplier = 1f;
        difficulty = 2;
    }

    public Dictionary<int, bool> hasPet = new();
    private List<float> prices = new List<float> { 256f, 128f, 200f };

    public GameObject[] petModel;

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void SetVolume(float volum)
    {
        volume = volum;
    }

    public void UpdateDifficulty(int df)
    {
        difficulty = df;
        switch (difficulty)
        {
            case 0: // Very Easy
                multiplier = 0.1f;
                break;
            case 1: // Easy
                multiplier = 0.5f;
                break;
            case 2: // Medium
                multiplier = 1f;
                break;
            case 3: // Hard
                multiplier = 2f;
                break;
            case 4: // ASIAN
                multiplier = 5f;
                break;
        }
    }


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
        StartCoroutine(updateGeneralSave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCurrentQuestID(int questID)
    {
        currentQuestID = questID;
    }

    public bool UseUltimate()
    {
        if (ultimateCount > 0)
        {
            ultimateCount--;
            return true;
        }
        return false;
    }

    public void AddUltimate(int num = 1)
    {
        ultimateCount += num;
    }

    public void addCoin(int coin)
    {
        this.coin += coin;
    }

    public void addPoint(int point)
    {
        this.point += point;
    }

    public void updatePointCoin(){
        point++;
        coin = (int) ((float) point * 0.8);
    }

    public void SaveGame(int id = 1)
    {
        string scene = "\"scene\": {\"name\":\""+ SceneManager.GetActiveScene().name + "\", \"index\": " + SceneManager.GetActiveScene().buildIndex.ToString() + ", \"currentQuestID\": " + currentQuestID.ToString() + ", \"ultimateCount\": " + ultimateCount.ToString() +"}"; 
        string pointCoint = "\"point\": " + this.point + ", \"coin\":" + this.coin + ", \"time\": " + PlayerStatistic.INSTANCE.getTimePlayed();
        string json = "{"+ pointCoint + ", " + scene + "}";

        string path = Path.Combine(Application.persistentDataPath, "savefile"+id+".json");

        File.WriteAllText(path, json);

        Debug.Log(path);
    }

    public void LoadGame(int id)
    {
        // Get the path
        if(File.Exists(Path.Combine(Application.persistentDataPath, "savefile"+id+".json")))
        {
            string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, "savefile"+id+".json"));

            // Deserialize the JSON
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            // Update some Class
            // Update GameManager
            point = gameData.point;
            coin = gameData.coin;
            currentQuestID = gameData.scene.currentQuestID;
            ultimateCount = gameData.scene.ultimateCount;
            
            // Update Scene (TBD)
            SceneManager.LoadScene(gameData.scene.index); 
        }

    }

    public void AddPet(int id = 0, float price = 0f)
    {
        // TODO: if tidak cukup maka ga jadi beli :V
        
        if (!hasPet.TryGetValue(id, out bool value) && coin > prices[id]){
            hasPet.Add(id, true);
            coin -= ((int)prices[id]);
            Debug.Log("Success, remaining coin : " + coin);
            Instantiate(petModel[id], GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        }
    }

    public void InitiatePet()
    {
        foreach (KeyValuePair<int, bool> pet in hasPet)
        {
            if (pet.Value)
            {
                Instantiate(petModel[pet.Key], GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
            }
        }
    }

    public IEnumerator updateGeneralSave()
    {
        while(true)
        {
            // Update GeneralSave
            string filePath = Path.Combine(Application.persistentDataPath, "generalsave.json");

            if(File.Exists(filePath) && SceneManager.GetActiveScene().buildIndex > 1)
            {
                string generalSaveJSON = File.ReadAllText(filePath);
                GeneralSave gs = JsonUtility.FromJson<GeneralSave>(generalSaveJSON);

                gs.playerData.playerName = PlayerStatistic.INSTANCE.getPlayerName();
                gs.playerData.distanceReached = PlayerStatistic.INSTANCE.getDistance();
                gs.playerData.enemiesKilled = PlayerStatistic.INSTANCE.getKillCount();
                gs.playerData.time = PlayerStatistic.INSTANCE.getTimePlayed();
                gs.playerData.bulletsShot = PlayerStatistic.INSTANCE.getBulletFired();
                gs.playerData.bulletsHit = PlayerStatistic.INSTANCE.getBulletHit();
                gs.playerData.deathCount = PlayerStatistic.INSTANCE.getDeathCount();
                gs.playerData.orbsCollected = PlayerStatistic.INSTANCE.getOrbsCollected();

                Debug.Log(JsonUtility.ToJson(gs));

                File.WriteAllText(filePath, JsonUtility.ToJson(gs));
            }

            yield return new WaitForSeconds(1);
        }
    }
}

[Serializable]
public class GameData
{
    public int point;
    public int coin;
    public float time;
    public SceneData scene;
}

[Serializable]
public class PlayerData
{
    public string playerName;
    public float distanceReached;
    public int enemiesKilled;
    public float time;
    public int bulletsShot;
    public int bulletsHit;
    public int deathCount;
    public int orbsCollected;
}

[Serializable]
public class SceneData
{
    public string name;
    public int index;
    public int currentQuestID;
    public int ultimateCount;
}
