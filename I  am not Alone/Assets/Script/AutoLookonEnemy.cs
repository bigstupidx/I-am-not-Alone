using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLookonEnemy : MonoBehaviour
{

    Transform weapon;
 //   public List<GameObject> enemy = new List<GameObject>();
    float MaxDistance;
    float MinDistance;
    int c = 0;
    Vector3 defaultPosition;
    public Transform TargetAi;
    // Use this for initialization
    void Start ()
    {

        weapon = transform.GetChild(0);
    }
    private void Update ()
    {
        if (TargetAi)
        {
            weapon.transform.LookAt(TargetAi); 
        }
        //if (enemy.Count == 0)
        //{
        //    enemy.Clear();
        //    return;
        //}
        //if (enemy.Count == 1)
        //{
        //    if ((enemy[c].gameObject))
        //    {
        //        weapon.transform.LookAt(enemy[c].transform.position);
        //    }
        //    else { enemy.Clear(); }
        //}
        //else
        //{
        //    if ((enemy[c].gameObject))
        //    {
        //        MinDistance = Vector2.Distance(transform.root.position, enemy[c].transform.position);
        //    }
        //    else { enemy.Clear(); }
        //    for (int i = 0; i < enemy.Count; i++)
        //    {
        //        if (enemy[i].gameObject)
        //        {
        //            float newDistance = Vector2.Distance(transform.root.position, enemy[i].transform.position);
        //            if (newDistance <= MinDistance)
        //            {
        //                MinDistance = newDistance;
        //                c = i;
        //                weapon.transform.LookAt(enemy[i].transform.position);




        //            }
        //        }
        //        else
        //        {
        //            enemy.Clear();
        //        }
        //    }
        //}

    }
    private void OnTriggerStay (Collider other)
    {
        weapon = transform.GetChild(0);

        if (other.CompareTag("AI"))
        {
            TargetAi = other.transform;
            //if (enemy.Find(obj => obj.name == other.gameObject.name))
            //{

            //}
            //else
            //{
            //    enemy.Clear();
            //    enemy.Add(other.gameObject);
            //}



        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("AI"))
        {
            //enemy.Clear();
            TargetAi = null;
            //c = 0;

            weapon.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }
    }

}
