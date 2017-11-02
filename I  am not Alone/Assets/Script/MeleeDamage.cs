using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    public float Damage;
    public handWeapon weapon;
    // Use this for initialization
    private void OnTriggerStay (Collider other)
    {
        if (weapon.fight)
        {

            if (other.transform.tag == "CraftMode")
            {


                other.transform.GetComponent<Health>().HelthDamage(Damage, true);


            }

            if (other.transform.tag == "CraftFromMenu")
            {


                other.transform.GetComponent<Health>().HelthDamage(Damage, true);







            }
            if (other.transform.tag == "Things")
            {


                other.transform.GetComponent<Health>().HelthDamage(Damage, true);



            }

            if (other.transform.tag == "AI")
            {


                other.transform.GetComponent<Health>().HelthDamage(Damage, true);




            } 
        }
    }
}


