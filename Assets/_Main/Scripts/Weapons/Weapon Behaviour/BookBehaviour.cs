using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehaviour : WeaponBehaviour
{
    public Vector3 launchOffset = new(-0.1f, 0.3f, 0f);
    public Vector3 dirBook = new(1f, 2f, 0f);
    public bool isThrowing;

    private void Awake()
    {
        isThrowing = true;
    }

    protected override void Start()
    {
        base.Start();

    }

    private void Update()
    {
        //if (!isThrowing)
        //{
        //    transform.position += Time.deltaTime * weaponData.Speed * direction;
        //}
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
}
