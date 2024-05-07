using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorInteractable : Interactable
{
    [SerializeField] private GameObject cam1, cam2;
    [SerializeField] private GameObject pCam;
    [SerializeField] private GameObject EnemyFirstHalf;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Willy2SecondCutscene enemyActiveChecker;
    [SerializeField] private AudioSource aud;
    [SerializeField] private AudioSource aud2;

    public bool isQuestStarted = false;
    public bool stopTransform = false;

    private readonly string[,] dialogues = new string[,]
    {
        {"Chatter", "Villager never open the door because of the monster"},
        {"Chatter", "Kill all the monster so then the villager can come here"},
        {"Chatter", "Use obstacle to hide if you need"},
        {"Chatter", "The monster has debuff area that you can see!"},
        {"Chatter", "Good luck..."},
    };

    protected override void Interact()
    {
        if (!isQuestStarted)
        {
            StartDialogues();
            promptMessage = string.Empty;
            OpenDoor();
            SwitchCamera.Instance.SwitchCameraMethod(cam1, cam2, 0.2f);
            StartCoroutine(SpawnEnemy());
            StartCoroutine(TransformPlayer());
            StartCoroutine(ActivatePlayer());
            base.Interact();
            isQuestStarted = true;
            aud.Stop();
            aud2.Play();
        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(17);
        EnemyFirstHalf.SetActive(true);
        yield return new WaitForSeconds(4);
        SwitchCamera.Instance.SwitchCameraMethod(cam2, pCam, 0.2f);
    }

    IEnumerator TransformPlayer()
    {
        yield return null;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<InputManager>().enabled = false;
        playerModel.GetComponent<SkinnedMeshRenderer>().enabled = true;
        player.transform.localPosition = new Vector3(4, -1, -6);
        player.transform.localRotation = Quaternion.Euler(0, 90, 0);
        yield return new WaitForSeconds(4);
        player.GetComponent<CharacterController>().enabled = true;
        while (!stopTransform)
        {
            yield return new WaitForFixedUpdate();
            player.GetComponent<Nightmare.PlayerMovement>().ProcessMove(Vector2.up * 0.4f); // biar lambat aja sih
        }
        CloseDoor();
    }

    IEnumerator ActivatePlayer()
    {
        yield return new WaitForSeconds(20);
        Debug.Log("Activated");
        enemyActiveChecker.enabled = true;
        player.GetComponent<InputManager>().enabled = true;
    }

    void OpenDoor()
    {
        GetComponent<Animator>().SetBool("isOpen", true);
        GetComponent<MeshCollider>().isTrigger = true;
    }

    void CloseDoor()
    {
        GetComponent<Animator>().SetBool("isOpen", false);
        GetComponent<MeshCollider>().isTrigger = false;
    }

    void StartDialogues()
    {
        Conversation.Instance.StartConversation(dialogues);
    }
}
