using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDamageable
{
    public BuildingScriptableObject buildingData;
    public EnemySpawnController enemySpawner;
    [SerializeField] StatusBar hpBar;
    private float damage;

    float currentHealth;
    [SerializeField] private GameObject[] defenses;

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
    public void AddDefence(string defenceType)
    {      
        foreach (var item in defenses)
        {
            if (item.CompareTag(defenceType.ToString()) && !item.activeSelf) 
            {
                item.SetActive(true);
                var defence = item?.GetComponent<IDefence>();
                defence.IsUpgrade = true;
                break;
            }
        }
      
    }

    public void UpgradeDefence(string defenceType)
    {
        foreach (var item in defenses)
        {
            var targetUpgrade = item.GetComponent<IDefence>();
            if (item.CompareTag(defenceType.ToString()) && targetUpgrade.OnUpgrade)
            {
                if (targetUpgrade.IsUpgrade) return;
                targetUpgrade.UpgradeStats();
                break;
            }
        }
    }
 
    void Kill()
    {
     
        GameManager.instance.GameOver();
        enemySpawner.StopSpawn();
        gameObject.SetActive(false);
    }




}
