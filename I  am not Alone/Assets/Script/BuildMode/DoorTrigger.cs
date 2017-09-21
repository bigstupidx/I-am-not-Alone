using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DoorTrigger : MonoBehaviour
{
    private SwitchMode buildMode;

    public NavMeshObstacle obstacle;
    public CraftItem craftItem;
    Rigidbody rigid;
    Material material;
    HingeJoint hinge;
    bool PlayerHere;
    bool close = false;
    // Use this for initialization
    void Start ()
    {
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();

        obstacle = transform.parent.GetComponent<NavMeshObstacle>();
        rigid = transform.parent.GetComponent<Rigidbody>();
        material = transform.parent.GetComponent<Material>();
        hinge = transform.parent.GetComponent<HingeJoint>();


        // Make the spring reach shoot for a 70 degree angle.
        // This could be used to fire off a catapult.



    }


    private void Update ()
    {

        if (hinge.angle <= 0.0f)
        {
            if (PlayerHere)
            {
                rigid.isKinematic = false;
            }
            else
            {
                rigid.isKinematic = true;
            }
            obstacle.enabled = true;
        }

    }


    private void OnTriggerStay (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHere = true;
            if (craftItem.Built)
            {
                rigid.isKinematic = true;
                obstacle.enabled = true;
            }


        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHere = false;

        }
    }
}
