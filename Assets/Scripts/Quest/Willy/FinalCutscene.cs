using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject crosshair;

    private CharacterController characterController;
    private InputManager inputManager;
    private SkinnedMeshRenderer meshRenderer;
    private Nightmare.PlayerMovement move;

    [SerializeField] private Animator finalGateOpen;

    private void Awake()
    {
        characterController = player.GetComponent<CharacterController>();
        inputManager = player.GetComponent<InputManager>();
        meshRenderer = playerModel.GetComponent<SkinnedMeshRenderer>();
        move = player.GetComponent<Nightmare.PlayerMovement>();
    }

    private void OnEnable()
    {
        crosshair.SetActive(false);
        characterController.enabled = false;
        inputManager.enabled = false;
        meshRenderer.enabled = true;
        player.transform.localPosition = new Vector3(-130, -4, -150);
        player.transform.localRotation = Quaternion.Euler(0, 255, 0);
        StartCoroutine(OpenFinalGate());
    }

    IEnumerator MoveToFinish()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            move.ProcessMove(Vector2.up);
        }
    }

    IEnumerator OpenFinalGate()
    {
        yield return new WaitForSeconds(3f);
        characterController.enabled = true;
        finalGateOpen.SetBool("isOpen", true);
        finalGateOpen.GetComponent<AudioSource>().Play();
        StartCoroutine(MoveToFinish());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
