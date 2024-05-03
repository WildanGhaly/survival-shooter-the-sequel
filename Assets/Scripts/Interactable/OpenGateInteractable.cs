using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenGateInteractable : Interactable
{
    [SerializeField] private Animator gate;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject cutSceneCam;
    [SerializeField] private GameObject cutSceneCam2;
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private InputManager inputManager;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private SkinnedMeshRenderer playerSkin;
    [SerializeField] private Nightmare.PlayerMovement playerMovement;
    [SerializeField] private CharacterController characterController;

    [SerializeField] private Animator pathStone;
    [SerializeField] private AudioSource pathStoneAudio;
    [SerializeField] private GameObject enemyManager;

    [SerializeField] private GameObject crosshair;

    private readonly string[,] dialogues = new string[,]
    {
        {"Player", "Here we go!"},
        {"Chatter", "Ahh too noisy, don't make too much noise!"},
        {"Chatter", "HOW DID THAT ROCK JUST FELL???"},
        {"Chatter", "OH NOOO, THEY ARE WAKING UP!!"},
        {"Chatter", "The gate will open in 120 seconds, please be safe"},
    };

    public void OpenGate()
    {
        inputManager.enabled = false;
        crosshair.SetActive(false);
        promptMessage = string.Empty;
        StartCoroutine(SwitchPlayerToCutscene(playerCam, cutSceneCam, cutSceneCam2));
        StartCoroutine(OpenGateSequence());
        ChatMode();
    }

    void ChatMode()
    {
        Conversation.Instance.StartConversation(dialogues);
    }

    IEnumerator SwitchPlayerToCutscene(GameObject pCam, GameObject c1Cam, GameObject c2Cam)
    {
        SwitchCamera.Instance.SwitchCameraMethod(pCam, c1Cam, fadeDuration);

        yield return new WaitForSeconds(4.5f);
        SwitchCamera.Instance.SwitchCameraMethod(c1Cam, c2Cam, fadeDuration);

        yield return new WaitForSeconds(2.5f);
        StartCoroutine(ClosePath());

        yield return new WaitForSeconds(2);
        SwitchCamera.Instance.SwitchCameraMethod(c2Cam, pCam, fadeDuration);
    }

    IEnumerator OpenGateSequence()
    {
        yield return new WaitForSeconds(3);
        gate.SetBool("isOpen", true);
        gate.GetComponent<AudioSource>().Play();
        yield return StartCoroutine(StartMovingFromFirstZone());
    }

    IEnumerator StartMovingFromFirstZone()
    {
        playerSkin.enabled = true;

        characterController.enabled = false;

        yield return null;

        playerTransform.rotation = Quaternion.Euler(0, -165, 0);
        playerTransform.localPosition = new Vector3(2, -5.5f, -5.5f);

        Vector2 moveDirection = Vector2.up;

        yield return null;
        characterController.enabled = true;

        while (playerTransform.position.z > -35)
        {
            playerMovement.ProcessMove(moveDirection);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ClosePath()
    {
        yield return null;
        pathStone.SetBool("isOpen", false);
        pathStoneAudio.Play();
        yield return new WaitForSeconds(3f);
        inputManager.enabled = true;
        ActivateEnemies();
    }

    private void ActivateEnemies()
    {
        enemyManager.SetActive(true);
    }

    public void CloseGate()
    {
        gate.SetBool("isOpen", false);
    }
}

