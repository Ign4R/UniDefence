using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    [SerializeField] StatusBar hpBar;

    //[Header("current stats")]
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    private void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;        
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        hpBar.SetState(currentHealth, enemyData.MaxHealth);
        //Debug.Log(currentHealth);

        if (currentHealth <= 0) Kill();
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
