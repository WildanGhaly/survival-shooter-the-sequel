using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCamera : MonoBehaviour
{
    public GameObject cam2;

    private readonly string[,] dialogues = new string[,]
    {
        {"Chatter", "Congratulations!, you are the first person who can escape from this maze"},
    };

    void Start()
    {
        Conversation.Instance.StartConversation(dialogues);
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3f);    
        SwitchCamera.Instance.SwitchCameraMethod(gameObject, cam2, 0.5f);
    }
}
