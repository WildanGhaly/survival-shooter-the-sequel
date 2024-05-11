using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameUI : MonoBehaviour
{
    private TextMeshProUGUI promptText;

    private void Start()
    {
        promptText = GetComponent<TextMeshProUGUI>();

        if (GameManager.INSTANCE != null)
        {
            promptText.text = GameManager.INSTANCE.playerName;
        }
    }
}
