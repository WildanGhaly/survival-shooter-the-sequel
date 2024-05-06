using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorInteractable : Interactable
{
    [SerializeField] private GameObject cam1, cam2;
    [SerializeField] private GameObject pCam;
    [SerializeField] private GameObject EnemyFirstHalf;

    protected override void Interact()
    {
        GetComponent<Animator>().SetBool("isOpen", true);
        GetComponent<MeshCollider>().isTrigger = true;
        SwitchCamera.Instance.SwitchCameraMethod(cam1, cam2, 0.2f);
        StartCoroutine(SpawnEnemy());
        base.Interact();
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(17);
        EnemyFirstHalf.SetActive(true);
        yield return new WaitForSeconds(4);
        SwitchCamera.Instance.SwitchCameraMethod(cam2, pCam, 0.2f);
    }
}
