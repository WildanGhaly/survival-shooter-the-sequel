using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene2Trigger : Collidable
{
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject firstCam;
    [SerializeField] private GameObject secondCam;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject secondGate;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject wizardHealthBar;

    protected override void CollideEnter()
    {
        StartCoroutine(SwitchPlayerToCutscene(playerCam, firstCam, secondCam));
        StartCoroutine(PlayerMove());
        base.CollideEnter();
    }

    IEnumerator SwitchPlayerToCutscene(GameObject pCam, GameObject c1Cam, GameObject c2Cam)
    {
        yield return SwitchCameraWithFade(pCam, c1Cam);
        yield return new WaitForSeconds(6f);
        yield return SwitchCameraWithFade(c1Cam, c2Cam);
        yield return new WaitForSeconds(4f);
        yield return SwitchCameraWithFade(c2Cam, pCam);
        wizardHealthBar.SetActive(true);
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

    IEnumerator PlayerMove()
    {
        player.GetComponent<CharacterController>().enabled = false;
        playerModel.GetComponent<SkinnedMeshRenderer>().enabled = true;
        player.GetComponent<InputManager>().enabled = false;
        yield return new WaitForSeconds(0.75f);
        player.transform.SetPositionAndRotation(new Vector3(-82, 0, -156), Quaternion.Euler(0, -90, 0));
        yield return null;
        player.GetComponent<CharacterController>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        while (secondGate.activeInHierarchy)
        {
            yield return new WaitForFixedUpdate();
            player.GetComponent<Nightmare.PlayerMovement>().ProcessMove(Vector2.up);
        }
        yield return new WaitForSeconds(5);
        player.GetComponent<InputManager>().enabled = true;
    }
}
