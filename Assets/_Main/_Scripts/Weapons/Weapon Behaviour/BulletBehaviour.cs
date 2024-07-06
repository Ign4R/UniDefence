using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class BulletBehaviour : WeaponBehaviour
{
    private float currentPierce = 0;

    protected override void Start()
    {
        base.Start();
        
    }

    private void Update()
    {
        ReducePierce();
        transform.position += direction * weaponData.Speed * Time.deltaTime;
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir.normalized;

        float xDir = direction.x;
        float yDir = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        //aca tmb deberia ir la rotacion de la imagen del assets.
        if (xDir == 0 && yDir != 0)
        {
            //scale.x *= -1;
            //scale.y *= -1;
            rotation.z = -90f;
        }
        else if (xDir != 0 && yDir == 0)
        {
            //scale.x *= -1;
            //scale.y *= -1;
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            //AudioManager.instance.Stop("Chainsaw"); pq stopeas el sonido??????????
            Destroy(gameObject);
        }

        if (collision.CompareTag("Limit"))
        {
            Destroy(gameObject);
        }
    }

    void ReducePierce()
    {
        currentPierce += Time.deltaTime;
        if (currentPierce >= maxLife)
        {
            Destroy(gameObject);
            currentPierce = 0;
        }
    }
}