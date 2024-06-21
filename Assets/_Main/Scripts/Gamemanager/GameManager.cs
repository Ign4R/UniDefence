using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] GameObject gameOverScreen;
    public bool isGameOver;

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
