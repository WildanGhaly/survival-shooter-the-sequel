using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptMessage;

    public void UpdateText(string text)
    {
        promptMessage.text = text;
    }
}
