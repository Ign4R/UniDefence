using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehaviour : WeaponBehaviour
{
    public Vector3 launchOffset = new(-0.1f, 0.3f, 0f);
    public Vector3 dirBook = new(1f, 2f, 0f);
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
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void DirectionAttack(Vector3 dirAttack)
    {
        if (isThrowing)
        {
            dirAttack = DirectionBook(dirAttack);

            GetComponent<Rigidbody2D>().AddForce(dirAttack * weaponData.Speed, ForceMode2D.Impulse);
        }
        transform.Translate(launchOffset);
    }

    public Vector3 DirectionBook(Vector3 directionPlayer)
    {
        direction = directionPlayer;

        if (directionPlayer.y < 0) //down
        {
            direction = new Vector3(-dirBook.x, dirBook.y, 0f);
        }
        else if (directionPlayer.y > 0) //up
        {
            direction = new Vector3(dirBook.x, dirBook.y, 0f);
        }

        if (directionPlayer.x > 0) //right
        {
            direction = new Vector3(dirBook.x, dirBook.y, 0f);
        }
        else if (directionPlayer.x < 0) //left
        {
            direction = new Vector3(-dirBook.x, dirBook.y, 0f);
        }
     
        //Debug.Log("direction" + direction);
        return direction;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange, layerEnemy);
            foreach (var hitCol in hitColliders)
            {
                var enemies = hitCol.GetComponent<EnemyStats>();

                if (enemies)
                {
                    //porcentaje de daño!.
                    //var closestPoint = hitCol.ClosestPoint(transform.position);
                    //var distance = Vector3.Distance(closestPoint, transform.position);
                    //var damagePercent = Mathf.InverseLerp(splashRange, 0, distance);
                    //enemies.TakeDamage(damagePercent * currentDamage);
                    
                    //daño 10
                    enemies.TakeDamage(currentDamage);
                }
            }

            //EnemyStats enemy = collision.GetComponent<EnemyStats>();
            //enemy.TakeDamage(currentDamage);
            //Debug.Log(currentDamage);
            //Debug.Log("hola");
        }
    }
}
