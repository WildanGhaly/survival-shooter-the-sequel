using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameInteractable : Interactable
{
    protected override void Interact()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().enabled = false;
        base.Interact();
    }
}
