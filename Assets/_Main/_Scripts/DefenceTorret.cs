using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DefenceTorret : WeaponBehaviour
{
    public GameObject prefabProyectil = null;
    public Transform positionShoot;
    private Vector3 dirMove;
    [Range(0.5f,3)]
    [SerializeField] private float speed;
    [Range(0.5f, 1)]
    [SerializeField] private float fireRate;

    private float nextFireTime;
  
    private bool isFreeze;
    private int countUpgrade;

    protected override void Awake()
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
    public override void UpgradeStats()
    {
        countUpgrade++;
        if (countUpgrade <= weaponData.MaxLevelUpgrade)
        {
            currentDamage++;
            speed =+ 0.25f; //valores a partir de su rango y su cantidad de pasos
            fireRate-=0.05f;//valores a partir de su rango y su cantidad de pasos
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
        GameObject prefab = Instantiate(prefabProyectil, positionShoot.position,Quaternion.identity);
        BulletBehaviour bullet = prefab.GetComponent<BulletBehaviour>(); 
        bullet.DirectionChecker(transform.right);
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
