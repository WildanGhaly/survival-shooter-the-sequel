using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public string promptMessage = "Collide";
    public bool eventCollideEnter;
    public bool eventCollideStay;
    public bool eventCollideExit;
    public void BaseCollideEnter()
    {
        if (eventCollideEnter)
        {
            GetComponent<CollisionEventEnter>().unityEvent.Invoke();
        }
        CollideEnter();
    }

    public void BaseCollideStay()
    {
        if (eventCollideStay)
        {
            GetComponent<CollisionEventStay>().unityEvent.Invoke();
        }
        CollideStay();
    }

    public void BaseCollideExit()
    {
        if (eventCollideExit)
        {
            GetComponent<CollisionEventExit>().unityEvent.Invoke();
        }
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
