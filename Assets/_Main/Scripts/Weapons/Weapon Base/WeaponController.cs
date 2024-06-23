using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base script for all weapon
/// </summary>

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;

    float currentCooldown;/// no se usa ??????????????

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

        //
        weaponHolder = guns[0];
        currentGun = guns[0];
        currentWeaponIndex = 0;
    }

    protected virtual void Start()
    {
        //currentCooldown = weaponData.CooldownDuration;
    }

    protected virtual void Update()
    {
        //currentCooldown -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
        {
            Attack();
            nextFireTime = Time.time + fireRate;  // Actualizar el tiempo en el que se podrá disparar nuevamente
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentWeaponIndex++;
            if (currentWeaponIndex < totalWeapons)
            {
                guns[currentWeaponIndex - 1].SetActive(false);
                guns[currentWeaponIndex].SetActive(true);                
            }
            else
            {
                guns[currentWeaponIndex - 1].SetActive(false);
                currentWeaponIndex = 0;
                guns[currentWeaponIndex].SetActive(true);
            }
            //
            GameManager.instance.UpdateInfo(spriteGuns[currentWeaponIndex]);
            Debug.Log(currentWeaponIndex);
        }
    }

    protected virtual void Attack()
    {
        //cada arma podria tener su sonido.
        AudioManager.instance.Play("Bullet");

        //reinicia el ciclo de tiempo.
        //currentCooldown = weaponData.CooldownDuration;
    }


}