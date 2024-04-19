using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public string promptMessage = "Collide";
    public void BaseCollideEnter()
    {
        CollideEnter();
    }

    public void BaseCollideStay()
    {
        CollideStay();
    }

    public void BaseCollideExit()
    {
        CollideExit();
    }

    protected virtual void CollideEnter()
    {

    }

    protected virtual void CollideStay()
    {

    }

    protected virtual void CollideExit()
    {

    }
}
