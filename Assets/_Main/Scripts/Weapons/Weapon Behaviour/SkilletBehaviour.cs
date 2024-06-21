using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilletBehaviour : WeaponBehaviour
{
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        transform.position += weaponData.Speed * Time.deltaTime * direction;
    }
}