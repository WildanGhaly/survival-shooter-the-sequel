using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestComplete : Collidable
{
    [SerializeField] private FinalCutscene finalCutscene;
    [SerializeField] private Animator exitAnimator;
    [SerializeField] private int sceneIndexTarget = 1;
    [SerializeField] private float delayExit = 6.25f;

    protected override void CollideEnter()
    {
        finalCutscene.enabled = false;
        exitAnimator.SetBool("StartAnimate", true);
        base.CollideEnter();
    }
}
