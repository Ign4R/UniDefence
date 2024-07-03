using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingScriptableObject buildingData;
    public EnemySpawnController enemySpawner;
    [SerializeField] StatusBar hpBar;
    private float damage;


    //[Header("current stats")]
    float currentHealth;
   [SerializeField] private GameObject[] defenses;
    private int currentIndex=0;

    private void Awake()
    {
        currentHealth = buildingData.MaxHealth;
        buildingData.Invulnerable = false;
    }

    public void TakeDamage(float dmg)
    {
        AudioManager.instance.Play("Impact");
        damage += dmg;
        StartCoroutine(ApplyDamageOverTime());
    }
    public void UnregisterDamage(float dmg)
    {
        damage -= dmg;
        AudioManager.instance.Stop("Impact");
        StopCoroutine(ApplyDamageOverTime());
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

    IEnumerator ApplyDamageOverTime()
    {
        while (true) // Esto crea un bucle infinito
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                damage = 0;
                Kill();
                yield break;
            }
            AudioManager.instance.Play("Impact",true);
            hpBar.SetState(currentHealth, buildingData.MaxHealth);
            yield return new WaitForSeconds(buildingData.InvulnerableTime);

        }
   
    }

}
