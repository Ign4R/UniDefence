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
        spawnedBook.GetComponent<BookBehaviour>().DirectionAttack(player.lastMove);
        //spawnedBook.GetComponent<BookBehaviour>().DirectionChecker(player.lastMove);
    }
}