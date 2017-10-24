using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobaCamera : MonoBehaviour
{
    public bool ForPlayer;
    public Transform target;
    public Vector3 defaultDistance = new Vector3(0f, 2f, -10f);
    public float distanceDamp = 10;
    public float rotationDamp = 10f;

    Transform myT;
    // Use this for initialization
    void Start ()
    {
        myT = transform;
        if (ForPlayer)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;

        }
    }

    private void LateUpdate ()
    {
        Vector3 toPos = target.position + (target.rotation * defaultDistance);
        Vector3 curos = Vector3.Lerp(myT.position, toPos, distanceDamp * Time.deltaTime);
        myT.position = curos;

        Quaternion toRot = Quaternion.LookRotation(target.position - myT.position, target.up);
        Quaternion curRot = Quaternion.Slerp(myT.rotation, toRot, rotationDamp * Time.deltaTime);
        myT.rotation = curRot;
    }
}
