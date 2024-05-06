using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour
{
    public GameObject cam2;
    [SerializeField] Animator gate;

    private readonly string[,] dialogues = new string[,]
    {
        {"Chatter", "You have one mission, RUNN!!!"},
    };
    private void Start()
    {
        Conversation.Instance.StartConversation(dialogues);
    }
    private void FixedUpdate()
    {
        if (transform.position.z < -188)
        {
            Vector3 position = transform.position;
            position.z += Time.deltaTime * 10f;
            transform.position = position;
        }
        else
        {
            enabled = false;
            gate.SetBool("isGateOpen", true);   
        }
    }

    private void OnDisable()
    {
        SwitchCamera.Instance.SwitchCameraMethod(gameObject, cam2, 0.5f);
    }
}
