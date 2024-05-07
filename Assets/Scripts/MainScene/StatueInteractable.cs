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
        SceneManager.LoadScene(4);
    }
}