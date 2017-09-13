using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInside : MonoBehaviour {

    public GameObject TwoFloour;
    public GameObject Roof;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Roof.SetActive(false);
            TwoFloour.SetActive(false);

        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Roof.SetActive(true);
            TwoFloour.SetActive(true);

        }
    }
}
