using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[ExecuteInEditMode]
public class GhostEye : MonoBehaviour
{
    RaycastHit hit;
    ZombieLevel1 zombie;
    // Use this for initialization
    void Start ()
    {
        zombie = GetComponent<ZombieLevel1>();
        // InvokeRepeating("MyUpdate", 0.0f, 1f);
    }


    
}


