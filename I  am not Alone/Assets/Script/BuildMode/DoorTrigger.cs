using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DoorTrigger : MonoBehaviour
{
    private SwitchMode buildMode;

    public NavMeshObstacle obstacle;
    public CraftItem craftItem;
    public OffMeshLink offmeshLink;
    Rigidbody rigid;

    HingeJoint hinge;
    bool PlayerHere;
    bool close = false;
    Renderer rend;
    // Use this for initialization
    void Start ()
    {
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();

        obstacle = transform.GetComponent<NavMeshObstacle>();
        rigid = transform.GetComponent<Rigidbody>();

        hinge = transform.GetComponent<HingeJoint>();


        // Make the spring reach shoot for a 70 degree angle.
        // This could be used to fire off a catapult.





    }

    public void DoorClosed(bool close)
    {
        rigid.isKinematic = close;
        obstacle.enabled = close;
    }

    private void Update ()
    {
        //if (rigid.isKinematic)
        //{
        //    obstacle.enabled = true;
        //    //    rend.sharedMaterial = materials[1];
        //}
        //else
        //{
        //    obstacle.enabled = false;
        //    //    rend.sharedMaterial = materials[0];
        //}
        if (craftItem.Built)
        {
            rigid.isKinematic = true;
            obstacle.enabled = true;
        }
    }



}
