using System.Collections;
using UnityEngine;

public class CloseToPet : MonoBehaviour
{
    private readonly string[,] dialogues = new string[,]
    {
        {"Chatter", "The pet is in proximity"},
        {"Chatter", "You can do it!!"},
    };

    public void Prompt()
    {
        StartCoroutine(FirstCutscene());
    }

    IEnumerator FirstCutscene()
    {
        Conversation.Instance.StartConversation(dialogues);
        yield return new WaitForSeconds(0);
    }
}