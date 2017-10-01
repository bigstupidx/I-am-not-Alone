using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AC.LSky;
public class ZombieLevel1 : MonoBehaviour
{
    public NavMeshAgent agent;

    public bool ThingsDamage;
    public float PlayerDamage;
    public float damage;
    public Transform newTraget;
    public float timerStop;

    public bool JointWindow;
    public bool DestoyAll;
    public float standartSpeed;
    public bool RigidExplosion;
    //   public bool WinDowAttack;
    bool stoping;

    public LSky _sky;
    Health health;
    RaycastHit hit;
    Ray ray;
    NavMeshObstacle obstacle;
    Transform player;
    private Rigidbody rigi;
    public AudioClip zombieAtack;
    public AudioClip zombieStay;
    public AudioClip zombieDeth;
    AudioSource source;
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        InvokeRepeating("DayDestroyObject", 0.0f, 0.5f);
        health = GetComponent<Health>();
        rigi = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();

    }

    void DayDestroyObject ()
    {
        if (_sky.IsDay)
        {
            health.HelthDamage(0.9f, false);
        }

    }
    // Update is called once per frame
    void Update ()
    {
  
        timerStop -= Time.deltaTime;
        if (RigidExplosion)
        {
            if (agent.isOnNavMesh)
            {
                rigi.isKinematic = true;
                RigidExplosion = false;
                agent.enabled = true;
            }
        }
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
                        hit.transform.GetComponent<Health>().HelthDamage(damage, false);
                        if (!source.isPlaying)
                        {
                            source.PlayOneShot(zombieAtack);
                        }

                    }
                    if (hit.transform.CompareTag("CraftMode"))

                    {
                        if (!source.isPlaying)
                        {
                            source.PlayOneShot(zombieAtack);
                        }
                        if (hit.transform.GetComponent<Health>())
                        {
                            hit.transform.GetComponent<Health>().HelthDamage(damage, false);
                        }
                        else
                        {
                            hit.transform.GetChild(0).GetComponent<Health>().HelthDamage(damage, false);
                        }


                    }

                }

                if (hit.transform.CompareTag("Player"))

                {
                    hit.transform.GetComponent<Health>().HelthDamage(PlayerDamage, false);

                    if (!source.isPlaying)
                    {
                        source.PlayOneShot(zombieAtack);
                    }
                }

                if (DestoyAll)
                {
                    if (hit.transform.CompareTag("WallCrash"))

                    {
                        hit.transform.GetComponent<Health>().HelthDamage(damage, false);


                    }
                    if (!source.isPlaying)
                    {
                        source.PlayOneShot(zombieAtack);
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
                if (!newTraget)
                {

                    agent.SetDestination(player.position);

                }
                else
                {
                    agent.SetDestination(newTraget.position);
                }
            }




        }
        else
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(zombieStay);
            }

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
