using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioSource bombClip;

    void Start()
    {
        bombClip.Play();    
    }
}
