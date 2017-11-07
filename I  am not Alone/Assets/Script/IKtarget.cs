using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKtarget : MonoBehaviour
{

    public Animator m_anim;
    public Transform target;
    public Transform targetBody;


    Transform body;

    Transform Hand;
    // Use this for initialization
    void Start ()
    {

        body = m_anim.GetBoneTransform(HumanBodyBones.Chest);

        //  Hand = m_anim.GetBoneTransform(HumanBodyBones.LeftHand);
    }

    // Update is called once per frame
    void Update ()
    {

        if (!m_anim.GetBool("handAttack"))
        {
            if (target)
            {
                //Quaternion r = Quaternion.LookRotation(target.position);
                //body.rotation = Quaternion.Lerp(body.rotation, r, Time.deltaTime);
                body.LookAt(target.position);

                //   Hand.transform.position = handTarget.position;
            }
            else
            {

                targetBody = transform;
                targetBody.position = transform.position;
                //Quaternion r = Quaternion.LookRotation(targetBody.position);
                //body.rotation = Quaternion.Lerp(body.rotation, r, Time.deltaTime/10);
                body.LookAt(targetBody.position);
            }


        }

    }


}
