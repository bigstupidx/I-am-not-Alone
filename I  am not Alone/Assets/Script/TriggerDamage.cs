using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour {

    private void OnTriggerStay (Collider other)
    {
        if (other.transform.CompareTag(Tags.player))
        {
            other.GetComponent<PlayerHealth>().HelthDamage(50);
        }
    }
}
