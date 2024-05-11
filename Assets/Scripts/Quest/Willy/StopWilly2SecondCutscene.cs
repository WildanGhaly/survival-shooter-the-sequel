using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWilly2SecondCutscene : Collidable
{
    [SerializeField] private SecondDoorInteractable interactableDoor;

    protected override void CollideEnter()
    {
        interactableDoor.stopTransform = true;
        base.CollideEnter();
    }
}
