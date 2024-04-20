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

	public float normalSpeed
	{
		get;
		private set;
	} = 6;

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
    }
}
