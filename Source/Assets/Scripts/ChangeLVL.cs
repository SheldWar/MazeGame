using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ChangeLVL : MonoBehaviour
{
    public Text lvlText;
    public GameObject wall;
    public GameObject endRock;

    public int startMazeSize = 6;
    private const string levelKey = "CurrentLevel";
    private int currentLevel = 1;
    private Animator animator;

    void Start()
    {
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();

        currentLevel = PlayerPrefs.HasKey(levelKey) ? PlayerPrefs.GetInt(levelKey) : currentLevel;
        lvlText.text = "LEVEL " + currentLevel;
    }

    void Update()
    {
        if (animator.GetBool("isTwerking"))
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                LoadLevel();
            } else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (currentLevel == 29)
                {
                    currentLevel = 1;
                } else
                {
                    currentLevel++;
                }
                PlayerPrefs.SetInt(levelKey, currentLevel);
                LoadLevel();
            }
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey(levelKey);
    }
}
