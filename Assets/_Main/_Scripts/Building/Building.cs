using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingScriptableObject buildingData;
    public EnemySpawnController enemySpawner;
    [SerializeField] StatusBar hpBar;
    private float damage;
    private bool isDamage;

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
        isDamage = true;
        AudioManager.instance.Play("Impact");
        damage += dmg;
        StartCoroutine(ApplyDamageOverTime());
        //Debug.Log(currentHealth);
    }
    public void UnregisterDamage(float dmg)
    {
        isDamage = false;
        StopCoroutine(ApplyDamageOverTime());
        damage -= dmg;
    }
    public void UpgradeBuilindg()
    {
        AudioManager.instance.Stop("Impact");
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
        while (isDamage) // Esto crea un bucle infinito
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                damage = 0;
                Kill();
                yield break;
            }
            AudioManager.instance.Play("Impact");
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
