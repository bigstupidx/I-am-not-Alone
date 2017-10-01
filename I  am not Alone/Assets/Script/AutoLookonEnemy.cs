using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLookonEnemy : MonoBehaviour
{

    Transform weapon;
    //   public List<GameObject> enemy = new List<GameObject>();
    float MaxDistance;
    float MinDistance;
    int c = 0;
    Vector3 defaultPosition;
    public Transform TargetAi;
    public GameObject imageBotton;
    // Use this for initialization


    private void Update ()
    {


        if (TargetAi)
        {
            weapon.transform.LookAt(TargetAi);
        }


    }
    private void OnTriggerStay (Collider other)
    {
        if (transform.childCount != 0)
        {

            weapon = transform.GetChild(0);
        }
        else
        {

            return;
        }

        if (other.CompareTag("AI"))
        {
            TargetAi = other.transform;



        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (transform.childCount != 0)
        {

            weapon = transform.GetChild(0);
        }
        else
        {

            return;
        }
        if (other.CompareTag("AI"))
        {

            TargetAi = null;


            weapon.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }
    }

}
