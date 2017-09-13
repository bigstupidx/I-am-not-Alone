using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenFloor : MonoBehaviour {
    public GameObject Floor;
    // Use this for initialization
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Floor.SetActive(true);

        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Floor.SetActive(false);

        }
    }
}
