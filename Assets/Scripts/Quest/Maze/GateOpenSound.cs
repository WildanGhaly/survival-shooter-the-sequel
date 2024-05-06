using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpenSound : MonoBehaviour
{
    AudioSource aud;
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playSound()
    {
        aud.Play();
    }
}
