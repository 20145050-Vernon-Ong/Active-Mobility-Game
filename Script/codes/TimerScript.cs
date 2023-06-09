using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;

    public TMP_Text Timertxt;

    // Start is called before the first frame update
    void Start()
    {
        TimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 20)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
                Time.timeScale = 1;
            }
            else if (TimeLeft > 0)
            {
                Timertxt.color = new Color(255, 0, 0);
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
                Time.timeScale = 1;
            }
            else
            {
                TimeLeft = 0;
                TimerOn = false;
                Time.timeScale = 0;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        Timertxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);

    }

}
