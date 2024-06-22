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
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(xMove) > Mathf.Abs(yMove))
        {
            // Si el movimiento horizontal es mayor, ignorar el movimiento vertical
            yMove = 0;
        }
        else
        {
            // Si el movimiento vertical es mayor o igual, ignorar el movimiento horizontal
            xMove = 0;
        }

        moveDirection = new Vector2(xMove, yMove).normalized;

        rb.velocity = moveDirection * speed;

        if (xMove < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (xMove > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (yMove < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (yMove > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        if (moveDirection != Vector3.zero)
        {
            // Guarda la última dirección de movimiento
            lastMove = moveDirection;
        }
    }
}