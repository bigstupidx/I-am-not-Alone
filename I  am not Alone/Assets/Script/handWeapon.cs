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
                Destroy(buttonWeapon);
            }
        }

#if UNITY_ANDROID
        if (!selectionWeaponPlay.Fire1)
#else
        if (Input.GetMouseButtonUp(0))
#endif
        {
            l = false;
            anim.SetBool("handAttack", false);
        }
#if UNITY_ANDROID
        if (selectionWeaponPlay.Fire1)
#else
        if (Input.GetMouseButtonDown(0))
#endif

        {
            if (transform.GetChild(0).childCount != 0)
            {
                l = true;
                anim.SetBool("handAttack", true); 
            }



            //  Debug.Log("play");



        }
    }

    public void UpdateWeapon ()
    {
        intervalWeaponAmmunition = updateWeapon[level].intervalWeaponAmmunition;

    }
}
