using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Collidable collide = other.gameObject.GetComponent<Collidable>();
        if (collide != null)
        {
            collide.BaseCollideEnter();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Collidable collide = other.gameObject.GetComponent<Collidable>();
        if (collide != null)
        {
            collide.BaseCollideStay();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Collidable collide = other.gameObject.GetComponent<Collidable>();
        if (collide != null)
        {
            collide.BaseCollideExit();
        }
    }
}
