using UnityEngine;
public class PetShopInteractable : Interactable
{
    [SerializeField] private GameObject UI;

    protected override void Interact()
    {
        Debug.Log("Hello World");
        UI.SetActive(true);
        base.Interact();
    }
}
