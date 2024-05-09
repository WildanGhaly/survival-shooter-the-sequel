using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteGameInteract : Interactable
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject cutSceneCam;
    [SerializeField] private GameObject enemyManagerContinouos;
    [SerializeField] private GameObject enemyManager;
    [SerializeField] private GameObject previewPet;
    [SerializeField] private GameObject freedPet;
    [SerializeField] private InputManager inputManager;
    private readonly string[,] dialogues = new string[,]
    {
        {"Chatter", "Nice, you saved the pet"},
        {"Chatter", "That pet can heal you while you in combat"},
        {"Chatter", "So you can use the pet for further quest!"},
        {"Chatter", "Congratulations!!, Now back to mainland"},
    };

    public void CompleteScenePlay()
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

        enemyManagerContinouos.SetActive(false);
        SwitchCamera.Instance.SwitchCameraMethod(playerCam, cutSceneCam, 0.5f);
        yield return new WaitForSeconds(1);
        player.SetActive(false);
        previewPet.SetActive(true);
        freedPet.SetActive(false);
        Conversation.Instance.StartConversation(dialogues);
        yield return new WaitForSeconds(15);
        SwitchCamera.Instance.SimpleFade(1, 0.5f);
        yield return new WaitForSeconds(1);
        GameManager.INSTANCE.addCoin(100);
        GameManager.INSTANCE.addPoint(200);
        GameManager.INSTANCE.updateCurrentQuestID(6);
        SceneManager.LoadScene(3);
    }
}
