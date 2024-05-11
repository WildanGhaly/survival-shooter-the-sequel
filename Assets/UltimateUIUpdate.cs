using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UltimateUIUpdate : MonoBehaviour
{
    private TextMeshProUGUI promptText;
    private readonly string ultName = "ULT: ";

    private void Start()
    {
        promptText = GetComponent<TextMeshProUGUI>();    
    }

    void Update()
    {
        if (GameManager.INSTANCE != null)
        {
            promptText.text = ultName + GameManager.INSTANCE.ultimateCount.ToString();
        }       
    }
}
