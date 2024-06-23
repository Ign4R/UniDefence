using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehaviour : WeaponBehaviour
{
    public Vector3 launchOffset = new(-0.1f, 0.3f, 0f);
    public GameObject prefabEffect;
    public bool isThrowing;
    public float timeFly = 1.5f;
    public float splashRange = 2f;
    public LayerMask layerEnemy;

    Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
        isThrowing = true;
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(nameof(WaitSomeTime));
    }

    IEnumerator WaitSomeTime()
    {
        yield return new WaitForSeconds(timeFly);
        DamageInArea();
    }

    public void DirectionAttack(Vector3 dirPlayer, float speedPlayer)
    {
        Vector3 dirBook = new Vector3(2.5f, 4, 0);
        if (isThrowing)
        {
            DirectionBook(dirPlayer, dirBook);
            rb.AddForce(direction * weaponData.Speed, ForceMode2D.Impulse);
        }
        transform.Translate(launchOffset);
    }

    public Vector3 DirectionBook(Vector3 directionPlayer, Vector3 dirBook)
    {
        
        if (directionPlayer.y <0) //down
        {
            rb.gravityScale = -1;
            direction = new Vector3(dirBook.x, -dirBook.y, 0f);
        }
        else if (directionPlayer.y > 0) //up
        {
            rb.gravityScale = 1;
            direction = new Vector3(dirBook.x, dirBook.y, 0f);
        }

        if (directionPlayer.x > 0) //right
        {
            rb.gravityScale = 1;
            direction = new Vector3(dirBook.x, dirBook.y, 0f);
        }
        else if (directionPlayer.x < 0) //left
        {
            rb.gravityScale = 1;
            direction = new Vector3(-dirBook.x, dirBook.y, 0f);
        }
     
        //Debug.Log("direction" + direction);
        return direction;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            DamageInArea();
        }
    }
    public void DamageInArea()
    {
        var explotion= Instantiate(prefabEffect, transform.position, Quaternion.identity);
        Destroy(explotion, 2);
        var hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange, layerEnemy);
        foreach (var hitCol in hitColliders)
        {
            var enemy = hitCol.GetComponent<EnemyStats>();

            if (enemy!=null)
            {
                enemy.TakeDamage(currentDamage);
                //gameObject.SetActive(false);
                //Destroy(gameObject, 3);
            }
        }
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, splashRange );
    }
}
