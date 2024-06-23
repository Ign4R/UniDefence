using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiplomaBehaviour : WeaponBehaviour
{
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        transform.position += weaponData.Speed * Time.deltaTime * direction;
    }
    
    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

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
            ReducePierce();
        }
    }

    void ReducePierce()
    {
        currentPierce--;
        if (currentPierce <= 0) Destroy(gameObject);
    }
}