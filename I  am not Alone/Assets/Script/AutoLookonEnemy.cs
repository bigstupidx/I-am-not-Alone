using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLookonEnemy : MonoBehaviour
{

    public IKtarget iktarget;




    public Transform TargetAi;

    // Use this for initialization


    private void Update ()
    {


        if (TargetAi)
        {
            iktarget.target = TargetAi;
        }
        else
        {
            iktarget.target = null;
        }


    }
    private void OnTriggerStay (Collider other)
    {


        if (other.CompareTag("AI"))
        {
            TargetAi = other.transform.GetChild(0);



        }
    }
    private void OnTriggerExit (Collider other)
    {

        if (other.CompareTag("AI"))
        {

            TargetAi = null;

            iktarget.target = TargetAi;


        }
    }

}
