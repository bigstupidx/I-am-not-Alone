using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[ExecuteInEditMode]
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

        Debug.DrawRay(transform.position, fwd * 3.5f, Color.yellow);


        if (Physics.Raycast(transform.position, fwd, out hit, 3.5F))
        {
            if (zombie.m_animator)
            {
                if (!zombie.agent.isStopped)
                {
                    zombie.agent.isStopped = true;
                    zombie.m_animator.SetBool("attack", true);
                }
            }
            if (hit.transform.CompareTag(Tags.Things))
            {

                if (hit.transform.GetComponent<HingeJoint>())
                {
                    if (hit.transform.GetComponent<NavMeshObstacle>().enabled)
                    {

                        Quaternion targetDoor = Quaternion.LookRotation(hit.transform.position);
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetDoor, Time.deltaTime);

                        if (hit.transform.GetComponent<Health>())
                        {
                            hit.transform.GetComponent<Health>().HelthDamage(zombie.damage, false);
                        }
                        else
                        {
                            hit.transform.GetChild(0).GetComponent<Health>().HelthDamage(zombie.damage, false);
                        }
                    }



                }

            }

            if (hit.transform.CompareTag("CraftMode"))

            {


                if (!zombie.source.isPlaying)
                {
                    zombie.source.PlayOneShot(zombie.zombieAtack);
                }
                if (hit.transform.GetComponent<Health>())
                {
                    hit.transform.GetComponent<Health>().HelthDamage(zombie.damage, false);
                }
                else
                {
                    hit.transform.GetChild(0).GetComponent<Health>().HelthDamage(zombie.damage, false);
                }


            }


            if (hit.transform.CompareTag("Player"))

            {
                hit.transform.GetComponent<Health>().HelthDamage(zombie.PlayerDamage, false);

                if (!zombie.source.isPlaying)
                {
                    zombie.source.PlayOneShot(zombie.zombieAtack);


                }


                if (zombie.DestoyAll)
                {
                    if (hit.transform.CompareTag("WallCrash"))

                    {
                        hit.transform.GetComponent<Health>().HelthDamage(zombie.damage, false);



                    }

                    if (!zombie.source.isPlaying)
                    {
                        zombie.source.PlayOneShot(zombie.zombieAtack);
                    }
                }

            }

        }
        else
        {
            if (zombie.m_animator)
            {
                if (zombie.agent.isStopped)
                {
                    zombie.agent.isStopped = false;
                    zombie.m_animator.SetBool("attack", false);
                }
            }
        }

    }
}


