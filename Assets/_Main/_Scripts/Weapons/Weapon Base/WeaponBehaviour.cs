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
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int maxLife;
    public virtual void UpgradeStats()
    {

    }
    protected virtual void Awake()
    {
        currentDamage = weaponData.MaxDamage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        maxLife = weaponData.LifeTime;
    }

    protected virtual void Start()
    {

    }
}