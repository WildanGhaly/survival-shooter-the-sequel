using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInstance : MonoBehaviour
{
    public static BaseInstance Instance;

	[SerializeField] private float gunDamage = 20f;

	public float multiplierGunDamage
	{
		get;
		private set;
	} = 1;

	public float defaultSpeed
	{
		get;
		private set;
	} = 6;

	public float currentSpeed
	{
		get;
		private set;
	} = 6;

	public float normalSpeed
	{
		get;
		private set;
	} = 6;

	public float sprintSpeed
	{
		get;
		private set;
	} = 12;

	void Awake()
	{
		Instance = this;
	}

	public float GetGunDamage()
    {
		return gunDamage * multiplierGunDamage;
    }

	public void UpdateGunDamage(float damage)
    {
		gunDamage = damage;
    }

	public void HalfDamage()
    {
		gunDamage = gunDamage / 2;
    }

	public void UpdadeNormalSpeed(float speed)
    {
		defaultSpeed = normalSpeed;
		normalSpeed = speed;
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
