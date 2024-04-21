using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    [SerializeField] private bool isIncreaseSpeed;
    [SerializeField] private bool isIncreaseDamage;

    [SerializeField] private float increaseSpeedDuration = 10;
    [SerializeField] private float increaseDamageDuration = 10;

    private float increaseSpeedTime = 0;
    private float increaseDamageTime = 0;

    public static PlayerPowerUp Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (isIncreaseSpeed)
        {
            increaseSpeedTime += Time.deltaTime;
            if (increaseSpeedDuration <= increaseSpeedTime)
            {
                isIncreaseSpeed = false;
                increaseSpeedTime = 0;
                BaseInstance.Instance.ResetSpeed();
            }
        }

        if (isIncreaseDamage)
        {
            increaseDamageTime += Time.deltaTime;
            if (increaseDamageDuration <= increaseDamageTime)
            {
                isIncreaseDamage = false;
                increaseDamageTime = 0;
                BaseInstance.Instance.HalfDamage();
            }
        }
    }

    public void IncreaseSpeedPowerUp(float normalSpeed)
    {
        isIncreaseSpeed = true;
        increaseSpeedTime = 0;
        BaseInstance.Instance.UpdadeNormalSpeed(normalSpeed);
    }

    public void IncreaseDamagePowerUp()
    {
        isIncreaseDamage = true;
        increaseDamageTime = 0;
    }
}
