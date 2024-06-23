using UnityEngine;

public class DiplomaController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedDiploma = Instantiate(weaponData.Prefab);
        spawnedDiploma.transform.position = transform.position;
        spawnedDiploma.GetComponent<DiplomaBehaviour>().DirectionChecker(player.lastMove);
    }
}