using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInstance : MonoBehaviour
{
    public static BaseInstance Instance;

	public int gunDamage
	{
		get;
		private set;
	} = 20;

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

	public void UpdateGunDamage(int damage)
    {
		gunDamage = damage;
    }

	public void UpdadeNormalSpeed(float speed)
    {
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
}
