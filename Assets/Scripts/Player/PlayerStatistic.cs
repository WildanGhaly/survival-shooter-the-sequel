using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistic : MonoBehaviour
{
    public static PlayerStatistic INSTANCE;
    private float distanceReached = 0.0f;
    protected float updateCountdown = 5f; 
    private int enemiesKilled = 0;
    private float time;
    private int bulletsShot;
    private int bulletsHit;
    private Vector3 previousPosition;
    private Vector3 currentPosition;

    void Awake()
    {
        if(INSTANCE == null){
            INSTANCE = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
        time = 0f;
        StartCoroutine(CalculateDistance(updateCountdown));
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public void addKill(){
        enemiesKilled++;
    }

    public int getKillCount(){
        return enemiesKilled;
    }

    public float getDistance(){
        return distanceReached;
    }

    public float getTimePlayed(){
        return time;
    }

    public void setBulletHit(int hit){
        this.bulletsHit = hit;
    }

    public void setBulletFired(int fired){
        this.bulletsShot = fired;
    }

    public float getHitRatio(){
        return bulletsHit/bulletsShot;
    }

    protected virtual IEnumerator CalculateDistance(float count){
        currentPosition = transform.position;

        float distance = Vector3.Distance(previousPosition, currentPosition);

        previousPosition = currentPosition;

        distanceReached += distance;

        Debug.Log(distanceReached);

        yield return new WaitForSeconds(count);

        StartCoroutine(CalculateDistance(updateCountdown));
    }
}
