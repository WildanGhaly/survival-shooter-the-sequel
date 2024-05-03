using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public static SwitchCamera Instance;

    [SerializeField] private CanvasGroup fadeCanvasGroup;

    private void Awake()
    {
        Instance = this;
    }

    public void SwitchCameraMethod(GameObject offCam, GameObject onCam, float fadeDuration)
    {
        StartCoroutine(SwitchCameraWithFade(offCam, onCam, fadeDuration));
    }

    public void SimpleFade(float targetAlpha, float fadeDuration)
    {
        StartCoroutine(Fade(targetAlpha, fadeDuration));
    }

    IEnumerator SwitchCameraWithFade(GameObject offCam, GameObject onCam, float fadeDuration)
    {
        yield return StartCoroutine(Fade(1, fadeDuration));
        offCam.SetActive(false);
        onCam.SetActive(true);

        yield return StartCoroutine(Fade(0, fadeDuration));
    }

    IEnumerator Fade(float targetAlpha, float fadeDuration)
    {
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
    }
}
