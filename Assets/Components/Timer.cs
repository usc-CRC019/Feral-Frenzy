using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header ("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentSeconds;
    public int currentMinutes;
    public int currentHours;
    public bool countDown;
    public bool isActive;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    private bool hitMinute;
    private bool hitHour;

     void Start()
    {
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00");
        timeFormats.Add(TimerFormats.Dbl, "00");
        timeFormats.Add(TimerFormats.DblDbl, "00.00");

        timerText.alpha = 0;
        hitMinute = false;
        hitHour = false;
        isActive = false;
    }

 
    void Update()
    {
        // Functionality for timer - can count down or count up, can set limit or run infinitely
        if (isActive)
        {
            currentSeconds = countDown ? currentSeconds -= Time.deltaTime : currentSeconds += Time.deltaTime;
            timerText.alpha = 255;
        }
        

        if (currentSeconds >= 60)
        {
            currentMinutes++;
            hitMinute = true;
            currentSeconds = 0;
        }

        if (currentMinutes >= 60)
        {
            currentHours++;
            hitHour = true;
            currentMinutes = 0;
        }


        if(hasLimit && ((countDown && currentSeconds <= timerLimit) || (!countDown && currentSeconds >= timerLimit)))
        {
            currentSeconds = timerLimit;
            SetTimerText();
            enabled = false;
        }

        SetTimerText();

    }

    private void SetTimerText()
    {
        //Functionality for decimal formatting

        //timerText.text = hasFormat ? currentSeconds.ToString(timeFormats[format]) : currentSeconds.ToString();

        if (hasFormat)
        {
            if (hitMinute)
            {
                timerText.text = currentMinutes.ToString(timeFormats[TimerFormats.Dbl]) + ":" + currentSeconds.ToString(timeFormats[TimerFormats.Dbl]);
            }
            else if (hitHour)
            {
                timerText.text = currentHours.ToString(timeFormats[TimerFormats.Dbl]) + ":" + currentMinutes.ToString(timeFormats[TimerFormats.Dbl]) + ":" + currentSeconds.ToString(timeFormats[TimerFormats.Dbl]);
            }
            else
            {
                timerText.text = currentSeconds.ToString(timeFormats[format]);
            }
        }
        else
        {
            timerText.text = currentHours.ToString(timeFormats[TimerFormats.Dbl]) + ":" + currentMinutes.ToString(timeFormats[TimerFormats.Dbl]) + ":" + currentSeconds.ToString();
        }

        

    }

    public void RestartTimer()
    {
        isActive = false;
        hitMinute = false;
        hitHour = false;
        currentSeconds = 0f;
        currentMinutes = 0;
        currentHours = 0;
    }
}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundrethsDecimal,
    Dbl,
    DblDbl,
}
