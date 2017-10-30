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

    public Animator anim;
    public float intervalWeaponAmmunition = 0.5f;
    public float WeaponAmmunition = 1;
    public int level;
    public List<UpdateWeapon> updateWeapon = new List<UpdateWeapon>();
    WeaponController _weaponController;
    Transform AdvancedPoolingSystem;
    SelectionWeaponForPC selectionWeaponPlay;
    public GameObject buttonWeapon;
    // Use this for initialization
    public bool l;

    private void OnEnable ()
    {
        UpdateWeapon();

        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        selectionWeaponPlay = GameObject.Find("WeaponController").GetComponent<SelectionWeaponForPC>();
        _weaponController.Ammunition(WeaponAmmunition);
    }


    public void AttackAxe ()
    {
        anim.SetBool("handAttack", true);
    }
    public void WaitingAxe ()
    {
        anim.SetBool("handAttack", false);
    }

    public void UpdateWeapon ()
    {
        intervalWeaponAmmunition = updateWeapon[level].intervalWeaponAmmunition;

    }
}
