using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {

	public bool isPaused = false;
	[SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
		pauseMenu = GameObject.Find("PauseMenu");
		if (pauseMenu == null) { Debug.Log("Pause Menu object is not found in scene"); return; }
		pauseMenu.SetActive(false);
    }

    public void Pause()
	{
        isPaused = !isPaused;
        EventManager.TriggerEvent("Pause", isPaused);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		//if (isPaused)
		//{
		//	//TODO!!: Handle cursor visibility with 1st 3rd and topdown
		//	//FIXME!!: Pause Menu is clashing with UI Keyboard Controls
		//	pauseMenu.SetActive(true);
		//	Cursor.visible = true;
		//	Cursor.lockState = CursorLockMode.Confined;
		//} else
		//{
		//          pauseMenu.SetActive(false);
		//      }
		pauseMenu.SetActive(isPaused);
		Debug.Log("Game is Paused ("+isPaused+")");
    }

	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
