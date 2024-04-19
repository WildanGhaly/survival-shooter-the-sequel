using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private float distance = 3f;
    private InputManager inputManager;
    private PlayerUI playerUI;

    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        inputManager = GetComponent<InputManager>();
        playerUI = GetComponent<PlayerUI>();
    }

    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, distance, mask))
        {
            Interactable interactable = raycastHit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                playerUI.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
