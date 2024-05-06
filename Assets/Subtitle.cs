using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subtitle : MonoBehaviour
{
    GameObject chatSubject;
    public void SetPlayerText(string text)
    {
        chatSubject = GameObject.Find("Player Text");
        if (chatSubject == null) { return; }
        chatSubject.GetComponent<TMPro.TMP_Text>().text = text;
        Debug.Log(chatSubject.GetComponent<TMPro.TMP_Text>().text);
    }
    public void SetChatterText(string text)
    {
        chatSubject = GameObject.Find("Chatter Text");
        if (chatSubject == null){ return; }
        chatSubject.GetComponent<TMPro.TMP_Text>().text = text;
        Debug.Log(chatSubject.GetComponent<TMPro.TMP_Text>().text);
    }
}
