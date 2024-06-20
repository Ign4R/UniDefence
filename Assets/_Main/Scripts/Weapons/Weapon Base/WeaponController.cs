using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base script for all weapon
/// </summary>

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject prefab;
    public float damage;
    public float speed;
    public float cooldownDuration; //enfriamiento.
    float currentCooldown;
    public int pierce;
    //la cant de tiempo que un arma puede ir
    //hasta alcanzar a un enemigo antes de ser destruido.

    protected Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    protected virtual void Start()
    {
        currentCooldown = cooldownDuration;
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        //reinicia el ciclo de tiempo.
        currentCooldown = cooldownDuration;
    }
}
