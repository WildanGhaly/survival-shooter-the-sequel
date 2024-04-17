using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xSensitivity = 30f;
    private float ySensitivity = 30f;
    private float tilt = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessLook(Vector2 look)
    {
        float vertical = look.y * ySensitivity * Time.deltaTime;
        float horizontal = look.x * xSensitivity * Time.deltaTime;

        tilt -= vertical;
        tilt = Mathf.Clamp(tilt, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(tilt, 0, 0);

        transform.Rotate(Vector3.up * horizontal);
    }
}
