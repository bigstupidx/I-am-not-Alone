using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneObjectRandom : MonoBehaviour
{

    int randomObjectActive;

    // Use this for initialization
    void Start ()
    {
        Debug.Log(transform.childCount);
        randomObjectActive = Random.Range(0, transform.childCount);
 
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i != randomObjectActive)
            {
             Destroy(transform.GetChild(i).gameObject);
            }
        }


    }

}
