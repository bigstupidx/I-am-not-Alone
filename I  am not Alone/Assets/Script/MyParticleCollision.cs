using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticleCollision : MonoBehaviour
{
    private ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
    public float bulletDamage;
    int safeLength;
    public bool FireGun;



    // Use this for initialization
    void Start ()
    {
        safeLength = GetComponent<ParticleSystem>().GetSafeCollisionEventSize();

        //pool = PoolingSystem.Instance;
    }

    private void Update ()
    {
        //if (old)
        //{
        //    if (!old.isPlaying)
        //    {


        //        if (!FireGun)
        //        {

        //            sparkl.gameObject.SetActive(false);
        //        }
        //        old = null;
        //    }
        //}
    }


    void OnParticleCollision (GameObject other)
    {



        if (other.CompareTag("CraftMode"))
        {




            if (other.transform.root.name != transform.name)
            {
                other.GetComponent<Health>().HelthDamage(bulletDamage, true, transform.position);

            }






        }

        if (other.CompareTag("WallCrash"))
        {



            if (other.transform.root.name != transform.name)
            {
                other.GetComponent<Health>().HelthDamage(bulletDamage, true, transform.position);


            }

        }
        if (other.CompareTag("AI"))
        {
            other.GetComponent<ZombieLevel1>().m_animator.SetLayerWeight(1, 1);
            other.GetComponent<ZombieLevel1>().m_animator.SetTrigger(HashAnim.GhostTriggerHit);

            // pool.InstantiateAPS("BloodSprayEffect", other.transform.position, Quaternion.identity);
            if (other.transform.root.name != transform.name)
            {
                other.GetComponent<Health>().HelthDamage(bulletDamage, true, transform.position);
                int l = Random.Range(0, 30);
                if (l == 7)
                {
                    other.GetComponent<ZombieLevel1>().timerStop = 1;
                }

            }
            other.GetComponent<ZombieLevel1>().m_animator.SetLayerWeight(1, 0);

        }
        if (other.CompareTag("CraftFromMenu"))
        {



            if (other.transform.root.name != transform.name)
            {
                other.GetComponent<Health>().HelthDamage(bulletDamage, true, transform.position);


            }

        }
        if (other.CompareTag("Things"))
        {



            if (other.transform.root.name != transform.name)
            {


                other.GetComponent<Health>().HelthDamage(bulletDamage, true, transform.position);

                if (other.transform.name == "Door")
                {

                    if (!FireGun)
                    {

                    }




                }


            }













        }


    }



}








