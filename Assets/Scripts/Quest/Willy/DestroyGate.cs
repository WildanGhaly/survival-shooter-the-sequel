using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyGate : MonoBehaviour
{
    [System.Serializable]
    public class GatePhase
    {
        public List<Animator> gates;
        public float delay;
    }

    [SerializeField] private List<GatePhase> phases;
    [SerializeField] private float timerGate = 120f;
    [SerializeField] Animator finalGate;
    [SerializeField] AudioSource audioSource;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] GameObject timerObject;

    private readonly string[,] dialogues = new string[,]
    {
        {"Chatter", "Gate is open, it will be closed after you enter"}
    };

    void Start()
    {
        StartCoroutine(StartDestroyGate());
        StartCoroutine(OpenSecondGate());
        StartCoroutine(StartTimer());
    }

    IEnumerator StartDestroyGate()
    {
        foreach (GatePhase phase in phases)
        {
            yield return new WaitForSeconds(phase.delay);

            foreach (Animator gate in phase.gates)
            {
                if (gate != null)
                {
                    gate.SetBool("destroy", true);
                    gate.GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    IEnumerator OpenSecondGate()
    {
        yield return new WaitForSeconds(timerGate);
        finalGate.SetBool("isOpen", true);
        finalGate.GetComponent<AudioSource>().Play();
    }

    IEnumerator StartTimer()
    {
        timerObject.SetActive(true);
        while (timerGate > 0)
        {
            timer.text = ((int)timerGate).ToString();
            yield return new WaitForSeconds(1);
            timerGate--;
        }
        timerObject.SetActive(false);
        Conversation.Instance.StartConversation(dialogues);
    }
}

