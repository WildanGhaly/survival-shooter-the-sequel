using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameInteractable : Interactable
{
    protected override void Interact()
    {
        GameManager.INSTANCE.SaveGame();
        base.Interact();
    }
}
