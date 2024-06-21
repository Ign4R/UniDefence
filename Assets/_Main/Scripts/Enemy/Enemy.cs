using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    [SerializeField] Transform targetDestination;

    //[SerializeField] bool isTouchingEnemy = false;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetDestination = FindObjectOfType<Building>().transform;
    }

    private void Start()
    {
        //encuentra todos los gameobject que tenga el tag SpawnPoint.. (magnifico)
        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randomSpawnPoint = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[randomSpawnPoint].transform.position;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rb.velocity = direction * enemyData.MoveSpeed;

        //como evitar que el enemigo empuje al jugador!?
        //Vector3 distance = targetDestination.position - transform.position;
        //if (distance.x <= 1.26f)
        //if (!isTouchingEnemy)
        //{
        //    rb.velocity = direction * enemyData.MoveSpeed;
        //}
        //else
        //{
        //    rb.velocity = Vector2.zero;
        //}

        //transform.position = Vector3.MoveTowards(transform.position, 
        //                                         targetDestination.position, 
        //                                         enemyData.MoveSpeed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Building"))
        {
            Debug.Log("colision?");

            Building building = collision.gameObject.GetComponent<Building>();
            building.TakeDamage(enemyData.Damage);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("colisiona con player");

            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(enemyData.Damage);

            //isTouchingEnemy = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //isTouchingEnemy = false;
    }
}
