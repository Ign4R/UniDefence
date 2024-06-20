using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base script of all malee behaviour
/// se refiere a armas como el ajo.. tipo bombas..
/// </summary>

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public float destroyAfterSeconds;

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }
}
