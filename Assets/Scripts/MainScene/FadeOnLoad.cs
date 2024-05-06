using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOnLoad : MonoBehaviour
{
    void Start()
    {
        SwitchCamera.Instance.SimpleFade(0, 1f);
    }
}
