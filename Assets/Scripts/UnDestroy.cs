using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] sceneManager = GameObject.FindGameObjectsWithTag("SceneManager"); 
        DontDestroyOnLoad(gameObject);
        if (sceneManager.Length > 1){
            Destroy(sceneManager[sceneManager.Length-1]);
        }
    }
}
