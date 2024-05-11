using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveGameManager : MonoBehaviour
{
    public GameObject[] saveSlotName = new GameObject[3];
    public GameObject[] availableSlotName = new GameObject[3];

    public void SaveGame(int id)
    {
        string savedName = saveSlotName[id-1].GetComponent<TMP_InputField>().text;
        if (availableSlotName[id - 1].activeInHierarchy)
        {
            savedName = availableSlotName[id - 1].GetComponent<TMP_InputField>().text;
        }
        GameManager.INSTANCE.SaveGame(id, savedName);
        GetComponent<ReadSaveFiles>().ReadSaveFilesMethod();
    }
}
