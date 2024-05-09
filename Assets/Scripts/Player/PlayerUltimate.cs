using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimate : MonoBehaviour
{
    [SerializeField] private GameObject ultimate;
    [SerializeField] private float godModeTime = 10f;
    [SerializeField] private float ultimateTime = 20f;
    [SerializeField] private float backToPlayer = 2f;
    [SerializeField] private float healTimeFrequency = 0.2f, healPerSecond = 10f;
    [SerializeField] private GameObject pCam, ultCam;
    [SerializeField] private SkinnedMeshRenderer playerModel;

    private Nightmare.PlayerHealth health;
    private Animator animate;
    private InputManager inputManager;

    private void Awake()
    {
        health = GetComponent<Nightmare.PlayerHealth>();
        animate = ultimate.GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
    }

    private void OnEnable()
    {
        if (!GameManager.INSTANCE.UseUltimate())
        {
            enabled = false;
            return;
        }
        inputManager.enabled = false;
        pCam.SetActive(false);
        ultCam.SetActive(true);
        playerModel.enabled = true;
        ultimate.SetActive(true);
        animate.SetBool("Ult", true);
        StartCoroutine(WaitForDestroy());
        StartCoroutine(BackToPlayer());
        StartCoroutine(HealOverTime());
        if (!health.godMode)
        {
            health.godMode = true;
            StartCoroutine(WaitForGod());
        }
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(ultimateTime);
        DisableAnimate();
    }

    IEnumerator WaitForGod()
    {
        yield return new WaitForSeconds(godModeTime);
        health.godMode = false;
    }

    IEnumerator BackToPlayer()
    {
        yield return new WaitForSeconds(backToPlayer);
        inputManager.enabled = true;
        playerModel.enabled = false;
        SwitchCamera.Instance.SwitchCameraMethod(ultCam, pCam, 0.1f);
    }

    private void DisableAnimate()
    {
        ultimate.GetComponent<Animator>().SetBool("Ult", false);
        enabled = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        ultimate.SetActive(false);
    }

    IEnumerator HealOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(healTimeFrequency);
            if (HealthSystem.Instance.isDeath)
            {
                break;
            }
            HealthSystem.Instance.TakeDamage(-healTimeFrequency * healPerSecond);
        }
        enabled = false;
    }
}
