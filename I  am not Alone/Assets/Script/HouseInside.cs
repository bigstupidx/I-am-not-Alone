using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInside : MonoBehaviour
{

    public GameObject TwoFloour;
    public GameObject[] Roof;
    public BetweenFloor betweenFloor;
    // Use this for initialization
    void Start ()
    {

    }
    private void OnTriggerEnter (Collider other)
    {
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
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {

            for (int i = 0; i < Roof.Length; i++)
            {
                Roof[i].SetActive(true);
            }

            TwoFloour.SetActive(true);




        }
    }
}
