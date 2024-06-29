using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public GameObject loot;
    [SerializeField] StatusBar hpBar;

    //[Header("current stats")]
    float currentMoveSpeed;
    private float currentHealth;
    float currentDamage;
    private bool isDamage;
    private int damage;

    private void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;        
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        AudioManager.instance.Play("Impact");
        //hpBar.SetState(currentHealth, enemyData.MaxHealth);
        //Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }
  
    void Kill()
    {
        float numRandom = Random.Range(-5, 5);
        //print(numRandom);
        if (numRandom > 0)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
     
    }

}
