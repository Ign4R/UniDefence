using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : WeaponController
{
   
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedBook = Instantiate(weaponData.Prefab);
        spawnedBook.transform.position = transform.position;
        var book =spawnedBook.GetComponent<BookBehaviour>();
        book.DirectionAttack(player.lastMove);
        book.SetStats(modDamage, modFireRate);
        //spawnedBook.GetComponent<BookBehaviour>().DirectionChecker(player.lastMove);
    }

    public void UpgradeStats()
    {
        modDamage += 3;
        var incrementFR = 0.4f;
        if (modFireRate > incrementFR) modFireRate -= incrementFR;
    }
}