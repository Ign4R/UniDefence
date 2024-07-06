using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base script for all weapon
/// </summary>

public class WeaponController : MonoBehaviour
{
    [SerializeField] protected Transform positionShoot;
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;

    float currentCooldown; // No se usa actualmente

    public float fireRate = 0.5f;  // Tiempo mínimo entre disparos
    private float nextFireTime = 0f;  // Tiempo en el que se podrá disparar nuevamente
    protected Player player;

    [Header("Change guns")]
    int totalWeapons = 2;
    public int currentWeaponIndex;
    [SerializeField] protected GameObject[] guns;
    [SerializeField] protected GameObject weaponHolder;
    [SerializeField] protected GameObject currentGun;
    [Space]
    [SerializeField] protected Sprite[] spriteGuns;

    private void Awake()
    {
        player = FindObjectOfType<Player>();

        // Inicializar la primera arma
        if (guns.Length > 0)
        {
            weaponHolder = guns[0];
            currentGun = guns[0];
            currentWeaponIndex = 0;
        }
        else
        {
            Debug.LogError("No weapons found in guns array.");
        }
    }

    protected virtual void Start()
    {
        //currentCooldown = weaponData.CooldownDuration;
    }

    void Update()
    {
        // Manejo del disparo
        if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
        {
            Attack();
            nextFireTime = Time.time + fireRate;  // Actualizar el tiempo en el que se podrá disparar nuevamente
        }

        // Cambio de arma
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon();
        }
    }
    private void ChangeWeapon()
    {
        // Desactivar el arma actual
        guns[currentWeaponIndex].SetActive(false);

        // Incrementar el índice del arma actual
        currentWeaponIndex++;

        // Reiniciar el índice si es necesario
        if (currentWeaponIndex >= guns.Length)
        {
            currentWeaponIndex = 0;
        }

        // Activar la nueva arma
        guns[currentWeaponIndex].SetActive(true);

        // Chequear que no se pase del rango
        if (currentWeaponIndex <= spriteGuns.Length)
        {
            GameManager.instance.UpdateInfo(spriteGuns[currentWeaponIndex]);
        }
       
    }
    protected virtual void Attack()
    {
        // Cada arma podría tener su sonido.
        //AudioManager.instance.Play("Bullet");

        // Reinicia el ciclo de tiempo.
        //currentCooldown = weaponData.CooldownDuration;
     
    }
   
}
