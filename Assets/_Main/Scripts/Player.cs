using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    float xMove;
    float yMove;
    Vector3 moveDirection;
    [SerializeField] float speed = 2.5f;

    Rigidbody2D Rb;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();        
    }

    void Move()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(xMove, yMove).normalized;

        Rb.velocity = moveDirection * speed; 
    }
}
