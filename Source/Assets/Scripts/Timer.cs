using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float timer;
    private bool isStarted;

    private void Start()
    {
        timer = 0f;
    }

    private void Update()
    {
        if (isStarted)
        {
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = timeString;
        }
    }

    public void startTimer()
    {
        isStarted = true;
    }

    public void stopTimer()
    {
        isStarted = false;
    }
}
