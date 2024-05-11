using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Willy2SecondCutscene : MonoBehaviour
{
    bool isTriggered;
    [SerializeField] private GameObject pCam, cam2;
    [SerializeField] private Animator door;
    [SerializeField] private AudioSource aud;
    [SerializeField] private AudioSource aud2;

    private readonly string[,] dialogues = new string[,]
    {
        {"Chatter", "Now villager can always open this door"},
        {"Chatter", "Nice work!"},
    };

    void Update()
    {
        if (!isTriggered && GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            isTriggered = true;
            StartCoroutine(StartCutscene());
            aud.Stop();
            aud2.Play();
        }       
    }

    IEnumerator StartCutscene()
    {
        GameManager.INSTANCE.updateCurrentQuestID(4);
        yield return new WaitForSeconds(1);
        SwitchCamera.Instance.SwitchCameraMethod(pCam, cam2, 0.5f);
        Conversation.Instance.StartConversation(dialogues);
        door.SetBool("isOpen", true);
        yield return new WaitForSeconds(7);
        SwitchCamera.Instance.SwitchCameraMethod(cam2, pCam, 0.5f);
        door.GetComponent<MeshCollider>().isTrigger = true;
        enabled = false;
    }
}
