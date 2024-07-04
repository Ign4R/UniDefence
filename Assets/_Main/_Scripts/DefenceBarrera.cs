using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceBarrera : WeaponBehaviour, IDefence
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D coll;
    [SerializeField] private float maxHits=10;
    private int maxLevelUpgrade=10;
    private bool isAttack;
    private bool isResetting;
    private int countHits;
    private float currentCooldown;
    private int countUpgrade;

    public bool OnUpgrade { get ; set ; }
    public bool IsUpgrade { get; set; }

    /// representarlo con un numero

    protected override void Start()
    {
        base.Start();
        currentCooldown = weaponData.CooldownDuration;
    }
    private void Update()
    {
        if (isResetting)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown < 1)
            {
                currentCooldown = weaponData.CooldownDuration;
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
        if (countUpgrade <= weaponData.MaxLevelUpgrade)
        {
            currentDamage ++;
            maxHits =+ 4;
            weaponData.CooldownDuration--;
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
                yield return new WaitForSeconds(weaponData.CooldownDuration);
            }
            else
            {
                yield break;

            }
         
           
        }

    }



   
}
