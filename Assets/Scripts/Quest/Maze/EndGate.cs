using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGate : Collidable
{
    [SerializeField] Animator gate;
    [SerializeField] private MazeMonster mazeMonster;
    [SerializeField] private GameObject cam;
    public GameObject nextCam;
    public GameObject player;
    private Nightmare.PlayerMovement move;

    protected override void CollideEnter()
    {
        base.CollideEnter();
        player = GameObject.FindGameObjectWithTag("Player");
        move = player.GetComponent<Nightmare.PlayerMovement>();
        gate.SetBool("isClosed", true);
        mazeMonster.StopMovement();
        DisableCharacterController();
    }
    public void DisableCharacterController()
    {
        StartCoroutine(DisableControllerAfterDelay(1.0f));
    }

    private IEnumerator DisableControllerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        InputManager inputManager = player.GetComponent<InputManager>();
        if (inputManager != null)
        {
            Debug.Log("Input Manager will now be disabled.");
            inputManager.enabled = false;
        }
        SkinnedMeshRenderer skinMeshRenderer = player.GetComponentInChildren<SkinnedMeshRenderer>();
        if (skinMeshRenderer != null)
        {
            skinMeshRenderer.enabled = true;
            Debug.Log("SkinnedMeshRenderer has been enabled.");
        }
        enabled = false;
        StartCoroutine(MoveLeft());
    }
    private void OnDisable()
    {
        Debug.Log("Start End Camera");
        SwitchCamera.Instance.SwitchCameraMethod(cam, nextCam, 0.5f);
    }
    IEnumerator MoveLeft()
    {
        float initialPositionX = player.transform.position.x;
        Debug.Log("Initial Player position: " + initialPositionX);
        player.transform.eulerAngles = new Vector3(0, 270, 0);

        while (player.transform.position.x > 130)
        {
            yield return new WaitForFixedUpdate();
            move.ProcessMove(Vector2.up);
        }
        player.transform.eulerAngles = new Vector3(0, 0, 0);

        Debug.Log("Initial Player position: z " + player.transform.position.z);
        while (player.transform.position.z < -70)
        {
            yield return new WaitForFixedUpdate();
            move.ProcessMove(Vector2.up);
        }
    }

}
