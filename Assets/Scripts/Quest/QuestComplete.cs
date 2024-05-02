using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestComplete : Collidable
{
    [SerializeField] private FinalCutscene finalCutscene;

    protected override void CollideEnter()
    {
        finalCutscene.enabled = false;
        base.CollideEnter();
    }
}
