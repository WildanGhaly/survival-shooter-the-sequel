using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondQuestCutscene2 : MonoBehaviour
{
    [SerializeField] private GameObject secondHalfEnemy;
    [SerializeField] private GameObject pCam;
    [SerializeField] private Willy2ThirdCutscene bossDeathChecker;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(12);
        secondHalfEnemy.SetActive(true);
        yield return new WaitForSeconds(1);
        bossDeathChecker.enabled = true;
        SwitchCamera.Instance.SwitchCameraMethod(gameObject, pCam, 0.5f);
    }
}
