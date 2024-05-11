using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        InteractionEvent interaction = interactable.GetComponent<InteractionEvent>();
        if (interactable is EventOnlyInteractable)
        {
            interactable.promptMessage = EditorGUILayout.TextField("Prompt message", interactable.promptMessage);
            EditorGUILayout.HelpBox("INI HANYA UNTUK EVENT ONLY INTERACTABLE", MessageType.Info);
            if (interaction == null)
            {
                interactable.eventInteract = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            } 
        }
        else
        {
            base.OnInspectorGUI();
            if (interactable.eventInteract)
            {
                if (interaction == null)
                {
                    interactable.gameObject.AddComponent<InteractionEvent>();
                }
            }
            else
            {
                if (interaction != null)
                {
                    DestroyImmediate(interaction);
                }
            }
        }
    }
}
