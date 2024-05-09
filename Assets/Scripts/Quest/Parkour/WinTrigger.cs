using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class WinTrigger : MonoBehaviour
{
    public PlayableDirector cutscene;
    public bool cutscenePlayed = false;

    [ContextMenu("Win")]
    private void ParkourLevelWin()
    {
        Debug.Log("Player has winned");
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
        cutscene.stopped += OnCutscenePlayed;
        cutscene.Play();
        cutscenePlayed = true;
    }

    private void OnCutscenePlayed(PlayableDirector director)
    {
        cutscene.stopped -= OnCutscenePlayed;
        if (cutscenePlayed)
        {
            SceneManager.LoadScene("Closing");
            cutscenePlayed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ParkourLevelWin();
    }
}
