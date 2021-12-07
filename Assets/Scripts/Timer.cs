using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    public float maxTime;
    float timer;
    public TextMeshProUGUI timerText;
    bool timerIsActive = false;
    public float decimalPlaces;
    public bool startTimerOnPlay;
    public bool stopTimerOnZero;
    public bool timerFinished;
    public TextMeshPro babyCountText;
    public TextMeshPro moneyText;
    public UnityEvent timerEnd = new UnityEvent();
    

    // Start is called before the first frame update
    void Start()
    {
        timer = maxTime;
        if (startTimerOnPlay)
        {
            timerIsActive = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsActive)
        {
            float dp = Mathf.Pow(10f, decimalPlaces);
            timer -= Time.deltaTime;
            float rounded = Mathf.Round(timer * dp) / dp;
            timerText.text = rounded.ToString();
        }

        if (stopTimerOnZero)
        {
            if(timer <= 0)
            {
                string txt = "0.";
                for (int i = 0; i < decimalPlaces; i++)
                {
                    timerText.text = txt += (i*0).ToString();
                }
                
                timerIsActive = false;
            }
        }

        if(timer <= 0)
        {
            timerFinished = true;
            timerEnd.Invoke();
            babyCountText.text = "YOU MADE " + GetComponent<BabyCount>().babyCount + " BABIES";
            moneyText.text = "AND MADE  \n £(INSERT MONEY VALUE)";
            Debug.Log("MONEY VALUE NEEDED HERE");
            timer = maxTime;
        }
    }

    public void startTimer()
    {
        timerIsActive = true;
    }

    public void stopTimer()
    {
        timerIsActive = false;
    }

    public void ResetTimer()
    {
        timerIsActive = true;
        timer = maxTime;
    }
}
