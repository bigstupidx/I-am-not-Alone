﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("настроки здоровья")]

    public float MaxHealth = 100.0f;
    public float CurHelth = 100.0f;

    [Header("Woods,Metals,Glasses,Electrics,Interactive")]
    public int MakeMaterial;
    public GameObject CraftItemStaticForWallCrash;
    public GameObject[] explosion;

    PoolingSystem poolsistem;
    SwitchMode buildMode;
    CraftItem _craftItem;
    CheckInWeaponAndCraft checkWeaponAndCraft;

    [Space(15)]
    [Header("For Ai")]
    public bool WeaponBox;
    public bool MaterialBox;
    public bool InterectiveBox;
    public bool OrRandom;
    public int  MoneyAi;
    private void Start ()
    {
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        checkWeaponAndCraft = GameObject.Find("WeaponController").GetComponent<CheckInWeaponAndCraft>();
        if (transform.parent != null)
        {
            _craftItem = transform.parent.GetComponent<CraftItem>();
        }

        poolsistem = PoolingSystem.Instance;
    }
    // Use this for initialization

    public void MySelfDestroyer ()
    {
        poolsistem.InstantiateAPS("SmallExplosionEffect", transform.position, Quaternion.identity);
        this.gameObject.DestroyAPS();
        _craftItem = GetComponent<CraftItem>();
        _craftItem.DefaultOptions();

    }

    public void HelthDamage (float damage)
    {


        CurHelth -= damage;
    
        if (CurHelth > MaxHealth)
        {
            CurHelth = MaxHealth;

        }
        if (CurHelth <= 0)
        {
            CurHelth = 0;

            if (transform.CompareTag("Player"))
            {
                Debug.Log("умер");
                //     shipguiController.Healthometer.fillAmount = CurHelth / MaxHealth;

            }
            if (transform.CompareTag("Things"))
            {

                poolsistem.InstantiateAPS("SmallExplosionEffect", transform.position, Quaternion.identity);

                //   checkWeaponAndCraft.CreateBoxItem(transform.position,MakeMaterial);
                checkWeaponAndCraft.CreateBoxInterActive(transform.position);
                Destroy(gameObject);

            }
            if (transform.CompareTag("CraftMode"))
            {

                _craftItem.DefaultOptions();


                poolsistem.InstantiateAPS("SmallExplosionEffect", transform.position, Quaternion.identity);
            }
            if (transform.CompareTag("AI"))
            {

                //_craftItem = GetComponent<CraftItem>();
                //_craftItem.DefaultOptions();
               poolsistem.InstantiateAPS("SmallExplosionEffect", transform.position, Quaternion.identity);
                //this.gameObject.DestroyAPS();
                //_craftItem._StartHisEffect = false;
                checkWeaponAndCraft.MyMoney.text = (int.Parse(checkWeaponAndCraft.MyMoney.text) + MoneyAi).ToString();
                int r = Random.Range(0, 2);
                if (r==1)
                {
                    if (OrRandom)
                    {
                        int i = Random.Range(0, 2);
                        int l = Random.Range(0, 5);
                 
                        MakeMaterial = l;
                        if (i == 0)
                        {
                            checkWeaponAndCraft.CreateBoxItem(transform.position, MakeMaterial);
                        }
                        else
                        {
                            checkWeaponAndCraft.CreateBoxWeapon(transform.position);
                        }
                    }
                    else
                    {
                        if (MaterialBox)
                        {
                            checkWeaponAndCraft.CreateBoxItem(transform.position, MakeMaterial);
                        }
                        if (WeaponBox)
                        {
                            checkWeaponAndCraft.CreateBoxWeapon(transform.position);
                        }
                    }
                    if (InterectiveBox)
                    {
                        checkWeaponAndCraft.CreateBoxInterActive(transform.position);
                    } 
                }
                // checkWeaponAndCraft.CreateBoxInterActive(transform.position);
                Destroy(gameObject);

            }
            if (transform.CompareTag("WallCrash"))
            {
                CraftItemStaticForWallCrash.SetActive(true);
                Destroy(gameObject);
         


            }
            if (transform.CompareTag("CraftFromMenu"))
            {

                _craftItem = GetComponent<CraftItem>();
                _craftItem.DefaultOptions();

                poolsistem.InstantiateAPS("SmallExplosionEffect", transform.position, Quaternion.identity);
                this.gameObject.DestroyAPS();
                _craftItem._StartHisEffect = false;



            }
            //else
            //{

            //    poolsistem.InstantiateAPS("SmallExplosionEffect", transform.position, Quaternion.identity);

            //    Destroy(gameObject);
            //}



        }
    }
}




