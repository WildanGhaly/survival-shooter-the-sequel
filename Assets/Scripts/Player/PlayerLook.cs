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
                SetFPSCam();
                tilt = Mathf.Clamp(tilt, -80f, 80f);
            }
            else if (inputManager.isThirdPerson)
            {
                SetTPSCam();
                tilt = Mathf.Clamp(tilt, 10f, 40f);
            }
            transform.Rotate(Vector3.up * horizontal);
        }
        else
        {
            SetTopDownCam();
            TopDownLook(look);
            tilt = 50f;
        }
        cam.transform.localRotation = Quaternion.Euler(tilt, 0, 0);
    }

    // FIXME: Yang rotate playernya doang, gunnya kaga
    public void TopDownLook(Vector2 mousePosition)
    {
#if !MOBILE_INPUT
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerModel.transform.localRotation = newRotation;
        }
#else

                    Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X") , 0f , CrossPlatformInputManager.GetAxisRaw("Mouse Y"));

                    if (turnDir != Vector3.zero)
                    {
                        // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                        Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

                        // Ensure the vector is entirely along the floor plane.
                        playerToMouse.y = 0f;

                        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                        Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                        // Set the player's rotation to this new rotation.
                        playerRigidbody.MoveRotation(newRotatation);
                    }
#endif
    }

    public void SetFPSCam()
    {
        cam.transform.SetLocalPositionAndRotation(new Vector3(0f, 0.8f, 0), Quaternion.Euler(0, 0, 0));
    }
    public void SetTopDownCam()
    {
        cam.transform.SetLocalPositionAndRotation(new Vector3(0, 7, -5), Quaternion.Euler(50, 0, 0));
    }
    public void SetTPSCam()
    {
        cam.transform.SetLocalPositionAndRotation(new Vector3(0, 3, -5), Quaternion.Euler(45, 0, 0));
    }
}
