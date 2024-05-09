using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameManager : MonoBehaviour
{
    public GameObject[] saveSlot = new GameObject[3];
    public GameObject[] slotAvailable = new GameObject[3];
    public bool[] isSaved = new bool[3];
    public int indexSelected;
    public int copyIndexSelected;

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
        indexSelected = index;
    }

    public void Clear()
    {
        if (indexSelected != -1){
            this.isSaved[indexSelected] = false;
            SetIndex(-1);
        }
    }

    public void Copy()
    {
        if (indexSelected != -1){
            copyIndexSelected = indexSelected;
        }
    }

    public void EnablePlayerInputManager()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().enabled = true;
    }
}
