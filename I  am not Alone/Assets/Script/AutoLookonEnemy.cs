using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLookonEnemy : MonoBehaviour
{

    public IKtarget iktarget;
    public Transform weapon1;
    public Transform weapon2;
    public Transform weapon3;


    public Transform TargetAi;

    // Use this for initialization


    private void Update ()
    {


        if (TargetAi)
        {
            WeaponLook(weapon1, TargetAi);
            WeaponLook(weapon2, TargetAi);
            WeaponLook(weapon3, TargetAi);
            iktarget.target = TargetAi;
        }
        else
        {
            WeaponLook(weapon1, iktarget.targetBody);
            WeaponLook(weapon2, iktarget.targetBody);
            WeaponLook(weapon3, iktarget.targetBody);
            iktarget.target = null;
        }


    }
    private void OnTriggerStay (Collider other)
    {


        if (other.CompareTag("AI"))
        {
            TargetAi = other.transform.GetChild(0);

            WeaponLook(weapon1, TargetAi);
            WeaponLook(weapon2, TargetAi);
            WeaponLook(weapon3, TargetAi);

        }
    }
    private void OnTriggerExit (Collider other)
    {

        if (other.CompareTag("AI"))
        {

            TargetAi = null;

            iktarget.target = TargetAi;

            WeaponLook(weapon1, iktarget.targetBody);
            WeaponLook(weapon2, iktarget.targetBody);
            WeaponLook(weapon3, iktarget.targetBody);
        }
    }


    void WeaponLook (Transform weapon, Transform target)
    {
        if (target == null)
        {

            return;
        }
        if (weapon.childCount == 0)
        {

            return;
        }
        else
        {
            weapon.LookAt(target);
        }

    }
    public void WeaponNull ()
    {
        weapon1.LookAt(iktarget.target);
        weapon2.LookAt(iktarget.target);
        weapon3.LookAt(iktarget.target);
    }
}
