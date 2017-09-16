using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenFloor : MonoBehaviour {
    public GameObject Floor;

    public bool FloorCraft;
    public SwitchMode switchMode;
    // Use this for initialization
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FloorCraft = true;
            switchMode.CheckInBuiltWalls(true);
            Floor.SetActive(true);
         
         

        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FloorCraft = false;
            switchMode.CheckInBuiltWalls(true);
            Floor.SetActive(false);

        }
    }
}
