using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpInHell : MonoBehaviour
{
    [SerializeField] GameObject cam1;
    [SerializeField] GameObject cam2;

    [SerializeField] GameObject camUI;

    [SerializeField] GameObject enemyManager;

    [SerializeField] InputManager inputManager;

    [SerializeField] GameObject statueHealthBar;

    private readonly string[,] dialogues = new string[,]
    {
        {"Player", "What now? Where am I?"},
        {"Player", "That statue! I need to destroy it!"},
        // {"Player", "What should I do?"},
        // {"Chatter", "Open the gate slowly and get to the end of this path"},
        // {"Player", "Is that it? Is there any danger?"},
        // {"Chatter", "Enemies are asleep at this time, don't make sound and you will be fine!"},
        // {"Player", "Haha that should be easy, I mean what could go wrong. Right??"}
    };

    // Start is called before the first frame update
    void Start()
    {
        SwitchCamera.Instance.SimpleFade(0, 2f);
        StartCoroutine(cutscene1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator cutscene1()
    {
        inputManager.enabled = false;
        Conversation.Instance.StartConversation(dialogues);
        yield return new WaitForSeconds(3);
        SwitchCamera.Instance.SwitchCameraMethod(cam1, cam2, 0.5f);
        yield return new WaitForSeconds(4);
        SwitchCamera.Instance.SwitchCameraMethod(cam2, camUI, 0.5f);
        
        inputManager.enabled = true;
        statueHealthBar.SetActive(true);
        enemyManager.SetActive(true);
    }
}
