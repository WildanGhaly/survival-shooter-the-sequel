using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private float countdown = 10f;

    private void Start()
    {
        StartCoroutine(DisappearAfterSeconds(countdown));
    }

    protected virtual IEnumerator DisappearAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
