using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKtarget : MonoBehaviour
{

    public Animator m_anim;

    public Transform targetBody;
    public Transform WeaponActive;
    Transform targetNull;
    Transform body;
    BulletSystem bullSystem;
    Transform Head;
    // Use this for initialization
    void Start ()
    {
        targetNull = transform.GetChild(0);
        body = m_anim.GetBoneTransform(HumanBodyBones.Chest);
        Head = m_anim.GetBoneTransform(HumanBodyBones.Head);
        //    body.LookAt(targetBody.position);
        Head.LookAt(targetBody.position);
        //  Hand = m_anim.GetBoneTransform(HumanBodyBones.LeftHand);
    }

    public void GetBulletSystem ()
    {
        bullSystem = WeaponActive.GetComponent<BulletSystem>();
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
    private void OnTriggerStay (Collider other)
    {
        if (other.transform.CompareTag(Tags.AI))
        {
            if (WeaponActive)
            {

                Vector3 r = WeaponActive.position - other.transform.position;
                Quaternion target = Quaternion.LookRotation(r);
                WeaponActive.rotation = Quaternion.RotateTowards(WeaponActive.rotation, target, Time.deltaTime);
              // keep only the horizontal direction
            //    WeaponActive.LookAt(other.transform.GetChild(0));
            }
        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.transform.CompareTag(Tags.AI))
        {
            if (WeaponActive)
            {
                WeaponActive.LookAt(targetNull.position);
            }
        }
    }

}
