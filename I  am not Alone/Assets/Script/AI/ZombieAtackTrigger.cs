using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAtackTrigger : MonoBehaviour {
    RaycastHit hit;
    Ray ray;

	// Use this for initialization
	void Start () {
		
	}

    private void Update ()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * 3f, Color.yellow);
        if (Physics.Raycast(transform.position, fwd,out hit, 3))
        {
            if (hit.transform.CompareTag("Things"))
            {
                hit.transform.GetComponent<Health>().HelthDamage(1);
            }
        }
  
    }
}
