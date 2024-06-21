using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("GameOver")]
    [SerializeField] GameObject gameOverScreen;
    public bool isGameOver;

    [Header("Timer")]
    public float timeValue = 90f;
    public TMP_Text timerText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(timeValue > 0f)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0f;
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0f)
        {
            timeToDisplay = 0;
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameOver()
    {
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        yield return new WaitForSeconds(0.15f); //Esperas un segundo.

        isGameOver = true;
        ShowGameOverScreen();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }
}
