using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCollide : MonoBehaviour
{
    [SerializeField] AudioSource aud;
    [SerializeField] AudioSource aud2;
    public void CollideEnter()
    {
        aud.Stop();
        aud2.Play();
    }
}
