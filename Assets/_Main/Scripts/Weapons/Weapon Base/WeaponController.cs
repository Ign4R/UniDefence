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

    float currentCooldown;

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
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        //reinicia el ciclo de tiempo.
        currentCooldown = weaponData.CooldownDuration;
    }
}