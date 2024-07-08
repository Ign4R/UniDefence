using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base script of all projectile behaviours.
/// </summary>

public class WeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected Vector3 direction;
    protected bool isActive;

    //current stats
    protected float currentFireRate;
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int maxLife;

    protected virtual void Awake()
    {
        currentFireRate = weaponData.FireRate;
        currentDamage = weaponData.MaxDamage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        maxLife = weaponData.LifeTime;
    }
    public void SetStats(float modDamage, float modFireRate)
    {
        currentDamage += modDamage;
        currentFireRate += modFireRate;
    }
    protected virtual void Start()
    {

    }
}