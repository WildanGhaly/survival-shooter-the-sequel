using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xSensitivity = 30f;
    private float ySensitivity = 30f;
    private float tilt = 0;
    public GameObject playerModel;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.isFirstPerson)
        {
            playerModel.SetActive(false);
        } 
        else
        {
            playerModel.SetActive(true);
        }
    }

    public void ProcessLook(Vector2 look)
    {
        float vertical = look.y * ySensitivity * Time.deltaTime;
        float horizontal = look.x * xSensitivity * Time.deltaTime;

        if (!inputManager.isTopDown)
        {
            tilt -= vertical;
            if (inputManager.isFirstPerson)
            {
                tilt = Mathf.Clamp(tilt, -80f, 80f);
            }
            else if (inputManager.isThirdPerson)
            {
                tilt = Mathf.Clamp(tilt, 10f, 40f);
            }
        }
        else
        {
            tilt = 40f;
        }
        cam.transform.localRotation = Quaternion.Euler(tilt, 0, 0);
        transform.Rotate(Vector3.up * horizontal);
    }
}
