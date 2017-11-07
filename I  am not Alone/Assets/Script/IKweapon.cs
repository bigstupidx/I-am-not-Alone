using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKweapon : MonoBehaviour
{
    Animator m_anim;
    public Transform handLeft;
    public Transform handRight;
    // Use this for initialization
    void Start ()
    {
        m_anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK (int layerIndex)
    {
        if (handLeft)
        {
            m_anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            m_anim.SetIKPosition(AvatarIKGoal.LeftHand, handLeft.position);
            m_anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            m_anim.SetIKPosition(AvatarIKGoal.RightHand, handRight.position);
        }
    }
}
