
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PressKeyOpenDoor : Collidable
{
    public GameObject Instruction;
    public GameObject CutscenePlay;
    public bool Action = false;

    void Start()
    {
        Instruction.SetActive(false);
    }

    protected override void CollideEnter()
    {
        base.CollideEnter();
        {
            Instruction.SetActive(true);
            Action = true;
        }
    }

    protected override void CollideExit()
    {
        base.CollideExit();
        Instruction.SetActive(false);
        Action = false;
    
    }


    protected override void CollideStay()
    {
        base.CollideStay();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {
                Instruction.SetActive(false);
                Action = false;
            }
        }
    }
}
