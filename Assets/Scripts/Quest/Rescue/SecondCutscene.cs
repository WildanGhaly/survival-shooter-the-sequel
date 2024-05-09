using System.Collections;
using UnityEngine;

public class SecondCutscene : MonoBehaviour
{
    [SerializeField] private GameObject enemyManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cam2;
    [SerializeField] private GameObject camUI;
    [SerializeField] private GameObject enemyManagerContinouos;
    [SerializeField] private GameObject finishGame;
    [SerializeField] private Animator gate;


    private readonly string[,] dialogues = new string[,]
    {
        {"Chatter", "YES!!"},
        {"Chatter", "You find the pet!"},
        {"Chatter", "Now, GO BACK TO THE HOLE WITH THE PET"},
        {"Chatter", "HURRY"},
    };

    public void SecondScenePlay()
    {
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        enemyManager.SetActive(false);
        SwitchCamera.Instance.SwitchCameraMethod(camUI, cam2, 0.5f);
        player.SetActive(false);
        yield return new WaitForSeconds(1);
        Conversation.Instance.StartConversation(dialogues);
        
        yield return new WaitForSeconds(18);
        player.SetActive(true);
        SwitchCamera.Instance.SwitchCameraMethod(camUI, cam2, 0.5f);
        enemyManager.SetActive(true);
        finishGame.SetActive(true);
    }
}