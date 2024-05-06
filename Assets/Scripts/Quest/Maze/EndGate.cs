using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGate : Collidable
{
    [SerializeField] Animator gate;
    protected override void CollideEnter()
    {
        base.CollideEnter();
        Debug.Log("GATE CLOSED");
        gate.SetBool("isClosed", true);
    }
}
