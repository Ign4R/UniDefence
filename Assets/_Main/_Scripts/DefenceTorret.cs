using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DefenceTorret : MonoBehaviour
{
    public GameObject prefabProyectil = null;
    public Transform positionShoot;
    private Vector3 dirMove;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;

    private float nextFireTime;
  
    private bool isFreeze;

    private void Start()
    {
        dirMove = Vector2.down;
    }
    void Update()
    {
        if (!isFreeze)
        {
            Movement();
        }
       
        //Busca enemigos dentro del radio de ataque
        //Si hay objetivo, entonces dispara
   
        if (Time.time > nextFireTime) 
        {
            Attack(); // Actualizar el tiempo en el que se podrá disparar nuevamente
            nextFireTime = Time.time + fireRate;
        }



    }

    private void Movement()
    {
        transform.position += dirMove * speed * Time.deltaTime;
        if (transform.localPosition.y >= 0.7f)
        {
            dirMove = Vector2.down;
        }
        else if(transform.localPosition.y <=-0.7f)
        {
            dirMove = Vector2.up;
        }
    }
    private void Attack()
    {
        //isAttacking = true;
        //Configuro con direccion y daño
        GameObject spawnedDiploma = Instantiate(prefabProyectil, positionShoot.position,Quaternion.identity);
        spawnedDiploma.GetComponent<BulletBehaviour>().DirectionChecker(transform.right);
        StartCoroutine(ResumeMovement());
    }

    IEnumerator ResumeMovement()
    {
        // Espera un tiempo (la duración del ataque)
        yield return new WaitForSeconds(0.5f); // Cambia 0.5 por el tiempo real de tu animación de ataque

        // Retoma el movimiento
        isFreeze = false;
    }


}
