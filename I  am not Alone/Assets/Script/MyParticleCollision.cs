using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticleCollision : MonoBehaviour
{
    private ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
    public int bulletDamage;
    // Use this for initialization
    void Start ()
    {

    }

    void OnParticleCollision (GameObject other)
    {


        if (other.tag == "CraftMode")
        {




            if (other.transform.root.name != transform.name)
            {
                other.GetComponent<Health>().HelthDamage(bulletDamage);

            }






        }

        if (other.tag == "WallCrash")
        {



            if (other.transform.root.name != transform.name)
            {
                other.GetComponent<Health>().HelthDamage(bulletDamage);


            }

        }
        if (other.tag == "CraftFromMenu")
        {



            if (other.transform.root.name != transform.name)
            {
                other.GetComponent<Health>().HelthDamage(bulletDamage);


            }

        }
        if (other.tag == "Things")
        {


            
            if (other.transform.root.name != transform.name)
            {
                other.GetComponent<Health>().HelthDamage(bulletDamage);


            }







        }

        int safeLength = GetComponent<ParticleSystem>().GetSafeCollisionEventSize();
        if (collisionEvents.Length < safeLength)
            collisionEvents = new ParticleCollisionEvent[safeLength];
        int numCollisionEvents = GetComponent<ParticleSystem>().GetCollisionEvents(other, collisionEvents);
        int i = 0;
        while (i < numCollisionEvents)
        {
            Vector3 collisionHitLoc = collisionEvents[i].intersection;

            //  Instantiate(smoke, collisionHitLoc, Quaternion.identity);
            i++;

        }

    }
}
