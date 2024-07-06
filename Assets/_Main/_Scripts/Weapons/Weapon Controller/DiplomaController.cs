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
        AudioManager.instance.Play("ShotPaper");
        GameObject spawnedDiploma = Instantiate(weaponData.Prefab);
        spawnedDiploma.transform.position = transform.position;
        spawnedDiploma.GetComponent<BulletBehaviour>().DirectionChecker(player.lastMove);
    }
}