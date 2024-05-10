using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLobby : Interactable
{
    protected override void Interact()
    {
        SwitchCamera.Instance.SimpleFade(1, 0.5f);
        StartCoroutine(WaitForLoad());
        base.Interact();
    }

    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(4);
    }
}
