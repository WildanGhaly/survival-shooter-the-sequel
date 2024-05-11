using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsUIUpdate : MonoBehaviour
{
    private TextMeshProUGUI promptText;
    private readonly string coinsName = "Coins: ";

    private void Start()
    {
        promptText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (GameManager.INSTANCE != null)
        {
            promptText.text = coinsName + GameManager.INSTANCE.coin.ToString();
        }
    }
}
