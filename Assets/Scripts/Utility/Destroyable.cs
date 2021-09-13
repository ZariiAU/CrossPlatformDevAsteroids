using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public CharacterStats statObject;
    public ParticleSystem explosionEffect;
    void Start()
    {
        statObject = GetComponent<CharacterStats>();
    }

    
}
