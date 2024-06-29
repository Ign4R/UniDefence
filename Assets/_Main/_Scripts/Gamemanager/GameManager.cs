using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public EnemySpawnController enemySpawner;
    public Player player;

    [Header("Screens")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject winOverScreen;
    [SerializeField] GameObject ScreenUpgrade;

    [Header("Progress Wave")]
    [SerializeField] private float countMax=2;
    [SerializeField] private float waveMax=10;
    private float countCurr;
    [SerializeField] Image progressBarObj;

    [Header("Weapon UI")]
    [SerializeField] private Image weaponsIcon;

    [Header("Sound")]
    [SerializeField] AudioSource gameOverClip;
    private int waves;

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
        enemySpawner.StopSpawn();
        player.Destroy();
        AudioManager.instance.Play("GameOver");
        gameOverScreen.SetActive(true);
    }

    public void EndWave()
    {
        waves++;
        countCurr = 0;
        UpdateProgressBar();
        if (waves >= waveMax)
        {
            WinGame();
        }
        else
        {
            ScreenUpgrade.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void NextWave()
    {
 
        countMax *= 2;
        ScreenUpgrade.SetActive(false);
        enemySpawner.ReduceSpawnRate();
        Time.timeScale = 1f;

    }
    void WinGame()
    {
        enemySpawner.StopSpawn();
        enemySpawner.DestroyEnemies();
        player.Destroy();
        winOverScreen.SetActive(true);
    }
    public void UpdateInfo(Sprite weapon)
    {
        weaponsIcon.sprite = weapon;
    }
}
