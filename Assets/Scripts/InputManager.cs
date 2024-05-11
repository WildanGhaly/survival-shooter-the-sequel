using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

public class InputManager : MonoBehaviour
{
    private PlayerLook look;
    private PlayerMovement move;
    private PlayerHealth health;
    private PlayerInput inputActions;
    public PlayerInput.OnFootActions onFoot;
    public PlayerShooting playerShooting;

    public bool isFirstPerson = true;
    public bool isThirdPerson;
    public bool isTopDown;

    public enum CameraMode
    {
        FirstPerson,
        ThirdPerson,
        TopDown
    }

    private CameraMode currentCameraMode;

    [SerializeField] private GameObject mobileButton;
    [SerializeField] private GameObject gun;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

#if UNITY_IOS || UNITY_ANDROID
        mobileButton.SetActive(true);
#else
        mobileButton.SetActive(false);
#endif

        health = GetComponent<PlayerHealth>();
        look = GetComponent<PlayerLook>();
        inputActions = new PlayerInput();
        move = GetComponent<PlayerMovement>();


        onFoot = inputActions.OnFoot;

        onFoot.Jump.performed += ctx => move.Jump();

        onFoot.Sprint.performed += ctx => {
            if (onFoot.Movement.ReadValue<Vector2>() != Vector2.zero)
            {
                move.StartSprint();
            }
        };
        onFoot.Sprint.canceled += ctx => move.StopSprint();

        onFoot.FirstPerson.performed += ctx => SetCameraMode(CameraMode.FirstPerson);
        onFoot.ThirdPerson.performed += ctx => SetCameraMode(CameraMode.ThirdPerson);
        onFoot.IsometricTopDown.performed += ctx => SetCameraMode(CameraMode.TopDown);

        onFoot.Fire.performed += ctx => gun.GetComponent<PlayerShooting>().StartFire();
        onFoot.Fire.canceled += ctx => gun.GetComponent<PlayerShooting>().StopFire();

        onFoot.PrimaryWeapon.performed += ctx => gun.GetComponent<PlayerShooting>().ChangeWeapon(1);
        onFoot.SecondaryWeapon.performed += ctx => gun.GetComponent<PlayerShooting>().ChangeWeapon(2);
        onFoot.SwordWeapon.performed += ctx => gun.GetComponent<PlayerShooting>().ChangeWeapon(3);

        onFoot.SwitchWeapon.performed += ctx => gun.GetComponent<PlayerShooting>().RollWeapon();

        onFoot.Ultimate.performed += ctx => Ultimate();

        onFoot.Pause.performed += ctx => { FindObjectOfType<PauseManager>().Pause(); };
        onFoot.ToggleDebugCheat.performed += ctx => { FindObjectOfType<PauseManager>().Pause(); };
    }

    private void FixedUpdate()
    {
        if (!health.isDead)
            move.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!health.isDead)
            if (isThirdPerson || isFirstPerson)
            {
                look.ProcessLook(onFoot.FPSTPSLook.ReadValue<Vector2>());
            } else if (isTopDown)
            {
                look.ProcessLook(onFoot.IsometricTopDownLook.ReadValue<Vector2>());
            }
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }

    private void SetCameraMode(CameraMode mode)
    {
        currentCameraMode = mode;
        isFirstPerson = (mode == CameraMode.FirstPerson);
        isThirdPerson = (mode == CameraMode.ThirdPerson);
        isTopDown = (mode == CameraMode.TopDown);

        switch (mode)
        {
            case CameraMode.FirstPerson:
                look.SetFPSCam();
                break;
            case CameraMode.ThirdPerson:
                look.SetTPSCam();
                break;
            case CameraMode.TopDown:
                look.SetTopDownCam();
                break;
        }
    }

    private void Ultimate()
    {
        GetComponent<PlayerUltimate>().enabled = true;
    }
}
