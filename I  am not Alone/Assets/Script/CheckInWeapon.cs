using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInWeapon : MonoBehaviour
{

    public Transform grid;

    public List<WeaponParams> WeaponBought = new List<WeaponParams>();

   // public List<int> WeaponLevel = new List<int>();
    DbGame db;
    void Start ()
    {
        db = GetComponent<DbGame>();
        db.OpenDB("DBGame.db");
        db.GetWeaponBought();
        CheckIn();
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