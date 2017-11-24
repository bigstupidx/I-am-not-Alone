using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SelectionWeaponForPC : MonoBehaviour
{

    public WeaponController weaponController;
    // Use this for initialization
    ThirdPersonUserControl usercontrol;
    public bool Fire1;
    Animator animator;

    private void Start ()
    {
        usercontrol = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<ThirdPersonUserControl>();
        animator = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown("1"))
        {
            weaponController.SelectionWeapon(0);

        }
        if (Input.GetKeyDown("2"))
        {
            weaponController.SelectionWeapon(1);

        }
        if (Input.GetKeyDown("3"))
        {
            weaponController.SelectionWeapon(2);

        }
    }
    public void WeaponHand ()
    {
        weaponController.SelectionWeapon(0);
    }
    public void Weapon1 ()
    {
        weaponController.SelectionWeapon(1);
    }
    public void Weapon2 ()
    {
        weaponController.SelectionWeapon(2);
    }
    public void Weapon3 ()
    {
        weaponController.SelectionWeapon(3);
    }

    public void WeaponPlayUp ()
    {
    
        Fire1 = false;
        animator.SetBool("attack", Fire1);
    }
    public void WeaponPlayDown ()
    {

        Fire1 = true;
        animator.SetBool("attack", Fire1);
    }
}
