using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Willy2ThirdCutscene : MonoBehaviour
{
    bool isTriggered;
    [SerializeField] private GameObject pCam, cam2;
    [SerializeField] private Animator door;

    private readonly string[,] dialogues = new string[,]
    {
        {"Chatter", "Now villager can get some woods"},
        {"Chatter", "Nice work! this is the end of your journey"},
    };

    void Update()
    {
        if (!isTriggered && GameObject.FindGameObjectWithTag("FinalBoss") == null)
        {
            isTriggered = true;
            StartCoroutine(StartCutscene());
        }
        
        if (GameObject.FindGameObjectWithTag("FinalBoss") == null && GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    IEnumerator StartCutscene()
    {
        SwitchCamera.Instance.SwitchCameraMethod(pCam, cam2, 0.5f);
        Conversation.Instance.StartConversation(dialogues);
        door.SetBool("isOpen", true);
        yield return new WaitForSeconds(15);
        SwitchCamera.Instance.SwitchCameraMethod(cam2, pCam, 0.5f);
        door.GetComponent<MeshCollider>().isTrigger = true;
        enabled = false;
    }
}
