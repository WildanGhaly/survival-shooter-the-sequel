using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Conversation : MonoBehaviour
{
    public static Conversation Instance { get; private set; }

    [SerializeField] private GameObject playerChat, chatterChat, chatWidget;
    [SerializeField] private TextMeshProUGUI playerChatPrompt, chatterChatPrompt;
    [SerializeField] private float timeBetweenChat = 3f;

    private string[,] dialogues;
    private int currentDialogueIndex = 0;

    public event Action ConversationEnded;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartConversation(string[,] dialogues)
    {
        this.dialogues = dialogues;
        chatWidget.SetActive(true);
        currentDialogueIndex = 0;
        StartCoroutine(RunConversation());
    }

    IEnumerator RunConversation()
    {
        while (currentDialogueIndex < dialogues.GetLength(0))
        {
            string speaker = dialogues[currentDialogueIndex, 0];
            string text = dialogues[currentDialogueIndex, 1];

            if (speaker == "Player")
            {
                playerChatPrompt.SetText(text);
                playerChat.SetActive(true);
                chatterChat.SetActive(false);
            }
            else
            {
                chatterChatPrompt.SetText(text);
                chatterChat.SetActive(true);
                playerChat.SetActive(false);
            }

            currentDialogueIndex++;
            yield return new WaitForSeconds(timeBetweenChat);
        }

        ConversationEnded?.Invoke();
        playerChat.SetActive(false);
        chatterChat.SetActive(false);
        chatWidget.SetActive(false);
    }
}
