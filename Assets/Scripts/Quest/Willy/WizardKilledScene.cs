using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WizardKilledScene : MonoBehaviour
{
    [SerializeField] private float targetSink = 4f, sinkSpeed = 4f, waitBeforeSink = 3f;

    private void OnEnable()
    {
        StartCoroutine(SinkCorpse());
    }

    IEnumerator SinkCorpse()
    {
        Vector3 position = transform.position;
        GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(waitBeforeSink);
        while (targetSink > 0)
        {
            yield return null;
            float sinkAmount = Time.deltaTime * sinkSpeed;
            targetSink -= sinkAmount;
            position.y -= sinkAmount;
            transform.position = position;
        }
        Destroy(gameObject);
    }
}
