using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
    public GameObject button;

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

    public void SaveGame()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerStats.json", JsonUtility.ToJson(PlayerStatistic.INSTANCE));
    }
}
