using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HelpersAi : MonoBehaviour
{
    public Transform[] targets;
    NavMeshAgent agent;
    int currentTarget;
    // Use this for initialization
    void Start ()
    {
        currentTarget = Random.Range(0, targets.Length);
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update ()
    {
        float distance = Vector3.Distance(transform.position, targets[currentTarget].position);
        agent.SetDestination(targets[currentTarget].position);
        if (distance < 5)
        {
            Destroy(gameObject);
        }
     
     
    }
}
