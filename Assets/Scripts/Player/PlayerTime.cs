using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTime : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI timeDisplay;
    
    void Start()
    {
        StartCoroutine(TimePlayed(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual IEnumerator TimePlayed(float count){

        int minute = (int) PlayerStatistic.INSTANCE.getTimePlayed() / 60;
        int second = (int) PlayerStatistic.INSTANCE.getTimePlayed() % 60;

        timeDisplay.SetText(minute.ToString() + ":" + second.ToString());

        yield return new WaitForSeconds(count);

        StartCoroutine(TimePlayed(count));
    }
}
