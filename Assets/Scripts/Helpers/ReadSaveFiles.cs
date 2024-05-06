using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ReadSaveFiles : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] saveSlots;

    void OnEnable()
    {
        // Persistent data path
        string path = Application.persistentDataPath;

        // Konsensus file penyimpnanan: savefile[i].json
        for (int i = 0; i < 3; i++)
        {
            if(File.Exists(path+"/savefile"+(i+1)+".json"))
            {
                string json = File.ReadAllText(path+"/savefile"+(i+1)+".json");
                GetComponent<LoadGameManager>().isSaved[i] = true;

                // Deserialize the JSON
                GameData gameData = JsonUtility.FromJson<GameData>(json);

                saveSlots[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(gameData.scene.name);
                saveSlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(((int) gameData.player.time/60).ToString()+ ":"+((int)gameData.player.time%60).ToString());
            }else{
                GetComponent<LoadGameManager>().isSaved[i] = false;
            }
        }
    }

    // Update is called once per frame
    void Start()
    {
        
    }
}
