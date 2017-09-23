using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieLevel1 : MonoBehaviour
{
    public NavMeshAgent agent;
    NavMeshObstacle obstacle;
    Transform player;
    public bool ThingsDamage;
    public float damage;
    RaycastHit hit;
    Ray ray;
    public float timerStop;
    public bool WinDowAttack;
    bool stoping;
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();

    }


    // Update is called once per frame
    void Update ()
    {

        timerStop -= Time.deltaTime;
        if (timerStop <= 0)
        {

            if (stoping)
            {
                agent.isStopped = false;
                stoping = false;
            }
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(transform.position, fwd * 3f, Color.yellow);

            if (Physics.Raycast(transform.position, fwd, out hit, 3))
            {

                if (ThingsDamage)
                {
                    if (hit.transform.CompareTag("Things"))

                    {
                        hit.transform.GetComponent<Health>().HelthDamage(damage);


                    }
                    if (hit.transform.CompareTag("CraftMode"))

                    {
                        hit.transform.GetChild(0).GetComponent<Health>().HelthDamage(damage);


                    }
                    if (hit.transform.CompareTag("Player"))

                    {
                        hit.transform.GetComponent<Health>().HelthDamage(damage);


                    }
                }

            }


            if ((player.transform.position - transform.position).sqrMagnitude < Mathf.Pow(agent.stoppingDistance, 2))
            {
                // If the agent is in attack range, become an obstacle and
                // disable the NavMeshAgent component
                obstacle.enabled = true;
                agent.enabled = false;
                Vector3 relativePos = player.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 7f);
            }
            else
            {

                // If we are not in range, become an agent again
                obstacle.enabled = false;
                agent.enabled = true;
                agent.SetDestination(player.position);
            }




        }
        else
        {
            stoping = true;
            agent.isStopped = true;
        }
    }


    public void TransformRotation (Transform r)
    {
        Vector3 relativePos = r.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 7f);
    }
}
