using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondDoorInteractable : Interactable
{
    [SerializeField] private GameObject pCam, cam1;
    [SerializeField] private GameObject player, playerModel;
    [SerializeField] private AudioSource aud;
    [SerializeField] private AudioSource aud2;

    public bool stopTransform = false;

    public bool isQuestStarted = false;

    private readonly string[,] dialogues = new string[,]
    {
        {"Chatter", "Villager can only take the woods from here"},
        {"Chatter", "But the monster is insanely strong"},
        {"Chatter", "Defeat the monster, use trees to help you hide"},
        {"Chatter", "Good luck..."},
    };

    protected override void Interact()
    {
        if (!isQuestStarted)
        {
            SwitchCamera.Instance.SwitchCameraMethod(pCam, cam1, 0.2f);
            GetComponent<Animator>().SetBool("isOpen", true);
            StartCoroutine(TransformPlayer());
            promptMessage = string.Empty;
            isQuestStarted = true;
            GetComponent<MeshCollider>().isTrigger = true;
            base.Interact();
            aud.Stop();
            aud2.Play();
        }
    }

    IEnumerator TransformPlayer()
    {
        DisablePlayer(true);
        yield return null;
        player.transform.localPosition = new Vector3(36, -1, 18);
        player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(2);
        Conversation.Instance.StartConversation(dialogues);
        player.GetComponent<CharacterController>().enabled = true;
        while (!stopTransform)
        {
            yield return new WaitForFixedUpdate();
            player.GetComponent<Nightmare.PlayerMovement>().ProcessMove(Vector2.up * 0.5f);
        }
        GetComponent<Animator>().SetBool("isOpen", false);
        GetComponent<MeshCollider>().isTrigger = false;
        DisablePlayer(false);
    }

    void DisablePlayer(bool disable)
    {
        player.GetComponent<InputManager>().enabled = !disable;
        player.GetComponent<CharacterController>().enabled = !disable;
        playerModel.GetComponent<SkinnedMeshRenderer>().enabled = disable;
    }

}
