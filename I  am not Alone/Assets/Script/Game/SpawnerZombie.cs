using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AC.LSky;
public class SpawnerZombie : MonoBehaviour
{
    public int spawnerRadius;
    PoolingSystem poolsistem;
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnerRadius);
    }

    private void OnEnable ()
    {
        poolsistem = PoolingSystem.Instance;
    }
    public void CreateZombie (GameObject prefZombie, LSky sky)
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
       
            GameObject o = poolsistem.InstantiateAPS(prefZombie.name, randomLoc3d, Quaternion.identity);
            o.GetComponent<ZombieLevel1>()._sky = sky;
            o.GetComponent<ZombieLevel1>().agent.avoidancePriority += Random.Range(-20, +20);
            o.GetComponent<ZombieLevel1>().standartSpeed = o.GetComponent<ZombieLevel1>().agent.speed;
      


    }

}
