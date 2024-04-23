using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInstance : MonoBehaviour
{
    public static BaseInstance Instance;

	[SerializeField] private float gunDamage = 20f;
	[SerializeField] private float multiplierGunDamage = 1f;
	[SerializeField] private float defaultSpeed = 6f;
	[SerializeField] private float normalSpeed = 6f;
	[SerializeField] private float sprintSpeed = 8f;
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
		return currentSpeed;
    }

	public void UpdateGunDamage(float damage)
    {
		gunDamage = damage;
    }

	public void HalfDamage()
    {
		gunDamage /= 2;
    }

	public void UpdadeNormalSpeed(float speedPercentage)
    {
		defaultSpeed = normalSpeed;
		normalSpeed += speedPercentage/100 * normalSpeed;
		currentSpeed = normalSpeed;
    }

	public void StartSprint()
    {
		currentSpeed = sprintSpeed;
    }

	public void StopSprint()
    {
		currentSpeed = normalSpeed;
    }

	public void ResetSpeed()
    {
		normalSpeed = defaultSpeed;
		currentSpeed = normalSpeed;
    }

	public void AddMultiplierGunDamage(float multiplier)
    {
		multiplierGunDamage += multiplier;
    }
}
