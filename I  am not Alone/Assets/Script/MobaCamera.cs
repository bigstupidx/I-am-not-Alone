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
    [Space(5)]
    public float power = 0.7f;
    public float duration = 1.0f;
    public float slowDownAmount = 1.0f;
    public bool shouldShake = false;
    Vector3 startPos;
    float initialDuration;
    float initialPower;
    Transform myT;

    // Use this for initialization
    void Start ()
    {
        myT = transform;

        initialPower = power;
        initialDuration = duration;
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
        if (shouldShake)
        {
            if (duration > 0)
            {
                myT.localPosition = curos + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                power = initialPower;
            }
        }
    }
}
