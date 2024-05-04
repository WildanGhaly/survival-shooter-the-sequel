using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerStatistic : MonoBehaviour
{
    [NonSerialized] public TextMeshProUGUI playerNameValue;
    [NonSerialized] public TextMeshProUGUI killCountValue;
    [NonSerialized] public TextMeshProUGUI distanceValue;
    [NonSerialized] public TextMeshProUGUI hitRatioValue;
    [NonSerialized] public TextMeshProUGUI timePlayedValue;
    public static PlayerStatistic INSTANCE;
    [SerializeField] private string playerName = "Player";
    [SerializeField] private float distanceReached = 0.0f;
    protected float updateCountdown = 5f; 
    [SerializeField] private int enemiesKilled = 0;
    [SerializeField] private float time;
    [SerializeField] private int bulletsShot;
    [SerializeField] private int bulletsHit;
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
        playerNameValue.SetText(getPlayerName().ToString() + "'s statistics");
        killCountValue.SetText(getKillCount().ToString());
        distanceValue.SetText(getDistance().ToString());
        hitRatioValue.SetText(getHitRatio().ToString());
        timePlayedValue.SetText(((int) getTimePlayed()/60).ToString()+ ":"+((int)getTimePlayed()%60).ToString());
    }

    public void setPlayerName(string name)
    {
        this.playerName = name;
    }

    public string getPlayerName()
    {
        return playerName;
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
        return (float) bulletsHit/ (float) bulletsShot;
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
