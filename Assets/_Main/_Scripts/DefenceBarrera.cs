using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenceBarrera : WeaponBehaviour, IDefence
{
    public Button upgradeDefence;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D coll;
    [SerializeField] private float maxHits=10;
    private int maxLevelUpgrade=10;
    private bool isAttack;
    private bool isResetting;
    private int countHits;
    private float currentCooldown;
    private float maxCooldown=10;
    private int countUpgrade;

    public bool UpgradeMax { get ; set ; }
    public bool IsActive { get; set; }

    /// representarlo con un numero
    protected override void Awake()
    {
        currentCooldown = maxCooldown;
    }

    private void Update()
    {
        if (isResetting)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown < 1)
            {
                currentCooldown = maxCooldown;
                coll.enabled = true;
                isResetting = false;
                spriteRenderer.color = Color.white;
            }
        }
    }
    public void CheckHits()
    {
        countHits++;
        if (countHits >= maxHits)
        {
            countHits = 0;
            coll.enabled = false;
            isResetting = true;
            spriteRenderer.color = Color.red;
          
        }
    }

    public override void UpgradeStats()
    {
        countUpgrade++;
        if (countUpgrade <= 10)
        {
            currentDamage ++;
            maxHits =+ 4;
            currentCooldown--;
        }
        else
        {
            UpgradeMax = true;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isResetting)
        {
            CheckHits();
            isAttack = true;
            AudioManager.instance.Play("Impact");
            EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
            StartCoroutine(Attack(enemy));
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
            StopCoroutine(Attack(enemy));
        }
    }
    IEnumerator Attack(EnemyStats enemy)
    {
        while (isAttack) // Esto crea un bucle infinito
        {
            if (enemy != null)
            {
                enemy.TakeDamage(currentDamage);
                yield return new WaitForSeconds(currentCooldown);
            }
            else
            {
                yield break;

            }
         
           
        }

    }



   
}
