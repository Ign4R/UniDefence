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
        damage += dmg;
        StartCoroutine(MakeVulnerableAgain());
        //Debug.Log(currentHealth);
    }
    public void UnregisterDamage(float dmg)
    {
        AudioManager.instance.Stop("Impact");
        damage -= dmg;
    }
    public void UpgradeBuilindg()
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

    IEnumerator MakeVulnerableAgain()
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

            hpBar.SetState(currentHealth, buildingData.MaxHealth);
            yield return new WaitForSeconds(buildingData.InvulnerableTime);
        
        }
    }

    //IEnumerator BlinkRountine() //blink parpadeos
    //{
    //    int t = 10;
    //    while (t > 0)
    //    {
    //        //spriteRenderer.enabled = false;
    //        //yield return new WaitForSeconds(t * blinkRate);
    //        //spriteRenderer.enabled = true;
    //        //yield return new WaitForSeconds(t * blinkRate);
    //        t--;
    //    }
    //}
}
