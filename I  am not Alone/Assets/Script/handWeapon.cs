using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class handWeapon : MonoBehaviour
{
    public bool fight;
    public Animator anim;

    public void AttackAxe ()
    {
        fight = true;
        anim.SetBool("handAttack", true);
    }
    public void WaitingAxe ()
    {
        fight = false;
      anim.SetBool("handAttack", false);
    }


}
