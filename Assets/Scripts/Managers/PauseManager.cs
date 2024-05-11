using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {

	public static PauseManager INSTANCE;
	public bool isPaused = false;
	[SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
		if(INSTANCE == null){
			INSTANCE = this;
			// pauseMenu = GameObject.Find("PauseMenu");
			// if (pauseMenu == null) { Debug.Log("Pause Menu object is not found in scene"); return; }
			// pauseMenu.SetActive(false);
		}else{
			Destroy(gameObject);
		}
    }

    public void Pause()
	{
        isPaused = !isPaused;
        EventManager.TriggerEvent("Pause", isPaused);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		if(isPaused){
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;

			GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().enabled = false;
		}else{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.None;

			GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().enabled = true;
		}

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
