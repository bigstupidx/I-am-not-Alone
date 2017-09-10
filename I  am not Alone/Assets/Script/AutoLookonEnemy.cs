using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLookonEnemy : MonoBehaviour
{

    Transform weapon;
    public List<GameObject> enemy = new List<GameObject>();
    float MaxDistance;
    float MinDistance;
    int c = 0;
    Vector3 defaultPosition;
    // Use this for initialization
    void Start ()
    {

        weapon = transform.GetChild(0);
    }
    private void Update ()
    {

        if (enemy.Count == 0)
        {
            weapon.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            return;
        }
        if (enemy.Count == 1)
        {
            weapon.transform.LookAt(enemy[c].transform.position);
        }
        else
        {
            MinDistance = Vector2.Distance(transform.root.position, enemy[c].transform.position);
            for (int i = 0; i < enemy.Count; i++)
            {
                float newDistance = Vector2.Distance(transform.root.position, enemy[i].transform.position);
                if (newDistance <= MinDistance)
                {
                    MinDistance = newDistance;
                    c = i;
                    weapon.transform.LookAt(enemy[i].transform.position);




                }
            }
        }

    }
    private void OnTriggerStay (Collider other)
    {
        weapon = transform.GetChild(0);

        if (other.CompareTag("AI"))
        {
            if (enemy.Find(obj => obj.name == other.gameObject.name))
            {

            }
            else
            {
                enemy.Add(other.gameObject);
            }



        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("AI"))
        {
            enemy.Clear();

            c = 0;



        }
    }

}
