using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWilly2FirstCutscene : Collidable
{
    [SerializeField] private DoubleDoorInteractable interactableDoor;

    protected override void CollideEnter()
    {
        interactableDoor.stopTransform = true;
        base.CollideEnter();
    }
}
