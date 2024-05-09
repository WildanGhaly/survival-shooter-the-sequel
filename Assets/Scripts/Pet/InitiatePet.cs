using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiatePet : MonoBehaviour
{
    void Start()
    {
        GameManager.INSTANCE.InitiatePet();
    }
}
