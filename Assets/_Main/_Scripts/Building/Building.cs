using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour, IDamageable
{
    public BuildingScriptableObject buildingData;
    public EnemySpawnController enemySpawner;
    [SerializeField] StatusBar hpBar;
    private float damage;
    private bool maxUpgrade;
    [SerializeField] private Button upgradeT;
    [SerializeField] private Button upgradeB;
    [SerializeField] private Button addDefence;

    float currentHealth;
    [SerializeField] private GameObject[] defenses;
    private int countTurret;

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
        AudioManager.instance.Play("Chainsaw",true);
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
                defence.IsActive = true;
                break;
            }
            
        }

        if (defenceType == "Turret")
        {
            countTurret++;       
            upgradeT.interactable = true;
            if (countTurret >= 2)
            {
                addDefence.interactable = false;
            }
        }
        if (defenceType == "Barrier")
        {
            upgradeB.interactable = true;
        }

    }

    public void UpgradeDefence(string defenceType)
    {
        foreach (var item in defenses)
        {
            var targetUpgrade = item.GetComponent<IDefence>();
            if (item.CompareTag(defenceType.ToString()) && targetUpgrade.IsActive)
            {
                targetUpgrade.UpgradeDefence();
         
            }
        }
    }

    public bool CheckMax()
    {
        for (int i = 0; i < defenses.Length; i++)
        {
            var targetUpgrade = defenses[i].GetComponent<IDefence>();
            if (targetUpgrade.UpgradeMax || targetUpgrade.IsActive )
            {
                return maxUpgrade= true;
            }
            else if(!targetUpgrade.UpgradeMax || !targetUpgrade.IsActive)
            {
                return maxUpgrade= false;
            }
        }
        return maxUpgrade;
    }
 
    void Kill()
    {
     
        GameManager.instance.GameOver();
        enemySpawner.StopSpawn();
        gameObject.SetActive(false);
    }




}
