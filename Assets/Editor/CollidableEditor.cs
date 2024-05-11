using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Collidable), true)]
public class CollidableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Collidable collidable = (Collidable)target;
        CollisionEvent collision = collidable.GetComponent<CollisionEvent>();
        if (collidable is EventOnlyCollidable)
        {
            EditorGUILayout.HelpBox("INI HANYA UNTUK EVENT ONLY COLLIDABLE", MessageType.Info);
        }
        
        base.OnInspectorGUI();
        
        CollisionEventEnter collisionEnter = collidable.GetComponent<CollisionEventEnter>();
        CollisionEventStay collisionStay = collidable.GetComponent<CollisionEventStay>();
        CollisionEventExit collisionExit = collidable.GetComponent<CollisionEventExit>();

        if (collidable.eventCollideEnter)
        {
            if (collisionEnter == null)
            {
                collidable.gameObject.AddComponent<CollisionEventEnter>();
            }
        }
        else
        {
            if (collision != null)
            {
                DestroyImmediate(collisionEnter);
            }
        }

        if (collidable.eventCollideStay)
        {
            if (collisionStay == null)
            {
                collidable.gameObject.AddComponent<CollisionEventStay>();
            }
        }
        else
        {
            if (collision != null)
            {
                DestroyImmediate(collisionStay);
            }
        }

        if (collidable.eventCollideExit)
        {
            if (collisionExit == null)
            {
                collidable.gameObject.AddComponent<CollisionEventExit>();
            }
        }
        else
        {
            if (collision != null)
            {
                DestroyImmediate(collisionExit);
            }
        }
        
    }
}
