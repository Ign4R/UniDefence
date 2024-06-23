using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [Header("Weapon Stats")]
    [SerializeField] private GameObject prefab;
    public GameObject Prefab { get => prefab; set => prefab = value; }

    [SerializeField] private float damage;
    public float Damage { get => damage; set => damage = value; }

    [SerializeField] private float speed;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float cooldownDuration; //enfriamiento.
    public float CooldownDuration { get => cooldownDuration; set => cooldownDuration = value; }

    [SerializeField] private int lifeTime; //tiempo de alcance del arma..
    public int LifeTime { get => lifeTime; set => lifeTime = value; }

}