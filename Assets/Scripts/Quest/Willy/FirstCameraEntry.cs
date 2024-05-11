using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCameraEntry : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 5f;

    private void FixedUpdate()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - cameraSpeed * Time.deltaTime);
    }
}
