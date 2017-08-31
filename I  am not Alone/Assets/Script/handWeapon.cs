using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handWeapon : MonoBehaviour
{

    private Animator anim;
    public float intervalWeaponAmmunition = 0.5f;
    public float WeaponAmmunition = 1;
    WeaponController _weaponController;
    Transform AdvancedPoolingSystem;
    // Use this for initialization
    bool l;
    void Start ()
    {
        anim = GetComponent<Animator>();
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;

    }
    private void OnEnable ()
    {
        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        _weaponController.Ammunition(WeaponAmmunition);
    }
    // Update is called once per frame
    void Update ()
    {
        if (l)
        {
            WeaponAmmunition -= Time.deltaTime * intervalWeaponAmmunition;
            _weaponController.Ammunition(WeaponAmmunition);
            if (WeaponAmmunition <= 0)
            {
                l = false;
                if (transform.GetChild(0).childCount == 0) { return; }
                transform.GetChild(0).GetChild(0).gameObject.DestroyAPS();
                transform.GetChild(0).GetChild(0).transform.SetParent(AdvancedPoolingSystem);

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            l = false;
            anim.SetBool("attack", false);
        }
        if (Input.GetMouseButtonDown(0))

        {
            l = true;
            anim.SetBool("attack", true);



            //  Debug.Log("play");



        }
    }
}
