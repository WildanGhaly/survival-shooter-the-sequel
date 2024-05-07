using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondDoorInteractable : Interactable
{
    [SerializeField] private GameObject pCam, cam1;
    [SerializeField] private GameObject player, playerModel;

    bool isTriggered = false;

    protected override void Interact()
    {
        SwitchCamera.Instance.SwitchCameraMethod(pCam, cam1, 0.2f);
        base.Interact();
    }

    IEnumerator TransformPlayer()
    {
        DisablePlayer(true);
        yield return null;
        player.transform.localPosition = new Vector3(36, -1, 18);
        player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(2);

        while (!isTriggered)
        {
            player.GetComponent<Nightmare.PlayerMovement>().ProcessMove(Vector2.up * 0.5f);
        }

    }

    void DisablePlayer(bool disable)
    {
        player.GetComponent<InputManager>().enabled = !disable;
        player.GetComponent<CharacterController>().enabled = !disable;
        playerModel.GetComponent<SkinnedMeshRenderer>().enabled = disable;
    }

}
