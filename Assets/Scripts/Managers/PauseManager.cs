using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {

	public bool isPaused = false;
	[SerializeField] private Canvas pauseCanvas;

    public void Pause()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        isPaused = !isPaused;
		if (isPaused)
		{
            pauseCanvas.enabled = true;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
		} else
		{
            pauseCanvas.enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
		Debug.Log("Game is Paused ("+isPaused+")");
		EventManager.TriggerEvent("Pause", isPaused);
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
