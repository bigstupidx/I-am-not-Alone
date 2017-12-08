using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    public float Damage;
    public handWeapon weapon;
    public ParticleSystem system;
    // Use this for initialization
    private void OnTriggerEnter (Collider other)
    {
        if (weapon.fight)
        {

            if (other.transform.CompareTag(Tags.CraftMode))
            {


                if (other.transform.parent.GetComponent<CraftItem>())
                {
                    if (other.transform.parent.GetComponent<CraftItem>().Built)
                    {
                        if (other.transform.GetComponent<Health>())
                        {
                            other.transform.GetComponent<Health>().HelthDamage(Damage, true, transform.position);
                            system.Play();
                        }
                    }
                }





            }

            if (other.transform.CompareTag(Tags.CraftFromMenu))
            {

                if (other.transform.GetComponent<Health>())
                {
                    other.transform.GetComponent<Health>().HelthDamage(Damage, true, transform.position);
                    system.Play();
                }






            }
            if (other.transform.CompareTag(Tags.Things))
            {

                if (other.transform.GetComponent<Health>())
                {
                    other.transform.GetComponent<Health>().HelthDamage(Damage, true, transform.position);
                    system.Play();
                }


            }
            if (other.transform.CompareTag(Tags.WallCrash))
            {

                if (other.transform.GetComponent<Health>())
                {
                    other.transform.GetComponent<Health>().HelthDamage(Damage, true, transform.position);
                    system.Play();

                }





            }

            if (other.transform.CompareTag(Tags.AI))
            {

                if (other.transform.GetComponent<Health>())
                {
                    other.transform.GetComponent<Health>().HelthDamage(Damage, true, transform.position);
                    system.Play();
                }



            }
        }
    }
}


