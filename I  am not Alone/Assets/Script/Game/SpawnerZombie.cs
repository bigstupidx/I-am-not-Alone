using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerZombie : MonoBehaviour
{
    public int spawnerRadius;
    public bool Zombielevel1;
    public bool ZombieDog;
    public bool ZombieMutant;
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnerRadius);
    }

    public void CreateZombie (GameObject prefZombie)
    {

        // Choose a random location within the spawnRadius
        Vector2 randomLoc2d = Random.insideUnitCircle * spawnerRadius;
        Vector3 randomLoc3d = new Vector3(transform.position.x + randomLoc2d.x, transform.position.y, transform.position.z + randomLoc2d.y);
        // Make sure the location is on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomLoc3d, out hit, 100, 1))
        {
            randomLoc3d = hit.position;
        }
        // Instantiate and make the enemy a child of this object
        GameObject o = (GameObject)Instantiate(prefZombie, randomLoc3d, transform.rotation);
        if (Zombielevel1)
        {
            o.GetComponent<ZombieLevel1>().agent.speed = Random.Range(3.0f, 4.5f);
        }
        else if (ZombieDog)
        {
            o.GetComponent<ZombieLevel1>().agent.speed = Random.Range(5.0f, 8f);
        }
        else if (ZombieMutant)
        {
            o.GetComponent<ZombieLevel1>().agent.speed = Random.Range(2, 4f);
        }
    }

}
