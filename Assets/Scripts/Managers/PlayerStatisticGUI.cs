using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatisticGUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TextMeshProUGUI playerNameValue;
    [SerializeField] public TextMeshProUGUI killCountValue;
    [SerializeField] public TextMeshProUGUI distanceValue;
    [SerializeField] public TextMeshProUGUI hitRatioValue;
    [SerializeField] public TextMeshProUGUI timePlayedValue;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerNameValue.SetText(PlayerStatistic.INSTANCE.getPlayerName().ToString() + "'s statistics");
        killCountValue.SetText(PlayerStatistic.INSTANCE.getKillCount().ToString());
        distanceValue.SetText(PlayerStatistic.INSTANCE.getDistance().ToString());
        if(float.IsNaN(PlayerStatistic.INSTANCE.getHitRatio()))
        {
            hitRatioValue.SetText("0%");
        }else{
            hitRatioValue.SetText((PlayerStatistic.INSTANCE.getHitRatio() * 100).ToString() + "%");
        }
        timePlayedValue.SetText(((int) PlayerStatistic.INSTANCE.getTimePlayed()/60).ToString()+ ":"+((int)PlayerStatistic.INSTANCE.getTimePlayed()%60).ToString());
    }
}
