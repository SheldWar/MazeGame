using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagsController : MonoBehaviour
{
    public Text flagText;
    public GameObject flag;
    public int flagsLimit;

    private int flagsRemaining;

    void Start()
    {
        var currentLvl = PlayerPrefs.HasKey("CurrentLevel") ? PlayerPrefs.GetInt("CurrentLevel") : 1;
        flagsRemaining = currentLvl - 1 + flagsLimit;
        flagText.text = flagsRemaining.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && flagsRemaining > 0)
        {
            createFlag();
        }
    }

    void createFlag()
    {
        Vector3 spawnPosition = transform.position + transform.TransformDirection(new Vector3(0f, 0f, 0.2f));
        spawnPosition.y = 15.602f;
        Instantiate(flag, spawnPosition, Quaternion.identity);
        flagsRemaining--;
        flagText.text = flagsRemaining.ToString();
    }

    public void flagRemoved()
    {
        flagsRemaining++;
        flagText.text = flagsRemaining.ToString();
    }
}
