using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    [SerializeField] private bool isIncreaseSpeed;
    [SerializeField] private bool isIncreaseDamage;

    [SerializeField] private float increaseSpeedDuration = 10;
    [SerializeField] private float increaseDamageDuration = 10;

    public static PlayerPowerUp Instance;

    private int multiplierCount = 0;
    [SerializeField] private int maxIncreaseDamageCount = 15;

    void Awake()
    {
        Instance = this;
    }

    public void IncreaseSpeedPowerUp(float duration, float speedPercentage)
    {
        StartCoroutine(HandleSpeedIncrease(duration, speedPercentage));
    }

    private IEnumerator HandleSpeedIncrease(float duration, float speedPercentage)
    {
        BaseInstance.Instance.UpdadeNormalSpeed(speedPercentage);
        BuffIconInstance.Instance.EnableSpeedBuff();
        yield return new WaitForSeconds(duration);
        BaseInstance.Instance.ResetSpeed();
        BuffIconInstance.Instance.DisableSpeedBuff();
    }

    public void IncreaseDamagePowerUp(float duration, int multiplier)
    {
        StartCoroutine(HandleDamageIncrease(duration, multiplier));
    }

    private IEnumerator HandleDamageIncrease(float duration, int multiplier)
    {
        float originalDamage = BaseInstance.Instance.GetGunDamage();
        BaseInstance.Instance.UpdateGunDamage(originalDamage * multiplier);
        BuffIconInstance.Instance.EnableDamageBuff();
        yield return new WaitForSeconds(duration);
        BaseInstance.Instance.UpdateGunDamage(originalDamage);
        BuffIconInstance.Instance.DisableDamageBuff();
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
