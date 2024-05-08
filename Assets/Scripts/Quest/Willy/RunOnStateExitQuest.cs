using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunOnStateExitQuest : StateMachineBehaviour
{
    [SerializeField] private int targetSceneIndex = 3;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene(targetSceneIndex);
    }
}
