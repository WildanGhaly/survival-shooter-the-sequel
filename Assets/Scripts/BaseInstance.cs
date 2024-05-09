using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInstance : MonoBehaviour
{
    public static BaseInstance Instance;

	[SerializeField] private float gunDamage = 20f;
	[SerializeField] private float multiplierGunDamage = 1f;
	[SerializeField] private float multiplierSpeed = 1f;
	[SerializeField] private float currentSpeed = 6f;

	void Awake()
	{
		Instance = this;
	}

	public float GetGunDamage()
    {
		return gunDamage * multiplierGunDamage;
    }

	public float GetCurrentSpeed()
    {
		return currentSpeed * multiplierSpeed;
    }

	public void AddMultiplierGunDamage(float multiplier)
    {
		multiplierGunDamage += multiplier;
		if (multiplierGunDamage <= 0) multiplierGunDamage = 0;
    }

	public void AddMultiplierSpeed(float multiplier)
    {
		multiplierSpeed += multiplier;
		if (multiplierSpeed <= 0) multiplierSpeed = 0;
	}

	public void ResetMultiplierGunDamage()
    {
		multiplierGunDamage = 1f;
    }

	public void ResetMultiplierSpeed()
    {
		multiplierSpeed = 1f;
    }
}
