using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TimerTrigger : MonoBehaviour
{
    public Text timerText;
    private bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            timerText.GetComponent<Timer>().startTimer();
        }
    }
}
