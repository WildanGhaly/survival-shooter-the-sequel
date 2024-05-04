using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKilledScene : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject cutsceneCam;
    [SerializeField] private GameObject healthBar, crosshair;

    private readonly string[,] dialogues = new string[,]
    {
        {"Chatter", "I knew you can do it, get that coins and go back!"}
    };

    private void OnEnable()
    {
        SwitchCamera.Instance.SwitchCameraMethod(playerCam, cutsceneCam, fadeDuration);
        Conversation.Instance.StartConversation(dialogues);
        healthBar.SetActive(false);
        crosshair.SetActive(false);
    }
}
