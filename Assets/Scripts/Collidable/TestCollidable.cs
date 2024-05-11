using UnityEngine;

public class TestCollidable : Collidable
{
    protected override void CollideEnter()
    {
        Debug.Log("ENter");
    }

    protected override void CollideStay()
    {
        Debug.Log("Stay");
    }
}
