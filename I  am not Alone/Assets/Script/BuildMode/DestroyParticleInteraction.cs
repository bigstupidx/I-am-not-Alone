using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleInteraction : MonoBehaviour {
    ParticleSystem system;


	// Use this for initialization
	void Start () {
        system = GetComponent<ParticleSystem>();
        InvokeRepeating("DestroyParticle", 0.0f, 1.0f);
	}
	
	// Update is called once per frame
	
}
