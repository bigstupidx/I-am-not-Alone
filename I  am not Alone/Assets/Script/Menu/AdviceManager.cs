using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdviceManager : MonoBehaviour
{
    int advice;
    // Use this for initialization
    void Start ()
    {
        advice = Random.Range(0, transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (i != advice)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

    }


}
