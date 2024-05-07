using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningMonologue : Subtitle
{
    private void Start()
    {
        string[,] openingMonologue = new string[,]
        {
            { "Player", "Ahh... Home Sweet Home" },
            { "Player", "I'm so tired playing army with my friends" },
            { "Player", "I'm just gonna go sleep" },
            { "Player", "So tired..." }
        };
        SetDialogue(openingMonologue);
    }
}