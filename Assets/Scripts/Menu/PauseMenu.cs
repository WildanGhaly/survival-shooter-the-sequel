using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [ContextMenu("Pause Game")]
    public void PauseGame()
    {
        FindObjectOfType<PauseManager>().Pause();
        gameObject.SetActive(true);
        Debug.Log("Level Pause");
    }
    public void ContinueGame()
    {
        FindObjectOfType<PauseManager>().Pause();
        gameObject.SetActive(false);
        Debug.Log("Level Continue");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Level Restart");
    }
    public void Cheats()
    {
        // TODO: not implemented
        Debug.Log("Cheats Menu");
    }
    public void ExitLevel()
    {
        // diasumsikan ke main level/main menu
        SceneManager.LoadScene(SceneManager.GetSceneByName("Main Menu").buildIndex);
        Debug.Log("Level Exit");
    }
}