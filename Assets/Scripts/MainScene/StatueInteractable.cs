using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatueInteractable : Interactable
{
    protected override void Interact()
    {
        base.Interact();
        StartCoroutine(LoadQuest());
    }
    IEnumerator LoadQuest()
    {
        Debug.Log("LOAD QUEST");
        SwitchCamera.Instance.SimpleFade(1, 2f);
        yield return new WaitForSeconds(2f);

        if(GameManager.INSTANCE.currentQuestID == 1 || GameManager.INSTANCE.currentQuestID == 2){ // Prev quest is Adventure
            SceneManager.LoadScene(4);
        }else if(GameManager.INSTANCE.currentQuestID == 3 || GameManager.INSTANCE.currentQuestID == 4){ // Prev quest is Willy-2 or Prev quest is Willy2-1
            SceneManager.LoadScene(5);
        }else if(GameManager.INSTANCE.currentQuestID == 5){ // Prev quest is Willy2-2
            SceneManager.LoadScene(6);
        }else if(GameManager.INSTANCE.currentQuestID == 6){ // Prev quest is Scene5
            SceneManager.LoadScene(7);
        }else if(GameManager.INSTANCE.currentQuestID == 7){ // Prev quest is Ultra Diff
            SceneManager.LoadScene(8);
        }
    }
}