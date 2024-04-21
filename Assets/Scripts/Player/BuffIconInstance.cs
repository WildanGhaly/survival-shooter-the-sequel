using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffIconInstance : MonoBehaviour
{
    public static BuffIconInstance Instance;
    public GameObject increaseDamageIcon;
    public GameObject increaseSpeedIcon;

    private void Awake()
    {
        Instance = this;
    }

    public void EnableDamageBuff()
    {
        increaseDamageIcon.SetActive(true);
    }

    public void DisableDamageBuff()
    {
        increaseDamageIcon.SetActive(false);
    }

    public void EnableSpeedBuff()
    {
        increaseSpeedIcon.SetActive(true);
    }

    public void DisableSpeedBuff()
    {
        increaseSpeedIcon.SetActive(false);
    }
}
