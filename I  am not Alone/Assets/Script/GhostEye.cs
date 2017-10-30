using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GhostEye : MonoBehaviour
{
    RaycastHit hit;
    ZombieLevel1 zombie;
    // Use this for initialization
    void Start ()
    {
        zombie = GetComponent<ZombieLevel1>();
        // InvokeRepeating("MyUpdate", 0.0f, 1f);
    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        Debug.DrawRay(transform.position, fwd * 2f, Color.yellow);


        if (Physics.Raycast(transform.position, fwd, out hit, 2))
        {
            if (hit.transform.CompareTag("Things"))
            {
                if (hit.transform.GetComponent<HingeJoint>())
                {
                    if (hit.transform.GetComponent<NavMeshObstacle>().enabled)
                    {

                        Quaternion targetDoor = Quaternion.LookRotation(hit.transform.position);
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetDoor, Time.deltaTime);
                    }



                }
            }

        }
    }
}


