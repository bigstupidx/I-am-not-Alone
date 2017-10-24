using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour {
    public bool GroundTrigger;
    // Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
    private void OnTriggerEnter (Collider other)
    {
        if (other.transform.CompareTag("Untagged"))
        {
            GroundTrigger = true;
        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.transform.CompareTag("Untagged"))
        {
            GroundTrigger = false;
        }
    }
}
