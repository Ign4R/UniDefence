using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Screens")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject endOverScreen;


    [Header("Progress Wave")]
    private float countMax;
    private float countCurr;
    [SerializeField] Image progressBarObj;


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
    private void Start()
    {
      
    }
    public void CheckProgressWave()
    {
        countCurr++;
        UpdateProgressBar();
        if (countCurr >= countMax)
        {
            EndWave();
        }
    }
    void UpdateProgressBar()
    {
        // Calcula el fillAmount basado en el progreso actual
        float fillValue = countCurr / countMax;

        // Aplica el fillAmount al componente Image
        progressBarObj.fillAmount = fillValue;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
    public void EndWave()
    {
        countCurr = 0;
        endOverScreen.SetActive(true);
    }




}
