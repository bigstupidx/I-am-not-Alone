using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInside : MonoBehaviour
{

    public GameObject TwoFloour;
    public GameObject[] Roof;
    public BetweenFloor betweenFloor;
    public bool playerOutSide;
    public TrasnparentWall trasnparentWall;
    // Use this for initialization

    private void OnTriggerEnter (Collider other)
    {
        trasnparentWall.OffTransoarent = true;
        playerOutSide = true;
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < Roof.Length; i++)
            {
                Roof[i].SetActive(false);
            }
            TwoFloour.SetActive(false);
            //        betweenFloor.RenderObjectToFloor(false, "Ignore Raycast");
            betweenFloor.RenderObjectToFloor(false);
        }
        if (other.CompareTag(Tags.AI))
        {
            other.GetComponent<Rigidbody>().detectCollisions = false;
        }
    }
    private void OnTriggerExit (Collider other)
    {

        playerOutSide = false;
        trasnparentWall.OffTransoarent = false;
        if (other.CompareTag("Player"))
        {

            for (int i = 0; i < Roof.Length; i++)
            {
                Roof[i].SetActive(true);
            }

            TwoFloour.SetActive(true);




        }
        if (other.CompareTag(Tags.AI))
        {
            other.GetComponent<Rigidbody>().detectCollisions = true;
        }
    }
}
