using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticleCollision : MonoBehaviour
{
    private ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
    public float bulletDamage;
    int safeLength;

    public ParticleSystem wood;
    public ParticleSystem sparkl;
    private ParticleSystem old;
    public GameObject decal;
    // Use this for initialization
    void Start ()
    {
        safeLength = GetComponent<ParticleSystem>().GetSafeCollisionEventSize();
        //pool = PoolingSystem.Instance;
    }

    void OnParticleCollision (GameObject other)
    {



        //if (old)
        //{
        if (collisionEvents.Length < safeLength)
            collisionEvents = new ParticleCollisionEvent[safeLength];
        int numCollisionEvents = GetComponent<ParticleSystem>().GetCollisionEvents(other, collisionEvents);

        if(numCollisionEvents == 0)
        {
            wood.Clear();
            sparkl.Clear();
            wood.gameObject.SetActive(false);
            sparkl.gameObject.SetActive(false);
        }

        if (numCollisionEvents != 0)
        {
            wood.gameObject.SetActive(true);
            sparkl.gameObject.SetActive(true);

            int i = 0;
            while (i < numCollisionEvents)
            {

                Vector3 collisionHitLoc = collisionEvents[i].intersection;

                old = sparkl;
                wood.Clear();
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

                            old = wood;

                            sparkl.Clear();
                        }


                    }







                }
                old.transform.position = collisionHitLoc;
                //   decal.transform.position = collisionHitLoc;
                old.Play();

                //  Instantiate(smoke, collisionHitLoc, Quaternion.identity);
                i++;

                //  } 
            }
        }
    }
}
