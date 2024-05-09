using System.Collections;
using UnityEngine;

public class FirstCutscene : MonoBehaviour
{
    [SerializeField] GameObject enemyManager;
    [SerializeField] InputManager inputManager;
    [SerializeField] GameObject cam1;
    [SerializeField] GameObject camUI;

    private readonly string[,] dialogues = new string[,]
    {
        {"Player" , "Where am I?"},
        {"Chatter", "Now, you're in a mystic island"},
        {"Chatter", "This is where the King imprison one of the rarest pet"},
        {"Chatter", "You must find the pet and bring it back to the mainland"},
        {"Chatter", "Ohh shoot! The guards heard us!"},
        {"Chatter", "GO FIND THE PET!!"},
        {"Player", "OKAY OKAY!"},
    };

    void Start()
    {
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        inputManager.enabled = false;
        Conversation.Instance.StartConversation(dialogues);
        yield return new WaitForSeconds(27);
        SwitchCamera.Instance.SwitchCameraMethod(cam1, camUI, 0.5f);

        inputManager.enabled = true;
        enemyManager.SetActive(true);
    }
}