using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    [Header("настроки здоровья")]

    public float MaxHealth = 100.0f;
    public float CurHelth = 100.0f;
    [Space(15)]
    [Header("For Craft")]
    [Header("Woods,Metals,Glasses,Electrics,Interactive")]
    public int MakeMaterial;
    public GameObject CraftItemStaticForWallCrash;


    [Space(15)]
    [Header("For Player")]
    public Image HealthPlayer;

    [Space(15)]
    [Header("For Ai")]
    public bool WeaponBox;
    public bool MaterialBox;
    public bool InterectiveBox;
    public bool OrRandom;
    public int  MoneyAi;


    PoolingSystem poolsistem;
    SwitchMode buildMode;
    CraftItem _craftItem;
    CheckInWeaponAndCraft checkWeaponAndCraft;
    private void Start ()
    {
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        checkWeaponAndCraft = GameObject.Find("WeaponController").GetComponent<CheckInWeaponAndCraft>();
        if (transform.parent != null)
        {
            _craftItem = transform.parent.GetComponent<CraftItem>();
        }

        poolsistem = PoolingSystem.Instance;
        if (transform.CompareTag("Player"))
        {
            HealthPlayer.fillAmount = CurHelth/MaxHealth;

        }
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
  
        if (transform.CompareTag("Player"))
        {
            HealthPlayer.fillAmount = CurHelth / MaxHealth;

        }
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
                        int i = Random.Range(0, 5);
                        int l = Random.Range(0, 5);
                 
                        MakeMaterial = l;
                        if (i == 0  || i==1)
                        {
                            checkWeaponAndCraft.CreateBoxItem(transform.position, MakeMaterial);
                        }
                        else if(i == 2)
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




