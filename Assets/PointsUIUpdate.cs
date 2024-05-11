using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsUIUpdate : MonoBehaviour
{
    private TextMeshProUGUI promptText;
    private readonly string pointsName = "Points: ";

    private void Start()
    {
        promptText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (GameManager.INSTANCE != null)
        {
            promptText.text = pointsName + GameManager.INSTANCE.point.ToString();
        }
    }
}
