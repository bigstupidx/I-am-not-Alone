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
        anim.SetLayerWeight(1, 1);
        anim.SetTrigger("attack");
    }
    public void WaitingAxe ()
    {
        fight = false;
        anim.SetLayerWeight(1,0);
   
    }


}
