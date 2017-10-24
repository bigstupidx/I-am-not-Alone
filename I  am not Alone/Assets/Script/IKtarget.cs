using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKtarget : MonoBehaviour
{

    public Animator m_anim;
    public Transform target;
    public Transform targetBody;
    public Transform handTarget;
    public Vector3 offset;

    Transform body;

    Transform Hand;
    // Use this for initialization
    void Start ()
    {

        body = m_anim.GetBoneTransform(HumanBodyBones.Chest);
    
        Hand = m_anim.GetBoneTransform(HumanBodyBones.LeftHand);
    }

    // Update is called once per frame
    void Update ()
    {
       
        if (!m_anim.GetBool("handAttack"))
        {
            if (target)
            {
          
                body.LookAt(target.position);

             //   Hand.transform.position = handTarget.position;
            }
            else
            {
             
                targetBody = transform;
                targetBody.position = transform.position;
                body.LookAt(targetBody.position);
            }

            body.rotation = body.rotation * Quaternion.Euler(offset);
        }

    }
}
