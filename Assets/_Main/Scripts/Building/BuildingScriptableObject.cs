using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingScriptableObject", menuName = "ScriptableObjects/Building")]
public class BuildingScriptableObject : ScriptableObject
{
    [Header("Building Stats")]
    [SerializeField] private float maxHealth;
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    [SerializeField] private bool invulnerable = false;
    public bool Invulnerable { get => invulnerable; set => invulnerable = value; }

    [SerializeField] float invulnerableTime;
    public float InvulnerableTime { get => invulnerableTime; set => invulnerableTime = value; }
}
