﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionWeaponForPC : MonoBehaviour {

    public WeaponController weaponController;
    // Use this for initialization

 

    // Update is called once per frame
    void Update () {
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
}
