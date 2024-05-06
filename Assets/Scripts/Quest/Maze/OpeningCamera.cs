using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCamera : MonoBehaviour
{
    public GameObject cam2;
    private void FixedUpdate()
    {
        if (transform.position.y < 80)
        {
            Vector3 position = transform.position;
            position.y += Time.deltaTime * 20f;
            transform.position = position;
        }
        else
        {
            Debug.Log("[START] ELSE");
            enabled = false;
        }
    }

    private void OnDisable()
    {
        Debug.Log("[START] ON DISABLED");
        SwitchCamera.Instance.SwitchCameraMethod(gameObject, cam2, 0.5f);
    }
}
