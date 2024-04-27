using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollidable : Collidable
{
    [SerializeField] protected float countdown = 5f;
    [SerializeField] protected GameObject audioSource;

    void Start()
    {
        StartCoroutine(DisappearAfterSeconds(countdown));
    }

    protected override void CollideEnter()
    {
        audioSource.GetComponent<AudioSource>().Play();
        base.CollideEnter();
    }

    protected override void CollideStay()
    {
        base.CollideStay();
    }

    protected override void CollideExit()
    {
        base.CollideExit();
    }

    protected virtual IEnumerator DisappearAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
