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

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    protected virtual void Start()
    {
        currentCooldown = weaponData.CooldownDuration;
    }

    protected virtual void Update()
    {
        //currentCooldown -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
        {
            Attack();
            nextFireTime = Time.time + fireRate;  // Actualizar el tiempo en el que se podrá disparar nuevamente
        }
    }

    protected virtual void Attack()
    {
        //reinicia el ciclo de tiempo.
        currentCooldown = weaponData.CooldownDuration;
    }
}