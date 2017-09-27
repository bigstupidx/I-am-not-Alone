using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    public float Damage;
    handWeapon handWeapon;
    private void Start ()
    {
        handWeapon = transform.parent.parent.GetComponent<handWeapon>();
    }
    private void OnEnable ()
    {
        try
        {
            handWeapon = transform.parent.parent.GetComponent<handWeapon>();
        }
        catch (System.Exception)
        {


        }
    }
    // Use this for initialization
    private void OnTriggerStay (Collider other)
    {

        if (other.transform.tag == "CraftMode")
        {




            if (handWeapon.l)
            {
                try
                {
                    other.transform.GetComponent<Health>().HelthDamage(Damage,true);
                }
                catch (System.Exception)
                {


                }

            }






        }

        if (other.transform.tag == "CraftFromMenu")
        {


            if (handWeapon.l)
            {
                try
                {
                    other.transform.GetComponent<Health>().HelthDamage(Damage, true);
                }
                catch (System.Exception)
                {


                }

            }








        }
        if (other.transform.tag == "Things")
        {


            if (handWeapon.l)
            {
                try
                {
                    other.transform.GetComponent<Health>().HelthDamage(Damage, true);
                }
                catch (System.Exception)
                {


                }

            }




        }
    
      if (other.transform.tag == "AI")
        {


            if (handWeapon.l)
            {
                try
                {
                    other.transform.GetComponent<Health>().HelthDamage (Damage, true);
}
                catch (System.Exception)
                {


                }

            }




        }
    }
}


