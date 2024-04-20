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
    private Camera cam;
    public bool isFirstPerson = true;
    public bool isThirdPerson;
    public bool isTopDown;

    [SerializeField] private GameObject mobileButton;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject crosshair;

    // Start is called before the first frame update
    void Awake()
    {
        // TODO: ni masi gagal di android
#if UNITY_IOS || UNITY_ANDROID
        mobileButton.SetActive(true);
#else
        mobileButton.SetActive(false);
#endif

        health = GetComponent<PlayerHealth>();
        look = GetComponent<PlayerLook>();
        cam = look.cam;
        inputActions = new PlayerInput();
        move = GetComponent<PlayerMovement>();
        
        
        onFoot = inputActions.OnFoot;

        onFoot.Jump.performed += ctx => move.Jump();
        onFoot.Sprint.performed += ctx => move.StartSprint();
        onFoot.Sprint.canceled += ctx => move.StopSprint();

        onFoot.FirstPerson.performed += ctx => FirstPerson();
        onFoot.ThirdPerson.performed += ctx => ThirdPerson();
        onFoot.IsometricTopDown.performed += ctx => TopDown();

        onFoot.Fire.performed += ctx => gun.GetComponent<PlayerShooting>().StartFire();
        onFoot.Fire.canceled += ctx => gun.GetComponent<PlayerShooting>().StopFire();
        onFoot.FireGranat.performed += ctx => gun.GetComponent<PlayerShooting>().StartThrowGranat();
        onFoot.FireGranat.canceled += ctx => gun.GetComponent<PlayerShooting>().StopThrowGranat();
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

    private void FirstPerson()
    {
        look.SetFPSCam();
        isFirstPerson = true;
        isThirdPerson = false;
        isTopDown = false;
        crosshair.SetActive(true);
    }

    private void ThirdPerson()
    {
        look.SetTPSCam();
        isFirstPerson = false;
        isThirdPerson = true;
        isTopDown = false;
        crosshair.SetActive(true);
    }

    private void TopDown()
    {
        look.SetTopDownCam();
        isFirstPerson = false;
        isThirdPerson = false;
        isTopDown = true;
        crosshair.SetActive(false);
    }
}
