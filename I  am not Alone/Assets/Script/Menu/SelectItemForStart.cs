using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemForStart : MonoBehaviour
{
    CheckInWeaponAndCraft checkWeapon;
    Toggle tog;
    public string nameItem;
    Health health;
    SwitchMode buildControll;
    // Use this for initialization
    void Start ()
    {
        health = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Health>();
        checkWeapon = GameObject.Find("WeaponController").GetComponent<CheckInWeaponAndCraft>();
        tog = GetComponent<Toggle>();
        buildControll = GameObject.Find("BuildController").GetComponent<SwitchMode>();
    }

    public void UpHealth (Toggle h)
    {
        if (h.isOn)
        {
            health.CurHelth = 150;
            health.MaxHealth = 150;
            for (int i = 0; i < buildControll.panelGoods.Count; i++)
            {
                buildControll.panelGoods[i].text = "0";
            }
        }
        else
        {
            health.CurHelth = 100;
            health.MaxHealth = 100;

        }
    }




    public void SelectItemCraft (Toggle g)
    {
        if (g.isOn)
        {
            if (checkWeapon.addItemCraft.Count < 5)
            {
                checkWeapon.addItemCraft.Add(nameItem);
            }
            else
            {
                tog.isOn = false;
            }

        }
        else
        {
            checkWeapon.addItemCraft.Remove(nameItem);
        }

    }
    public void SelectWeapon (Toggle g)
    {
        if (g.isOn)
        {
            if (checkWeapon.addItemCraftWeapon.Count < 3)
            {
                checkWeapon.addItemCraftWeapon.Add(nameItem);
            }
            else
            {
                tog.isOn = false;
            }
        }
        else
        {
            checkWeapon.addItemCraftWeapon.Remove(nameItem);
        }
    }

}
