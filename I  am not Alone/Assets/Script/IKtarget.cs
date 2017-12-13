using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKtarget : MonoBehaviour
{

    public Animator m_anim;

    public Transform targetBody;


    Transform body;

    Transform Head;
    // Use this for initialization
    void Start ()
    {

        body = m_anim.GetBoneTransform(HumanBodyBones.Chest);
        Head = m_anim.GetBoneTransform(HumanBodyBones.Head);
        //    body.LookAt(targetBody.position);
        Head.LookAt(targetBody.position);
        //  Hand = m_anim.GetBoneTransform(HumanBodyBones.LeftHand);
    }

    //// Update is called once per frame
    void Update ()
    {

        if (!m_anim.GetBool("handAttack"))
        {


            //targetBody = transform;
            //targetBody.position = transform.position;
            //Quaternion r = Quaternion.LookRotation(targetBody.position);
            //body.rotation = Quaternion.Lerp(body.rotation, r, Time.deltaTime/10);
            //   body.LookAt(targetBody.position);
            Head.LookAt(targetBody.position);



        }

    }


}
