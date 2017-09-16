using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class CameraSwitch : MonoBehaviour {

    public GameObject Camera1;
    public GameObject Camera2;
    public int InversParams;
    FirstPersonController player; 
    // Use this for initialization
    private void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
       
            player.ForcameraFrorward = Vector3.forward;
            player.ForcameraRight = Vector3.right;
        
    
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InversParams == 0)
            {
                player.ForcameraFrorward = Vector3.forward;
                player.ForcameraRight = Vector3.right;
            }
            if (InversParams == 1)
            {
                player.ForcameraFrorward = -Vector3.forward;
                player.ForcameraRight = -Vector3.right;
            }
            Camera1.SetActive(true);
            Camera2.SetActive(false);
        }
    }

}
