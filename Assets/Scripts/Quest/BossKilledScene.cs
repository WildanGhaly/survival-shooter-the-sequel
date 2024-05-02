using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKilledScene : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject cutsceneCam;

    private void OnEnable()
    {
        StartCoroutine(SwitchCameraWithFade(playerCam, cutsceneCam));
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
}
