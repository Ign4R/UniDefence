using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/Player")]
public class PlayerScriptableObject : ScriptableObject
{
    [Header("Player Stats")]
    [SerializeField] private float maxHealth;
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    [SerializeField] private bool invulnerable = false;
    public bool Invulnerable { get => invulnerable; set => invulnerable = value; }

    [SerializeField] float invulnerableTime;
    public float InvulnerableTime { get => invulnerableTime; set => invulnerableTime = value; }

    [SerializeField] float blinkRate = 0.01f;
    public float BlinkRate { get => blinkRate; set => blinkRate = value; }
}
