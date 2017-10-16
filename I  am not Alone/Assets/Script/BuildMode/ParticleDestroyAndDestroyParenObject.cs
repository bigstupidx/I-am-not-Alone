using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyAndDestroyParenObject : MonoBehaviour
{


    ParticleSystem system;
    // Use this for initialization
    void Start ()
    {
        system = GetComponent<ParticleSystem>();


        InvokeRepeating("DestroyPart", 0.0f, 5.0f);

    }

    void DestroyPart ()
    {

        if (!system.IsAlive())
        {

            gameObject.DestroyAPS();

        }

    }
}
