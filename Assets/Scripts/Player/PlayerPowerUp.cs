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
    private float currentMultiplier = 2f;

    public static PlayerPowerUp Instance;

    private int multiplierCount = 0;
    [SerializeField] private int maxIncreaseDamageCount = 15;

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
                BuffIconInstance.Instance.DisableSpeedBuff();
            }
        }

        if (isIncreaseDamage)
        {
            increaseDamageTime += Time.deltaTime;
            if (increaseDamageDuration <= increaseDamageTime)
            {
                isIncreaseDamage = false;
                increaseDamageTime = 0;
                BaseInstance.Instance.UpdateGunDamage(BaseInstance.Instance.GetGunDamage() / currentMultiplier);
                BuffIconInstance.Instance.DisableDamageBuff();
            }
        }
    }

    public void IncreaseSpeedPowerUp(float normalSpeed)
    {
        isIncreaseSpeed = true;
        increaseSpeedTime = 0;
        BaseInstance.Instance.UpdadeNormalSpeed(normalSpeed);
        BuffIconInstance.Instance.EnableSpeedBuff();
    }

    public void IncreaseDamagePowerUp(int multiplier)
    {
        isIncreaseDamage = true;
        increaseDamageTime = 0;
        BaseInstance.Instance.UpdateGunDamage(BaseInstance.Instance.GetGunDamage() * multiplier);
        BuffIconInstance.Instance.EnableDamageBuff();
        currentMultiplier = multiplier;
    }

    public void PermanentDamagePowerUp(float multiplier)
    {
        if (multiplierCount < 15)
        {
            multiplierCount++;
            BaseInstance.Instance.AddMultiplierGunDamage(multiplier);
        }
    }
}
