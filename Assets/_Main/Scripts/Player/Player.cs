using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Movement")]
    float xMove;
    float yMove;
    [SerializeField] float speed = 2.5f;
    public Vector3 moveDirection;
    public Vector3 lastMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        //por si el jugador no se mueve, le designas un movimiento inicial.
        lastMove = Vector3.right;
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

        rb.velocity = moveDirection * speed; 

        if(moveDirection != Vector3.zero)
        {
            //guardas la ultima posicion.
            lastMove = moveDirection;
        }
    }
}
