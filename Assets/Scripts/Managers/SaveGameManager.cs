using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public void SaveGame(int id)
    {
        GameManager.INSTANCE.SaveGame(id);
        GetComponent<ReadSaveFiles>().ReadSaveFilesMethod();
    }
}
