using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSceneManager : MonoBehaviour
{
    public static AllSceneManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
