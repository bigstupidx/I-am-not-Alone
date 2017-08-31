using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoxWithWeapon : MonoBehaviour
{

    PoolingSystem pool;
    string nameWeapon;
    int categoryWeapon;
    public List<WeaponParams> WeaponLoad = new List<WeaponParams>();

    DbGame db;
    // Use this for initialization
    void Start ()
    {
        db = GetComponent<DbGame>();
        pool = PoolingSystem.Instance;
    }

    private void OnEnable ()
    {
        pool = PoolingSystem.Instance;
    }
    private void Update ()
    {
        if (Input.GetButtonDown("Jump"))
        {
           // RandomCreateWeapon();
            GameObject box = pool.InstantiateAPS("BoxWithWeapon", new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity);
            box.transform.GetChild(0).GetComponent<BoxWeapon>().categoryWeapon = categoryWeapon;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().nameWeapon = nameWeapon;
        }
    }
    //void RandomCreateWeapon ()
    //{
    //    int i = Random.Range(0, 2);
    //    categoryWeapon = i;
    //    if (categoryWeapon == 0)
    //    {
    //        int l = Random.Range(0, MeleeWeapon.Length);
    //        nameWeapon = MeleeWeapon[l].name.ToString();
    //    }
    //    else
    //    {
    //        int l = Random.Range(0, BulletWeapon.Length);
    //        nameWeapon = BulletWeapon[l].name.ToString();
    //    }
    //}

}
