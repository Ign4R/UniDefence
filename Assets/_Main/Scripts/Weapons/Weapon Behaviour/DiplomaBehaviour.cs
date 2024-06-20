using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiplomaBehaviour : WeaponBehaviour
{
    DiplomaController diplomaController;

    protected override void Start()
    {
        base.Start();
        diplomaController = FindObjectOfType<DiplomaController>();
    }

    private void Update()
    {
        transform.position += diplomaController.speed * Time.deltaTime * direction;
    }
}
