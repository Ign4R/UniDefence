using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //int midLevel
    //int endLevel
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
    [SerializeField] private int waveCount;
    private float countCurr;
    [SerializeField] Image progressBarObj;
    [SerializeField] TMP_Text numWavesText;
    //public UnityEvent UpdateWave;

    [Header("States Enemy")]
    [SerializeField] private int lowStage = 0;
    [SerializeField] private int midStage = 3;
    [SerializeField] private int hardStage = 6;

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
    private void Start()
    {
        progressBarObj.fillAmount = 0;
        //Cursor.visible = false;
    }

    public void CheckProgressWave()
    {
        countCurr++;
        UpdateProgressBar();
        if (countCurr >= countMax)
        {
            EndWave();
        }
        //if(countCurr>= midLevel)
        //{enemyLevel++;
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
        player.OnPlayer(false);
        Cursor.visible = true;
        gameOverScreen.SetActive(true);
    }

    public void EndWave()
    {
        IsPlayerOn(false);

        waveCount++;

        //
        UpdateWavesUI(waveCount);
        //UpdateWave?.Invoke();

        //cambia el nivel del enemigo.
        ChangeState();

        countCurr = 0;
        UpdateProgressBar();
        if (waveCount >= waveMax)
        {
            WinGame();
        }
        else
        {
            ScreenUpgrade.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void UpdateWavesUI(int waveCount)
    {
        numWavesText.text = $"{waveCount+1}/{waveMax} ";
    }

    void ChangeState()
    {
        if(waveCount == lowStage)
        {
            //enemySpawner.ChangeEnemy();
        }
        else if(waveCount == midStage)
        {
            enemySpawner.ChangeEnemy();
        }
        else if(waveCount == hardStage)
        {
            enemySpawner.ChangeEnemy();
        }
    }

    public void NextWave()
    {
        IsPlayerOn(true);
        countMax *= 2;
        ScreenUpgrade.SetActive(false);
        enemySpawner.ReduceSpawnRate();
        Time.timeScale = 1f;

    }
    void WinGame()
    {
        enemySpawner.StopSpawn();
        enemySpawner.DestroyEnemies();
        player.OnPlayer(false);
        winOverScreen.SetActive(true);
        Cursor.visible = true;
    }
    public void UpdateInfo(Sprite weapon)
    {
        weaponsIcon.sprite = weapon;
    }

    public void IsPlayerOn(bool value)
    {
        player.OnPlayer(value);
    }
}
