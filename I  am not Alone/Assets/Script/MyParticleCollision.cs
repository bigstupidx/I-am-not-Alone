using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticleCollision : MonoBehaviour
{
    private ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
    public float bulletDamage;

    // Use this for initialization
    void Start ()
    {
        //pool = PoolingSystem.Instance;
    }

    void OnParticleCollision (GameObject other)
    {
      

        if (other.CompareTag("CraftMode"))
        {




            if (other.transform.root.name != transform.name)
            {
                other.GetComponent<Health>().HelthDamage(bulletDamage, true);

            }






        }

        if (other.CompareTag("WallCrash"))
        {



            if (other.transform.root.name != transform.name)
            {
                other.GetComponent<Health>().HelthDamage(bulletDamage, true);


            }

        }
        if (other.CompareTag("AI"))
        {

           
           // pool.InstantiateAPS("BloodSprayEffect", other.transform.position, Quaternion.identity);
            if (other.transform.root.name != transform.name)
            {
                other.GetComponent<Health>().HelthDamage(bulletDamage, true);
                int l = Random.Range(0, 30);
                if (l ==7)
                {
                    other.GetComponent<ZombieLevel1>().timerStop = 1;
                }
               
            }

        }
        if (other.CompareTag("CraftFromMenu"))
        {



            if (other.transform.root.name != transform.name)
            {
                other.GetComponent<Health>().HelthDamage(bulletDamage, true);


            }

        }
        if (other.CompareTag("Things"))
        {


            
            if (other.transform.root.name != transform.name)
            {
                other.GetComponent<Health>().HelthDamage(bulletDamage, true);


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
