using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    [SerializeField] Transform targetDestination;
    Rigidbody2D rb;
    public bool test;
    private Coroutine damageCoroutine;
    private int countTargets;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetDestination = FindObjectOfType<Building>()?.transform;
    }

    private void Start()
    {
        if (test) return;
        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randomSpawnPoint = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[randomSpawnPoint].transform.position;
        transform.rotation = spawnPoint[randomSpawnPoint].transform.rotation;
    }

    private void FixedUpdate()
    {
        if (targetDestination != null)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 direction = targetDestination.position - transform.position;

        if (transform.rotation.z == 0)
        {
            direction.y = 0;
        }
        else
        {
            direction.x = 0;
        }

        direction = direction.normalized;

        rb.velocity = direction * enemyData.MoveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            countTargets++;
            PlayerStats target = collision.gameObject.GetComponent<PlayerStats>();
            target.SetDamage(enemyData.Damage);
            damageCoroutine = StartCoroutine(ApplyDamageOverTime(target));
        }
        if (collision.gameObject.CompareTag("Building"))
        {
            countTargets++;
            Building target = collision.gameObject.GetComponent<Building>();
            target.SetDamage(enemyData.Damage);
            target.TakeDamage();
            damageCoroutine = StartCoroutine(ApplyDamageOverTime(target));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            countTargets--;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }

        }
        if (collision.gameObject.CompareTag("Building"))
        {
            countTargets--;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    IEnumerator ApplyDamageOverTime(IDamageable target)
    {
        while (countTargets > 0)
        {
            if (countTargets == 0) yield break;
            target.TakeDamage();
            yield return new WaitForSeconds(2);

        }
    }
}
