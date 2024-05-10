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
    [SerializeField] public TextMeshProUGUI orbsCollectedValue;
    [SerializeField] public TextMeshProUGUI deathCountValue;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerNameValue.SetText(PlayerStatistic.INSTANCE.getPlayerName().ToString() + "'s statistics");
        killCountValue.SetText(PlayerStatistic.INSTANCE.getKillCount().ToString());
        distanceValue.SetText((PlayerStatistic.INSTANCE.getDistance() / 1000f).ToString("F2") + " km");
        deathCountValue.SetText(PlayerStatistic.INSTANCE.getDeathCount().ToString());
        orbsCollectedValue.SetText(PlayerStatistic.INSTANCE.getOrbsCollected().ToString());
        if(float.IsNaN(PlayerStatistic.INSTANCE.getHitRatio()))
        {
            hitRatioValue.SetText("0%");
        }else{
            hitRatioValue.SetText((PlayerStatistic.INSTANCE.getHitRatio() * 100).ToString("F2") + "%");
        }
        
        int totalSeconds = (int)PlayerStatistic.INSTANCE.getTimePlayed();
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        timePlayedValue.SetText($"{hours:D2}:{minutes:D2}:{seconds:D2}");
    }
}
