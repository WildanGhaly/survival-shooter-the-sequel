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
        Debug.Log("Level Pause");
    }
    public void ContinueGame()
    {
        FindObjectOfType<PauseManager>().Pause();
        Debug.Log("Level Continue");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindObjectOfType<PauseManager>().Pause();
        Debug.Log("Level Restart");
    }
    public void ExitLevel()
    {
        // diasumsikan ke main level/main menu
        SceneManager.LoadScene(0);
        FindObjectOfType<PauseManager>().Pause();
        Debug.Log("Level Exit");
    }
}