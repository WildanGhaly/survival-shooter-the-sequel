using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainHall : MonoBehaviour
{
    void Start()
    {
        GameManager.INSTANCE.addCoin(100);
        GameManager.INSTANCE.addPoint(150);
        GameManager.INSTANCE.updateCurrentQuestID(1);
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        SwitchCamera.Instance.SimpleFade(1,2f);
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(4);
    }
}