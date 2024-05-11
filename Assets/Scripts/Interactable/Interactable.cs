using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool eventInteract;
    public string promptMessage = "Interact";
    public void BaseInteract()
    {
        if (eventInteract)
        {
            GetComponent<InteractionEvent>().unityEvent.Invoke();
        }
        Interact();
    }

    protected virtual void Interact()
    {

    }
}
