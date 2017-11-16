using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoizonEffect : MonoBehaviour
{
    ParticleSystem system;
    GameObject Player;
    private void Start ()
    {
        system = GetComponent<ParticleSystem>();
        system.Play();
        Player = GameObject.FindGameObjectWithTag(Tags.player);
    }




    private void OnParticleCollision (GameObject other)
    {
        if (other.transform.CompareTag(Tags.player))
        {

            other.GetComponent<Health>().HelthDamage(0.5f, false, other.transform.position);

        }
    }
}
