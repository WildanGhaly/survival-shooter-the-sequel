using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocation : MonoBehaviour
{
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    protected float updateCountdown = 5f; 
    private float distanceReached = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
        StartCoroutine(CalculateDistance(updateCountdown));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     protected virtual IEnumerator CalculateDistance(float count){
        currentPosition = transform.position;

        float distance = Vector3.Distance(previousPosition, currentPosition);

        previousPosition = currentPosition;

        distanceReached += distance;

        PlayerStatistic.INSTANCE.addDistance(distanceReached);

        yield return new WaitForSeconds(count);

        StartCoroutine(CalculateDistance(updateCountdown));
    }
}
