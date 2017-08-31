using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWeapon : MonoBehaviour
{

    PoolingSystem pool;
    WeaponController _weaponController;
    public string nameWeapon;
    public int categoryWeapon;
    // Use this for initialization
    void Start ()
    {

        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        pool = PoolingSystem.Instance;
    }

    private void OnEnable ()
    {
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        pool = PoolingSystem.Instance;
    }
    private void OnTriggerEnter (Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
         

            _weaponController.PlayerWeapon(nameWeapon, categoryWeapon);
            transform.parent.gameObject.DestroyAPS();


        }
    }


}
