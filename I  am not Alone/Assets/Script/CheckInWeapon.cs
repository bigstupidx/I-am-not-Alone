using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInWeapon : MonoBehaviour
{
    public bool ShopOrNot;
    public Transform grid;

    public List<WeaponParams> WeaponBought = new List<WeaponParams>();
    PoolingSystem pool;

    DbGame db;
    void Start ()
    {
        db = GetComponent<DbGame>();
        db.OpenDB("DBGame.db");
        db.GetWeaponBought();
        if (ShopOrNot)
        {
            CheckIn(); 
        }
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

            int i = Random.Range(0, WeaponBought.Count);
            GameObject box = pool.InstantiateAPS("BoxWithWeapon", new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity);
            Debug.Log(WeaponBought[i].nameWeapon + " " + WeaponBought[i].category);
            box.transform.GetChild(0).GetComponent<BoxWeapon>().categoryWeapon = WeaponBought[i].category;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().nameWeapon = WeaponBought[i].nameWeapon;
        }
    }
    public void CheckIn ()
    {

        for (int i = 0; i < WeaponBought.Count; i++)
        {
            if (WeaponBought[i] != null)
            {

                grid.Find(WeaponBought[i].nameWeapon).GetComponent<ItemParams>().levelItem = WeaponBought[i].levelWeapon;
                grid.Find(WeaponBought[i].nameWeapon).GetComponent<ItemParams>().IntializedParams();
            }
        }

    }


}
public class WeaponParams
{
    public int category;
    public string nameWeapon;
    public int levelWeapon;


    public WeaponParams (string nameweapon, int level, int category)
    {
        this.category = category;
        this.nameWeapon = nameweapon;
        this.levelWeapon = level;

    }
}