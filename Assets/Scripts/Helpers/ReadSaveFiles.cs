using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ReadSaveFiles : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] saveSlots;

    void Update()
    {
        ReadSaveFilesMethod();
    }

    public void ReadSaveFilesMethod()
    {
        // Persistent data path
        string path = Application.persistentDataPath;

        // Konsensus file penyimpnanan: savefile[i].json
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(path + "/savefile" + (i + 1) + ".json"))
            {
                string json = File.ReadAllText(path + "/savefile" + (i + 1) + ".json");
                GetComponent<LoadGameManager>().isSaved[i] = true;

                // Deserialize the JSON
                GameData gameData = JsonUtility.FromJson<GameData>(json);

                saveSlots[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(gameData.scene.name);
                int totalSeconds = (int)PlayerStatistic.INSTANCE.getTimePlayed();
                int hours = totalSeconds / 3600;
                int minutes = (totalSeconds % 3600) / 60;
                int seconds = totalSeconds % 60;

                saveSlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText($"{hours:D2}:{minutes:D2}:{seconds:D2}");
                saveSlots[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("Quest: " + gameData.scene.currentQuestID.ToString());
                saveSlots[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText("$ " + (gameData.point.ToString()) + " / " + (gameData.coin.ToString()));
            }
            else
            {
                GetComponent<LoadGameManager>().isSaved[i] = false;
            }
        }
    }
}
