using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseGateCollidable : Collidable
{
    [SerializeField] private GameObject gateToClose;
    [SerializeField] private GameObject gateToOpen;
    [SerializeField] private GameObject enemyManager;

    protected override void CollideEnter()
    {
        base.CollideEnter();
        gateToClose.GetComponent<Animator>().SetBool("isOpen", false);
        gateToClose.GetComponent<AudioSource>().Play();
        gateToOpen.GetComponent<Animator>().SetBool("isOpen", true);
        gateToOpen.GetComponent<AudioSource>().Play();
        StartCoroutine(KillAllEnemy());
    }

    IEnumerator KillAllEnemy()
    {
        enemyManager.SetActive(false);
        yield return new WaitForFixedUpdate();
        while (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            yield return null;
        }
        Destroy(gameObject);
    }
}
