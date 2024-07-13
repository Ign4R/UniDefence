using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    Rigidbody2D rb;
    [SerializeField] private Animator _animator;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    private PlayerStats _stats;
    public Vector3 moveDirection;
    public Vector3 lastMove;
    public bool isTest;

    private void Awake()
    {
        _stats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastMove = Vector3.down;
        //_animator.enabled = false;
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

        if (MobileDetector.IsMobile()|| isTest)
        {
            xMove = variableJoystick.Horizontal;
            yMove = variableJoystick.Vertical;

        }
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
        rb.velocity = moveDirection * _stats.CurrentSpeed;
        _animator.SetInteger("SpeedX", (int)rb.velocity.x);
        _animator.SetInteger("SpeedY", (int)rb.velocity.y);
        //_animator.SetFloat("SpeedX", (int)rb.velocity.x);


        if (xMove < 0)
        {
            spriteRenderer.sprite = sprites[0];
       
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (xMove > 0)
        {

            spriteRenderer.sprite = sprites[0];
 
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (yMove < 0)
        {

            //transform.rotation = Quaternion.Euler(0, 0, -90); down
        }
        else if (yMove > 0)
        {

        }

        if (moveDirection != Vector3.zero)
        {
            //_animator.enabled = true;
            // Guarda la última dirección de movimiento
            lastMove = moveDirection;
        }
   
    }

    public void OnPlayer(bool value)
    {
        gameObject.SetActive(value);
    }
}