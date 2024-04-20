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

	void Awake()
	{
		Instance = this;
	}

	public void UpdateGunDamage(int damage)
    {
		gunDamage = damage;
    }
}
