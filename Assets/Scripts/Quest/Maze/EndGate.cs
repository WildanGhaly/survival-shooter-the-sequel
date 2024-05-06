using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGate : Collidable
{
    [SerializeField] Animator gate;
    [SerializeField] private MazeMonster mazeMonster;

    protected override void CollideEnter()
    {
        base.CollideEnter();
        gate.SetBool("isClosed", true);
        mazeMonster.StopMovement();
    }
}
