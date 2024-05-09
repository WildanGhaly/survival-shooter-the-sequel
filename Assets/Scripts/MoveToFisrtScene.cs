using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToFisrtScene : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(startScene());    
    }

    IEnumerator startScene()
    {
        yield return new WaitForSeconds(27f);
        SceneManager.LoadScene(2);
    }
}
