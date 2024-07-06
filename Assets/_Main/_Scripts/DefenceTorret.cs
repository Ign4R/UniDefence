using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenceTorret : WeaponBehaviour, IDefence
{
    public Button upgradeDefence;
    public GameObject prefabProyectil = null;
    public Transform positionShoot;
    private Vector3 dirMove;
    [Range(0.5f,3)]
    [SerializeField] private float _currSpeedT;
    [Range(0.3f, 1)]
    [SerializeField] private float fireRate;

    private float nextFireTime;
  
    private bool isFreeze;
    private int countUpgrade;
    public bool UpgradeMax { get; set; }
    public bool IsActive { get; set; }
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
       
        if (Time.time > nextFireTime) 
        {
            ///TODO: Poner audio de torreta
            Attack();
            nextFireTime = Time.time + fireRate;
        }



    }
    public override void UpgradeStats()
    {
        countUpgrade++;

        if (countUpgrade < 10)
        {
            currentDamage++;
            _currSpeedT += 0.35f; //valores a partir de su rango y su cantidad de pasos
            fireRate -= 0.09f;//valores a partir de su rango y su cantidad de pasos

        }
        else
        {
            UpgradeMax = true;
        }
    }
    private void Movement()
    {
        transform.position += dirMove * _currSpeedT * Time.deltaTime;
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
