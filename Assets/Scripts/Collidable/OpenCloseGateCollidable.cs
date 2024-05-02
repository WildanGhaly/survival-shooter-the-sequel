using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseGateCollidable : Collidable
{
    [SerializeField] private GameObject gateToClose;
    [SerializeField] private GameObject gateToOpen;

    protected override void CollideEnter()
    {
        base.CollideEnter();
        gateToClose.GetComponent<Animator>().SetBool("isOpen", false);
        gateToClose.GetComponent<AudioSource>().Play();
        gateToOpen.GetComponent<Animator>().SetBool("isOpen", true);
        gateToOpen.GetComponent<AudioSource>().Play();
    }
}
