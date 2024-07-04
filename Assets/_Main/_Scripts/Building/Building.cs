using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDamageable
{
    public BuildingScriptableObject buildingData;
    public EnemySpawnController enemySpawner;
    [SerializeField] StatusBar hpBar;
    private float damage;


    //[Header("current stats")]
    float currentHealth;
   [SerializeField] private GameObject[] defenses;
    private int currentIndex=0;
    private bool isDamage;

    private void Awake()
    {
        currentHealth = buildingData.MaxHealth;
        buildingData.Invulnerable = false;
    }
    public void SetDamage(float dmg)
    {
        damage += dmg;
    }
    public void TakeDamage()
    {
        AudioManager.instance.Play("Impact");
        currentHealth -= damage;
        hpBar.SetState(currentHealth, buildingData.MaxHealth);
        if (currentHealth <= 0)
        {
            damage = 0;
            Kill();
        }   

    }

    public void AddDefence()
    {
        if (currentIndex < defenses.Length)
        {
            currentIndex++;
            defenses[currentIndex-1].SetActive(true);
        }
    }

    void Kill()
    {
        GameManager.instance.GameOver();
        enemySpawner.StopSpawn();
        gameObject.SetActive(false);
    }

}
