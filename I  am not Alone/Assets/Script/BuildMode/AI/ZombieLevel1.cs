using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AC.LSky;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NavMeshObstacle))]
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
    public float timerStop;
    public NavMeshPath navMeshPath;
    public bool JointWindow;
    public bool damageWindow;
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
    public Animator m_animator;
    AudioSource source;
    GameObject[] targetsDistance;
    public List<Transform> PriorityTarget = new List<Transform>();
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
        navMeshPath = new NavMeshPath();
        targetsDistance = GameObject.FindGameObjectsWithTag("TargetDistance");
        lineRender = GetComponent<LineRenderer>();
        SetNewPriority();



        if (!m_animator)
        {
            m_animator = GetComponent<Animator>();
        }

        //  Physics.IgnoreCollision(transform.GetComponent<Collider>(), other.GetComponent<Collider>());


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


        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * 3f, Color.yellow);

        if (Physics.Raycast(transform.position, fwd, out hit, 3))
        {

            if (m_animator)
            {
                if (ThingsDamage)
                {
                    if (hit.transform.CompareTag("Things"))

                    {
                        m_animator.SetBool("attack", true);
                        hit.transform.GetComponent<Health>().HelthDamage(damage, false);

                        if (!source.isPlaying)
                        {
                            source.PlayOneShot(zombieAtack);
                        }

                    }
                    else
                    {
                        m_animator.SetBool("attack", false);
                    }
                    if (hit.transform.CompareTag("CraftMode"))

                    {
                        m_animator.SetBool("attack", true);
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
                    else
                    {
                        m_animator.SetBool("attack", false);
                    }


                }

                if (hit.transform.CompareTag("Player"))

                {
                    hit.transform.GetComponent<Health>().HelthDamage(PlayerDamage, false);

                    if (!source.isPlaying)
                    {
                        source.PlayOneShot(zombieAtack);
                        m_animator.SetBool("attack", true);
                    }
                    else
                    {
                        m_animator.SetBool("attack", false);
                    }

                    if (DestoyAll)
                    {
                        if (hit.transform.CompareTag("WallCrash"))

                        {
                            hit.transform.GetComponent<Health>().HelthDamage(damage, false);
                            m_animator.SetBool("attack", true);

                        }
                        else
                        {
                            m_animator.SetBool("attack", false);
                        }
                        if (!source.isPlaying)
                        {
                            source.PlayOneShot(zombieAtack);
                        }
                    }

                }

            }

        }
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
                if (m_animator)
                {
                    if (!newTraget)
                    {
                        m_animator.SetBool("walk", true);
                        CalculateNewPath(player);

                    }
                    else
                    {
                        currentTarget = 0;
                        m_animator.SetBool("walk", true);
                        agent.SetDestination(newTraget.position);
                    }
                }
            }




        }
        else
        {

            if (!source.isPlaying)
            {
                source.PlayOneShot(zombieStay);
            }
            if (m_animator)
            {
                //   m_animator.SetBool("walk", false);
                m_animator.SetBool("attack", false);
            }
            stoping = true;
            agent.isStopped = true;
        }




    }




    void CalculateNewPath (Transform targetPosition)
    {
        //if (targetPosition)
        //{
            agent.CalculatePath(targetPosition.position, navMeshPath);
        //  }
        //else
        //{
        //    SetNewPriority();
        //}

        lineRender.positionCount = navMeshPath.corners.Length;
        lineRender.SetPositions(navMeshPath.corners);
        
        //for (int i = 0; i < navMeshPath.corners.Length - 1; i++)
        //{
        //    Debug.DrawLine(navMeshPath.corners[i], navMeshPath.corners[i + 1], Color.red);

        //}
        agent.SetDestination(targetPosition.position);

        if (navMeshPath.status == NavMeshPathStatus.PathPartial)
        {
        //    PathFinding();
        }
        //switch (navMeshPath.status)
        //{
        //    case NavMeshPathStatus.PathComplete:
        //        agent.SetDestination(targetPosition.position);
        //        break;
        //    case NavMeshPathStatus.PathPartial:

        //        break;
        //    case NavMeshPathStatus.PathInvalid:
        //        break;
        //    default:
        //        break;
        //}


    }


    void SetNewPriority ()
    {
       
        PriorityTarget.Clear();
       // PriorityTarget.Add(player);
        currentTarget = 0;

        for (int i = 0; i < targetsDistance.Length; i++)
        {
            PriorityTarget.Add(targetsDistance[i].transform);

        }
    }

    void PathFinding ()
    {

        //if (PriorityTarget.Count == 1)
        //{
        //    SetNewPriority();

        //}

        //if (currentTarget != 0)
        //{
        //    PriorityTarget.RemoveAt(OldTarget);
        //}
        //  var second = PriorityTarget[0].transform;
        var dist = Vector3.Distance(player.position, PriorityTarget[currentTarget].transform.position);   // Note 1
        for (var i = 0; i < PriorityTarget.Count; i++)
        {                   // Note 2
            var tempDist = Vector3.Distance(player.position, PriorityTarget[i].transform.position);
            if (tempDist < dist)
            {
                //   second = PriorityTarget[i].transform;
                Debug.Log(currentTarget);
                currentTarget = i;
                OldTarget = i;
            }
        }






    }


}










