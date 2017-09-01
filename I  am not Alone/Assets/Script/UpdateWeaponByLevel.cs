using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateWeaponByLevel  {


        public float intervalWeaponAmmunition;

        public int damage;

        public UpdateWeaponByLevel (float intervalAmuni, int _damage)
        {

            this.intervalWeaponAmmunition = intervalAmuni;
            this.damage = _damage;

        }
    
}
