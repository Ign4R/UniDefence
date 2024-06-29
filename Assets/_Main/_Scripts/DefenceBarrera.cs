using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceBarrera : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D coll;
    [SerializeField] private float maxDamage=2;
    [SerializeField] private float maxHits=10;
    private float currDamage;
    private bool isAttack;
    private bool isResetting;
    private float againToTimeAttack=2;
    private int countHits;
    private float currentCooldown;
   [SerializeField] private float maxCooldown;

    private void Start()
    {
        currDamage = maxDamage;
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
                enemy.TakeDamage(currDamage);
                yield return new WaitForSeconds(againToTimeAttack);
            }
            else
            {
                yield break;

            }
         
           
        }

    }



   
}
