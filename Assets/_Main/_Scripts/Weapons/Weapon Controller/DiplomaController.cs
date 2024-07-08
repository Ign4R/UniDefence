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
        GameObject instance = Instantiate(weaponData.Prefab);
        instance.transform.position = transform.position;
        DiplomaBehaviour diploma = instance.GetComponent<DiplomaBehaviour>();
        diploma.SetStats(modDamage, modFireRate);
        diploma.DirectionChecker(player.lastMove);


    }
    public void UpgradeStats()
    {
        modDamage += 1.5f;
        var increment = 0.2f;
        if (modFireRate > 0.1f)
        {
            modFireRate -= increment;
        }
    }


}