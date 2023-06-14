using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopTrigger : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas endLvlCanvas;
    public Text endLvlText;

    public GameObject confettiObject;
    public Text timerText;
    private bool isTriggered;

    public GameObject player;
    private Animator animator;

    private void Start()
    {
        animator = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var lvl = PlayerPrefs.HasKey("CurrentLevel") ? PlayerPrefs.GetInt("CurrentLevel") : 1;
        endLvlText.text = "Poziom " + lvl + " zostal ukonczony w " + timerText.text + " minut";
        endLvlCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            timerText.GetComponent<Timer>().stopTimer();
            confettiObject.SetActive(true);
            animator.SetBool("isTwerking", true);
        }
    }
}
