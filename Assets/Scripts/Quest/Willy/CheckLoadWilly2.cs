using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLoadWilly2 : MonoBehaviour
{
    [SerializeField] private GameObject door1, trigger1;
    [SerializeField] private GameObject door2, trigger2;

    public int questNumber;

    private void Start()
    {
        questNumber = GameManager.INSTANCE.currentQuestID;
        Debug.Log("Quest Number " + questNumber);
        LoadMap();
    }

    public void LoadMap()
    {
        if (questNumber >= 1)
        {
            SkipFirstQuest();
        }

        if (questNumber >= 2)
        {
            SkipSecondQuest();
        }
    }

    private void SkipFirstQuest()
    {
        door1.GetComponent<Animator>().SetBool("isOpen", true);
        door1.GetComponent<MeshCollider>().isTrigger = true;
        door1.GetComponent<DoubleDoorInteractable>().isQuestStarted = false;
        door1.GetComponent<Interactable>().promptMessage = string.Empty;
        trigger1.SetActive(false);
    }

    private void SkipSecondQuest()
    {
        door2.GetComponent<Animator>().SetBool("isOpen", true);
        door2.GetComponent<MeshCollider>().isTrigger = true;
        door2.GetComponent<SecondDoorInteractable>().isQuestStarted = false;
        door2.GetComponent<Interactable>().promptMessage = string.Empty;
        trigger2.SetActive(false);
    }
}
