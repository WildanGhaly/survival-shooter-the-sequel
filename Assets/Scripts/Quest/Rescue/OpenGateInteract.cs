using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateInteract : Interactable
{
    [SerializeField] private Animator gate;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject cutSceneCam;
    [SerializeField] private GameObject enemyManagerContinouos;
    [SerializeField] private GameObject enemyManager;
    [SerializeField] private GameObject prisonedPet;
    [SerializeField] private GameObject freedPet;
    [SerializeField] private GameObject finishGame;
    [SerializeField] private InputManager inputManager;
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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        SwitchCamera.Instance.SwitchCameraMethod(playerCam, cutSceneCam, 0.5f);
        yield return new WaitForSeconds(1);
        player.SetActive(false);
        gate.Play("MiniGateOpen");
        Conversation.Instance.StartConversation(dialogues);
        
        yield return new WaitForSeconds(18);
        player.SetActive(true);
        SwitchCamera.Instance.SwitchCameraMethod(cutSceneCam, playerCam, 0.5f);
        enemyManagerContinouos.SetActive(true);
        prisonedPet.SetActive(false);
        finishGame.SetActive(true);
        freedPet.SetActive(true);

        

        gameObject.SetActive(false);
    }
}
