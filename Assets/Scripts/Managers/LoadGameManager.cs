using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadGameManager : MonoBehaviour
{
    public GameObject[] saveSlot = new GameObject[3];
    public GameObject[] slotAvailable = new GameObject[3];
    public bool[] isSaved = new bool[3];
    public int indexSelected;
    public int copyIndexSelected;
    public bool copyState = false;

    void Start()
    {
        SetIndex(-1);
        copyIndexSelected = -1;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < isSaved.Length; i++)
        {
            if (isSaved[i]){
                saveSlot[i].SetActive(true);
                slotAvailable[i].SetActive(false);
            } else {
                saveSlot[i].SetActive(false);
                slotAvailable[i].SetActive(true);
            }
        }
    }

    public void SetIndex(int index)
    {
        if(!copyState){
            indexSelected = index;
        }else{
            copyIndexSelected = index;
            if (indexSelected != -1 && copyIndexSelected != -1 && copyState){

                Debug.Log("SRC Index " + indexSelected.ToString());
                Debug.Log("DEST INDEX " + copyIndexSelected.ToString());

                // Read From SRC File
                string srcPath = Path.Combine(Application.persistentDataPath, "savefile" + (indexSelected + 1).ToString() + ".json");

                // Copy to DEST File
                string destPath = Path.Combine(Application.persistentDataPath, "savefile" + (copyIndexSelected + 1).ToString() + ".json");
                
                if(File.Exists(srcPath))
                    File.WriteAllText(destPath, File.ReadAllText(srcPath));

                copyState = false;
                copyIndexSelected = -1;
            }
        }
    }

    public void Clear()
    {
        if (indexSelected != -1){
            this.isSaved[indexSelected] = false;
            
            // Delete the savegamefile
            string path = Path.Combine(Application.persistentDataPath, "savefile" + (indexSelected + 1).ToString() + ".json");

            File.Delete(path);

            SetIndex(-1);
        }
    }

    public void Copy()
    {
        copyState = true;
    }

    public void EnablePlayerInputManager()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().enabled = true;
    }
}
