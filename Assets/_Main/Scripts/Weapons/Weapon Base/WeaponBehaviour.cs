using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base script of all projectile behaviours.
/// </summary>

public class WeaponBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float xDir = direction.x;
        float yDir = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        //aca deberia ir la rotacion de la imagen del assets.
        if(xDir == 0 && yDir != 0) 
        {
            //scale.x *= -1;
            //scale.y *= -1;
            rotation.z = -90f;
        }  
        else if(xDir != 0 && yDir == 0) 
        {
            //scale.x *= -1;
            //scale.y *= -1;
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }
}