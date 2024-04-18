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
    public bool isTopdownPerson;

    [SerializeField] private GameObject gun;

    // Start is called before the first frame update
    void Awake()
    {
        health = GetComponent<PlayerHealth>();
        look = GetComponent<PlayerLook>();
        cam = look.cam;
        inputActions = new PlayerInput();
        move = GetComponent<PlayerMovement>();
        
        
        onFoot = inputActions.OnFoot;

        onFoot.Jump.performed += ctx => move.Jump();
        onFoot.Sprint.performed += ctx => move.StartSprint();
        onFoot.Sprint.canceled += ctx => move.StopSprint();

        onFoot.FirstPerson.performed += ctx => firstPerson();
        onFoot.ThirdPerson.performed += ctx => thirdPerson();
        onFoot.FrontPerson.performed += ctx => frontPerson();

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
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }

    private void firstPerson()
    {
        cam.transform.localPosition = new Vector3(0.38f, 0.8f, 0);
        cam.transform.localRotation = Quaternion.Euler(0, 0, 0);
        isFirstPerson = true;
        isThirdPerson = false;
        isTopdownPerson = false;
    }

    private void thirdPerson()
    {
        cam.transform.localPosition = new Vector3(0, 2, -5);
        cam.transform.localRotation = Quaternion.Euler(10, 0, 0);
        isFirstPerson = false;
        isThirdPerson = true;
        isTopdownPerson = false;
    }

    private void frontPerson()
    {
        cam.transform.localPosition = new Vector3(0, 5, -5);
        cam.transform.localRotation = Quaternion.Euler(30, 0, 0);
        isFirstPerson = false;
        isThirdPerson = false;
        isTopdownPerson = true;
    }
}
