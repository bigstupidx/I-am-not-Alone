using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticleCollision : MonoBehaviour
{
    private ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
    public float bulletDamage;
    int safeLength;
    public bool FireGun;
    public ParticleSystem wood;
    public ParticleSystem sparkl;
    private ParticleSystem old;

    Vector3 offset = new Vector3(0, -4000, 0);
    // Use this for initialization
    void Start ()
    {
        safeLength = GetComponent<ParticleSystem>().GetSafeCollisionEventSize();
        if (!FireGun)
        {
            wood.gameObject.SetActive(false);
            sparkl.gameObject.SetActive(false); 
        }
        //pool = PoolingSystem.Instance;
    }

    private void Update ()
    {
        if (old)
        {
            if (!old.isPlaying)
            {


                if (!FireGun)
                {
                    wood.gameObject.SetActive(false);
                    sparkl.gameObject.SetActive(false); 
                }
                old = null;
            } 
        }
    }


    void OnParticleCollision (GameObject other)
    {




        if (collisionEvents.Length < safeLength)
            collisionEvents = new ParticleCollisionEvent[safeLength];
        int numCollisionEvents = GetComponent<ParticleSystem>().GetCollisionEvents(other, collisionEvents);






        int i = 0;
        while (i < numCollisionEvents)
        {

            Vector3 collisionHitLoc = collisionEvents[i].intersection;

            old = sparkl;

            if (other.CompareTag("CraftMode"))
            {




                if (other.transform.root.name != transform.name)
                {
                    other.GetComponent<Health>().HelthDamage(bulletDamage, true, collisionHitLoc);

                }






            }

            if (other.CompareTag("WallCrash"))
            {

      

                if (other.transform.root.name != transform.name)
                {
                    other.GetComponent<Health>().HelthDamage(bulletDamage, true, collisionHitLoc);


                }

            }
            if (other.CompareTag("AI"))
            {


                // pool.InstantiateAPS("BloodSprayEffect", other.transform.position, Quaternion.identity);
                if (other.transform.root.name != transform.name)
                {
                    other.GetComponent<Health>().HelthDamage(bulletDamage, true, collisionHitLoc);
                    int l = Random.Range(0, 30);
                    if (l == 7)
                    {
                        other.GetComponent<ZombieLevel1>().timerStop = 1;
                    }

                }

            }
            if (other.CompareTag("CraftFromMenu"))
            {


     
                if (other.transform.root.name != transform.name)
                {
                    other.GetComponent<Health>().HelthDamage(bulletDamage, true, collisionHitLoc);


                }

            }
            if (other.CompareTag("Things"))
            {


   
                if (other.transform.root.name != transform.name)
                {


                    other.GetComponent<Health>().HelthDamage(bulletDamage, true, collisionHitLoc);

                    if (other.transform.name == "Door")
                    {

                        if (!FireGun)
                        {
                            old = wood; 
                        }


        

                    }


                }







            }






            if (!FireGun)
            {
                old.transform.position = collisionHitLoc;
                wood.gameObject.SetActive(true);
                sparkl.gameObject.SetActive(true);
                old.Play();

            }

 





            i++;

        }
    

    }



}








