using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSun : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.Play("ShotSun");
    }
}
