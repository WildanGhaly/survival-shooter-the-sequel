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

    [SerializeField] private TextMeshProUGUI chatMode;
    [SerializeField] private GameObject chatModeObject;

    [SerializeField] private GameObject crosshair;
    private void Start()
    {
        chatModeObject.SetActive(false);
    }

    public void OpenGate()
    {
        crosshair.SetActive(false);
        promptMessage = string.Empty;
        StartCoroutine(SwitchPlayerToCutscene(playerCam, cutSceneCam, cutSceneCam2));
        StartCoroutine(OpenGateSequence());
        StartCoroutine(ChatMode());
    }

    IEnumerator ChatMode()
    {
        yield return new WaitForSeconds(1);
        chatMode.text = "Go faster, don't wake them!";
        chatModeObject.SetActive(true);
        yield return new WaitForSeconds(3);
        chatMode.text = string.Empty;
        yield return new WaitForSeconds(0.25f);
        chatMode.text = "The gate will be open soon, please hurry!";
        yield return new WaitForSeconds(3);
        chatMode.text = string.Empty;
        yield return new WaitForSeconds(0.25f);
        chatMode.text = "OH NOOO, WHY NOW?? THEY ARE WAKING UP!";
        yield return new WaitForSeconds(3);
        chatMode.text = string.Empty;
        chatMode.text = "GET TO THE GATE, IT WILL OPEN IN TWO MINUTES";
        yield return new WaitForSeconds(3);
        chatMode.text = string.Empty;
        chatModeObject.SetActive(false);
    }

    IEnumerator SwitchPlayerToCutscene(GameObject pCam, GameObject c1Cam, GameObject c2Cam)
    {
        yield return SwitchCameraWithFade(pCam, c1Cam);
        yield return new WaitForSeconds(3.5f);
        yield return SwitchCameraWithFade(c1Cam, c2Cam);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(ClosePath());
        yield return new WaitForSeconds(2);
        yield return SwitchCameraWithFade(c2Cam, pCam);
    }

    IEnumerator SwitchCameraWithFade(GameObject offCam, GameObject onCam)
    {
        yield return StartCoroutine(Fade(1));
        offCam.SetActive(false);
        onCam.SetActive(true);

        yield return StartCoroutine(Fade(0));
    }

    IEnumerator Fade(float targetAlpha)
    {
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
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
        inputManager.enabled = false;
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

