using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] private int targetIndex = 1;
    void Start()
    {
        SceneManager.LoadScene(targetIndex);
    }
}
