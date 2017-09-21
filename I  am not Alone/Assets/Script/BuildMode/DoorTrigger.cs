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
    public Material[] materials;
    HingeJoint hinge;
    bool PlayerHere;
    bool close = false;
    Renderer rend;
    // Use this for initialization
    void Start ()
    {
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();

        obstacle = transform.parent.GetComponent<NavMeshObstacle>();
        rigid = transform.parent.GetComponent<Rigidbody>();

        hinge = transform.parent.GetComponent<HingeJoint>();


        // Make the spring reach shoot for a 70 degree angle.
        // This could be used to fire off a catapult.


        rend = transform.parent.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];


    }





    private void OnTriggerStay (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (rigid.isKinematic)
            {
                obstacle.enabled = true;
                rend.sharedMaterial = materials[1];
            }
            else
            {
                obstacle.enabled = false;
                rend.sharedMaterial = materials[0];
            }
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
            if (rigid.isKinematic)
            {
                obstacle.enabled = true;
                rend.sharedMaterial = materials[1];
            }
            else
            {
                obstacle.enabled = false;
                rend.sharedMaterial = materials[0];
            }
            if (craftItem.Built)
            {
                rigid.isKinematic = true;
                obstacle.enabled = true;
            }


        }
    }
}
