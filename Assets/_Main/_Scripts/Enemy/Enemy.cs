using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    [SerializeField] Transform targetDestination;
    Rigidbody2D rb;
    private bool isMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetDestination = FindObjectOfType<Building>()?.transform;
    }

    private void Start()
    {
        //encuentra todos los gameobject que tenga el tag SpawnPoint.. (magnifico)
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
        // Calcula la dirección horizontal hacia el objetivo
        Vector3 direction = targetDestination.position - transform.position;

        // Ignora la componente Y para asegurar que solo se considere el movimiento horizontal
        if (transform.rotation.z == 0)
        {
            direction.y = 0;
        }
        else
        {
            direction.x = 0;
        }
       

        // Normaliza el vector para asegurar que tenga magnitud 1
        direction = direction.normalized;

        // Establece la velocidad en la dirección horizontal
        rb.velocity = direction * enemyData.MoveSpeed;

    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Building"))
        {
            AudioManager.instance.Play("Impact");
            print("toque uni");
            Building building = collision.gameObject.GetComponent<Building>();
            building.TakeDamage(enemyData.Damage);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("colisiona con player");

            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(enemyData.Damage);
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Building"))
        {
            //AudioManager.instance.Stop("Impact");
            Building building = collision.gameObject.GetComponent<Building>();
            building.UnregisterDamage(enemyData.Damage);
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }

  
}
