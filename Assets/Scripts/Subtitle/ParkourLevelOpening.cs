using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourLevelOpening : Subtitle
{
    private void Start()
    {
        string[,] subtitle = new string[,]
        {
            { "Chatter", "Alright, this level should be easy" },
            { "Player", "What do I have to do?" },
            { "Chatter", "You just have to reach this flag" },
            { "Chatter", "Should be easy enough, right?" },
            { "Player", "Okay..." },
        };
        SetDialogue(subtitle);
    }
}
