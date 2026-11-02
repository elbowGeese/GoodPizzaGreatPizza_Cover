using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    [Header("Daily Timer")]
    public TMP_Text textTimer;
    public Slider sliderTimer;
    public Image sliderTimerFill;
    public Color dayStartColor, dayEndColor;
    public float timeInDay = 60f;
    private float timer;
    private Coroutine blinkCoroutine;
    public float blinkTime = 2f;


    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        UpdateTimer();
    }

    // hours of operation 12pm - 9pm
    // displays time in 15min intervals
    // each hour is 4 chunks x 9 hours = 28 text possible text updates
    private void UpdateTimer()
    {
        timer += Time.deltaTime;

        float step = timer / timeInDay;

        // text timer
        float clock = Mathf.Lerp(0f, 9f, step);
        int hour = Mathf.FloorToInt(clock);
        if(hour == 0) { hour = 12; }
        int minute = Mathf.FloorToInt((clock % 1f) * 100f);
        minute = (int) Mathf.Lerp(0f, 60f, (float) minute / 100f);
        int minDivisibleBy15 = (minute / 15) * 15;
        textTimer.text = $"{hour}:{minDivisibleBy15:D2}PM";

        if (hour == 8 && blinkCoroutine == null) 
        {
            blinkCoroutine = StartCoroutine(BlinkTimerTextRoutine());
        }

        if(timer >= timeInDay && blinkCoroutine != null) 
        {
            StopCoroutine(blinkCoroutine);
            textTimer.color = Color.red; 
            // play closing sfx
        }

        // slider timer
        float sliderValue = 1f - Mathf.Clamp01(step);
        sliderTimer.value = sliderValue;
        sliderTimerFill.color = Color.Lerp(dayStartColor, dayEndColor, step);
    }

    IEnumerator BlinkTimerTextRoutine()
    {
        while (true)
        {
            textTimer.color = Color.red;
            yield return new WaitForSeconds(blinkTime);
            textTimer.color = Color.white;
            yield return new WaitForSeconds(blinkTime);
        }
    }
}
