using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerZombie : MonoBehaviour {
    public int spawnerRadius;

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,spawnerRadius);
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
            o.GetComponent<ZombieLevel1>().agent.speed = Random.Range(3.0f, 6.0f);
        
    }
 
}
