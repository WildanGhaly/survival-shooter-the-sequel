using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCursor : MonoBehaviour
{
    public void DisableCursorMethod()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().enabled = true;
    }
}
