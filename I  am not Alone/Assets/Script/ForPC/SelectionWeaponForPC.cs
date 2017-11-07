using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SelectionWeaponForPC : MonoBehaviour
{

    public WeaponController weaponController;
    // Use this for initialization

    public bool Fire1;

    TouchPad touch;
    private void Start ()
    {
        touch = GameObject.Find("TurnAndLookTouchpad").GetComponent<TouchPad>();
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
        touch.Xsensitivity = 1f;
        touch.Ysensitivity = 1f;
        Fire1 = false;
    }
    public void WeaponPlayDown ()
    {
        touch.Xsensitivity = 0.2f;
        touch.Ysensitivity = 0.2f;
        Fire1 = true;
    }
}
