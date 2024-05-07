using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class yang mengadaptasi dari the one, the only,
/// Ghaly B
/// isinya cuman buat ngequeue percakapan dan pop setiap percakapan dicakapkan
/// </summary>
public class Subtitle : MonoBehaviour
{
    public static Subtitle Instance;

    GameObject chatSubject;
    string[,] dialogue; // adaptasi dari willy, tiap elemen berisi subjek dan ucapan
    Queue<string[]> dialogueQueue;

    private void Awake()
    {
        Instance = this;
    }

    public Subtitle()
    {
        this.dialogue = null;
    }

    public Subtitle(string[,] dialogue)
    {
        this.dialogue = dialogue;
        MakeDialogueQueue();
    }

    public void SetDialogue(string[,] dialogue)
    {
        this.dialogue = dialogue;
        MakeDialogueQueue();
    }

    public void MakeDialogueQueue()
    {
        dialogueQueue = new Queue<string[]>();
        for (int i = 0; i < dialogue.GetLength(0); i++) // TIL that foreach is iterating not just to rows but also the sub elems
        {
            string[] entry = new string[2];
            entry[0] = dialogue[i, 0];
            entry[1] = dialogue[i, 1];
            dialogueQueue.Enqueue(entry);
        }
    }

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

    public virtual void Next()
    {
        string[] next = dialogueQueue.Dequeue();
        if (next[0].Contains("Player"))
        {
            SetPlayerText(next[1]);
        } else
        {
            // Can be scaled to also handle different chat subject
            SetChatterText(next[1]);
        }
    }
}
