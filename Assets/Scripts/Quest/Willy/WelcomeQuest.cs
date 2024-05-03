using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeQuest : MonoBehaviour
{
    [SerializeField] private GameObject questUI;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject cameraStart, cameraPlayer, crosshair;

    private readonly string[,] dialogues = new string[,]
    {
        {"Chatter", "Hey, welcome to your first quest"},
        {"Player", "Where am I?"},
        {"Chatter", "This is your first quest!"},
        {"Player", "What should I do?"},
        {"Chatter", "Open the gate slowly and get to the end of this path"},
        {"Player", "Is that it? Is there any danger?"},
        {"Chatter", "Enemies are asleep at this time, don't make sound and you will be fine!"},
        {"Player", "Haha that should be easy, I mean what could go wrong. Right??"}
    };

    void Start()
    {
        inputManager.enabled = false;
        crosshair.SetActive(false);
        questUI.SetActive(true);
        Conversation.Instance.StartConversation(dialogues);
        Conversation.Instance.ConversationEnded += HandleConversationEnd;
        StartCoroutine(DisableWelcomeQuest());
    }

    void HandleConversationEnd()
    {
        questUI.SetActive(false);
        inputManager.enabled = true;
        SwitchCamera.Instance.SwitchCameraMethod(cameraStart, cameraPlayer, 0.5f);
    }

    void OnDisable()
    {
        if (Conversation.Instance != null)
        {
            Conversation.Instance.ConversationEnded -= HandleConversationEnd;
        }
    }

    IEnumerator DisableWelcomeQuest()
    {
        yield return new WaitForSeconds(40);
        enabled = false;
    }
}
