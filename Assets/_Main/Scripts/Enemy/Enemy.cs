using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    [SerializeField] Transform targetDestination;
    //[SerializeField] float speed = 2f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rb.velocity = direction * enemyData.MoveSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Building"))
        {
            Debug.Log("colision?");

            Building building = collision.gameObject.GetComponent<Building>();
            building.TakeDamage(enemyData.Damage);
        }        
    }


}
