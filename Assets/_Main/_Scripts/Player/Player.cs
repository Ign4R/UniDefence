using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
   [SerializeField] private Animator _animator;
    public Sprite[] sprites;
    [Header("Movement")]
    float xMove;
    float yMove;
    private PlayerStats _stats;
    private int _speed;
    public Vector3 moveDirection;
    public Vector3 lastMove;

    private void Awake()
    {
        _stats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastMove = Vector3.right;
        //por si el jugador no se mueve, le designas un movimiento inicial.
    }
    private void Update()
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

        _animator.SetFloat("Speed", rb.velocity.x);
        moveDirection = new Vector2(xMove, yMove).normalized;

        rb.velocity = moveDirection * _stats.CurrentSpeed;

        if (xMove < 0)
        {
            _animator.SetBool("Horizontal",true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (xMove > 0)
        {
            _animator.SetBool("Horizontal", true);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (yMove < 0)
        {
            _animator.SetBool("Back", true);
            //transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (yMove > 0)
        {
            _animator.SetBool("Front", true);
            //transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        if (moveDirection != Vector3.zero)
        {
            // Guarda la última dirección de movimiento
            lastMove = moveDirection;
        }
    }

    public void OnPlayer(bool value)
    {
        gameObject.SetActive(value);
    }
}