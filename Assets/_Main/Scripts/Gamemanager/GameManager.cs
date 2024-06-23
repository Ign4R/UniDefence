using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public EnemySpawnController enemySpawner;

    [Header("Screens")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject ScreenUpgrade;

    [Header("Progress Wave")]
    [SerializeField] private float countMax=5;
    private float countCurr;
    [SerializeField] Image progressBarObj;

    [Header("Weapon UI")]
    [SerializeField] private Image weaponsIcon;

    [Header("Sound")]
    [SerializeField] AudioSource gameOverClip;

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

    public void CheckProgressWave()
    {
        countCurr++;
        print(countCurr);
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

        fillValue = Mathf.Clamp01(fillValue);

        // Aplica el fillAmount al componente Image
        progressBarObj.fillAmount = fillValue;
    }

    public void GameOver()
    {
        //gameOverClip.Play();
        AudioManager.instance.Play("GameOver");
        gameOverScreen.SetActive(true);
    }

    public void EndWave()
    {
        countCurr = 0;
        UpdateProgressBar();
        ScreenUpgrade.SetActive(true);
        Time.timeScale = 0f;
    }

    public void NextRound()
    {
        ScreenUpgrade.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void UpdateInfo(Sprite weapon)
    {
        weaponsIcon.sprite = weapon;
    }
}
