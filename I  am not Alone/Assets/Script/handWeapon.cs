using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UpdateHandWeapon
{
    public float intervalWeaponAmmunition;

    public int damage;

    public UpdateHandWeapon (float intervalAmuni, int _damage)
    {

        this.intervalWeaponAmmunition = intervalAmuni;
        this.damage = _damage;

    }
}
public class handWeapon : MonoBehaviour
{

    private Animator anim;
    public float intervalWeaponAmmunition = 0.5f;
    public float WeaponAmmunition = 1;
    public int level;
    public List<UpdateWeapon> updateWeapon = new List<UpdateWeapon>();
    WeaponController _weaponController;
    Transform AdvancedPoolingSystem;
    // Use this for initialization
    public bool l;

    private void OnEnable ()
    {
        UpdateWeapon();
        anim = GetComponent<Animator>();
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

    public void UpdateWeapon ()
    {
        intervalWeaponAmmunition = updateWeapon[level].intervalWeaponAmmunition;

    }
}
