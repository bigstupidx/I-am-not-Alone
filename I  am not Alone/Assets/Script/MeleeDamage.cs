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

            if (other.transform.tag == "CraftMode")
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

            if (other.transform.tag == "CraftFromMenu")
            {

                if (other.transform.GetComponent<Health>())
                {
                    other.transform.GetComponent<Health>().HelthDamage(Damage, true, transform.position);
                    system.Play();
                }






            }
            if (other.transform.tag == "Things")
            {

                if (other.transform.GetComponent<Health>())
                {
                    other.transform.GetComponent<Health>().HelthDamage(Damage, true, transform.position);
                    system.Play();
                }


            }
            if (other.transform.tag == "WallCrash")
            {

                if (other.transform.GetComponent<Health>())
                {
                    other.transform.GetComponent<Health>().HelthDamage(Damage, true, transform.position);
                    system.Play();

                }





            }

            if (other.transform.tag == "AI")
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


