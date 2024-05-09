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
    public static PlayerStatistic INSTANCE;
    [SerializeField] private string playerName = "Player";
    [SerializeField] private float distanceReached = 0.0f;
    protected float updateCountdown = 5f; 
    [SerializeField] private int enemiesKilled = 0;
    [SerializeField] private float time;
    [SerializeField] private int bulletsShot = 0;
    [SerializeField] private int bulletsHit = 0;

    void Awake()
    {
        if(INSTANCE == null){
            INSTANCE = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
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

    public void setEnemiesKilled (int kill)
    {
        enemiesKilled = kill;
    }

    public int getKillCount(){
        return enemiesKilled;
    }

    public float getDistance(){
        return distanceReached;
    }

    public void setDistance(float dist)
    {
        distanceReached = dist;
    }

    public void addDistance(float dist)
    {
        distanceReached += dist;
    }

    public float getTimePlayed(){
        return time;
    }

    public void setTimePlayed(float time)
    {
        this.time = time;
    }

    public void setBulletHit(int hit){
        this.bulletsHit = hit;
    }

    public void setBulletFired(int fired){
        this.bulletsShot = fired;
    }

    public void addBulletHit(){
        bulletsHit++;
    }

    public void addBulletFired(){
        bulletsShot++;
    }

    public float getHitRatio(){
        return (float) bulletsHit/ (float) bulletsShot;
    }
}
