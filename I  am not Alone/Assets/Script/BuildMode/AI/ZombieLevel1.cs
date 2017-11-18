using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AC.LSky;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Health))]

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class ZombieLevel1 : MonoBehaviour
{
    public NavMeshAgent agent;
    public static float avoidancePredictionTime = 0.5f;
    public bool ThingsDamage;
    public float PlayerDamage;
    public float damage;
    public Transform newTraget;
    public float timeBetweenAttacks = 0.5f;
    public float timerStop;
    float timer;
    public NavMeshPath navMeshPathPlayer;
    public bool JointWindow;
    public bool damageWindow;
    public bool DestoyAll;
    public float standartSpeed;
    public bool RigidExplosion;


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
    public Animator m_animator;
    [HideInInspector]
    public AudioSource source;


    public int currentTarget = 0;
    public int OldTarget;
    LineRenderer lineRender;

    // Use this for initialization
    void Start ()
    {
        NavMesh.avoidancePredictionTime = 5;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        InvokeRepeating("DayDestroyObject", 0.0f, 0.5f);
        health = GetComponent<Health>();
        rigi = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();

        navMeshPathPlayer = new NavMeshPath();

        lineRender = GetComponent<LineRenderer>();


        transform.tag = Tags.AI;

        if (!m_animator)
        {
            m_animator = GetComponent<Animator>();
        }

          Physics.IgnoreCollision(transform.GetComponent<Collider>(), player.GetComponent<Collider>());


    }

    void DayDestroyObject ()
    {
        if (_sky.IsDay)
        {
            if (health.CurHelth > 0)
            {
                health.HelthDamage(0.9f, false, transform.position);
            }
        }

    }
    // Update is called once per frame
    void Update ()
    {


        if (timerStop > 0)
        {
            timerStop -= Time.deltaTime;
        }

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


            if (agent.isStopped)
            {
                agent.isStopped = false;
            }


            //if ((player.transform.position - transform.position).sqrMagnitude < Mathf.Pow(agent.stoppingDistance, 2))
            //{
            //    // If the agent is in attack range, become an obstacle and
                // disable the NavMeshAgent component
                //    obstacle.enabled = true;
                //    agent.enabled = false;
                //Vector3 relativePos = player.transform.position - transform.position;
                //Quaternion rotation = Quaternion.LookRotation(relativePos);
                //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 7f);
            //}
            //else
            //{


                //   If we are not in range, become an agent again
                //   obstacle.enabled = false;
                //  agent.enabled = true;
                if (m_animator)
                {
                    if (!newTraget)
                    {

                        CalculateNewPath(player);

                    }
                    else
                    {

                        CalculateNewPath(newTraget);

                    }
                }
           // }




        }
        else
        {

            if (!source.isPlaying)
            {
                source.PlayOneShot(zombieStay);
            }

            if (!agent.isStopped)
            {
                agent.isStopped = true;
            }

        }




    }



    void CalculateNewPath (Transform target)
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks)
        {
            timer = 0;
            //lineRender.positionCount = navMeshPathPlayer.corners.Length;
            //lineRender.SetPositions(navMeshPathPlayer.corners);
            Vector3 fwd = transform.TransformDirection(Vector3.forward * 0.5f);

            //  Debug.DrawRay(transform.position, fwd * 2.5f, Color.yellow);
            if (Physics.Raycast(transform.position, fwd, out hit, 2.5F))
            {




                if (hit.transform.GetComponent<PriorityObject>())
                {
                    if (target.CompareTag(Tags.player))
                    {
                        if (hit.transform.CompareTag(Tags.player))
                        {
                            target.GetComponent<Health>().HelthDamage(PlayerDamage, false, hit.point);

                            if (!source.isPlaying)
                            {
                                source.PlayOneShot(zombieAtack);


                            }
                            if (m_animator)
                            {
                                if (!agent.isStopped)
                                {

                                    agent.isStopped = true;
                                    m_animator.SetBool("attack", true);

                                }
                            }

                        }
                        else
                        {
                            if (m_animator)
                            {
                                if (agent.isStopped)
                                {

                                    agent.isStopped = false;
                                    m_animator.SetBool("attack", false);

                                }
                            }
                        }
                    }
                    else
                    {


                        if (target.GetComponent<Health>())
                        {
                            target.GetComponent<Health>().HelthDamage(damage, false, hit.point);
                        }
                        else
                        {
                            target.transform.GetChild(0).GetComponent<Health>().HelthDamage(damage, false, hit.point);
                        }

                        //if (!source.isPlaying)
                        //{
                        //    source.PlayOneShot(zombieAtack);


                        //}

                        if (m_animator)
                        {
                            if (!agent.isStopped)
                            {

                                agent.isStopped = true;
                                m_animator.SetTrigger("door");
                                m_animator.SetBool("attack", true);

                            }
                        }
                    }



                    GhostAnswer(hit.transform, hit.transform.GetComponent<PriorityObject>());


                }
            }
            else
            {
                newTraget = null;
                agent.SetDestination(target.position);
                if (m_animator)
                {
                    if (agent.isStopped)
                    {
                        source.PlayOneShot(zombieStay);
                        agent.isStopped = false;
                        m_animator.SetBool("attack", false);

                    }
                    else
                    {
                        m_animator.SetBool("attack", false);
                    }
                }
            }





        }




    }


    void GhostAnswer (Transform target, PriorityObject Object)
    {
        if (Object.Priority == 0)
        {
            if (Object.gameObject.GetComponent<DoorTrigger>())
            {
                if (Object.gameObject.GetComponent<DoorTrigger>().rigid.isKinematic)
                {

                    newTraget = target;

                }
                else
                {


                    newTraget = null;
                }
            }
            else
            {
                newTraget = target;
            }

        }
        if (Object.Priority == 1)
        {
            int r = Random.Range(0, 2);
            if (r == 1)
            {
                newTraget = target;

            }
            else
            {
                newTraget = null;
            }
        }
        if (Object.Priority == 2)
        {
            int r = Random.Range(0, 4);
            if (r == 3)
            {
                newTraget = target;

            }
            else
            {
                newTraget = null;
            }
        }
        if (Object.Priority == 3)
        {
            int r = Random.Range(0, 8);
            if (r == 4)
            {
                newTraget = target;

            }
            else
            {
                newTraget = null;
            }

        }

    }









}













