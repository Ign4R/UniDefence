using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// skillet = sartén
/// </summary>
public class SkilletController : WeaponController
{
    
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedSkillet = Instantiate(weaponData.Prefab);
        spawnedSkillet.transform.position = transform.position;
        spawnedSkillet.transform.parent = transform;
    }
}