using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ClosingRedirect : MonoBehaviour
{
    PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        playableDirector = gameObject.GetComponent<PlayableDirector>();
        if (playableDirector == null) { Debug.Log("Director Component is missing");  return;  }
        playableDirector.stopped += ctx => SceneManager.LoadScene("Main Menu");
    }
}
