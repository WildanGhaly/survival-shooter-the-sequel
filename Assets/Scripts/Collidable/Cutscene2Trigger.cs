using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene2Trigger : Collidable
{
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject firstCam;
    [SerializeField] private GameObject secondCam;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject secondGate;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject wizardHealthBar;

    private readonly string[,] dialogues = new string[,]
    {
        {"Chatter", "Wait... Is that... KEPALA KEROCO??"},
        {"Chatter", "This monster can launch a very powerfull attack and summon KEROCO"},
        {"Chatter", "Find cover when he attacks, you should be fine!"},
        {"Chatter", "He can walk through object, but it's weapon can't"},
    };

    protected override void CollideEnter()
    {
        StartCoroutine(SwitchPlayerToCutscene(playerCam, firstCam, secondCam));
        StartCoroutine(PlayerMove());
        base.CollideEnter();
    }

    IEnumerator SwitchPlayerToCutscene(GameObject pCam, GameObject c1Cam, GameObject c2Cam)
    {
        SwitchCamera.Instance.SwitchCameraMethod(pCam, c1Cam, fadeDuration);
        yield return new WaitForSeconds(7f);
        Conversation.Instance.StartConversation(dialogues);
        SwitchCamera.Instance.SwitchCameraMethod(c1Cam, c2Cam, fadeDuration);
        yield return new WaitForSeconds(4f);
        SwitchCamera.Instance.SwitchCameraMethod(c2Cam, pCam, fadeDuration);
        wizardHealthBar.SetActive(true);
    }

    IEnumerator PlayerMove()
    {
        player.GetComponent<CharacterController>().enabled = false;
        playerModel.GetComponent<SkinnedMeshRenderer>().enabled = true;
        player.GetComponent<InputManager>().enabled = false;
        yield return new WaitForSeconds(0.75f);
        player.transform.SetPositionAndRotation(new Vector3(-82, 0, -156), Quaternion.Euler(0, -90, 0));
        yield return null;
        player.GetComponent<CharacterController>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        while (secondGate.activeInHierarchy)
        {
            yield return new WaitForFixedUpdate();
            player.GetComponent<Nightmare.PlayerMovement>().ProcessMove(Vector2.up);
        }
        yield return new WaitForSeconds(5);
        player.GetComponent<InputManager>().enabled = true;
    }
}
