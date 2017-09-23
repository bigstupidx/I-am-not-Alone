using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponController : MonoBehaviour
{

    public GameObject Hand;

    public Image image;



    public GameObject WeaponOne;
    public GameObject WeaponTwo;
    PoolingSystem pool;
    Transform AdvancedPoolingSystem;
    CheckInWeaponAndCraft _checkInWeaponCraft;
    GameObject player;
    private void OnEnable ()
    {
        pool = PoolingSystem.Instance;
        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;
    }
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _checkInWeaponCraft = GetComponent<CheckInWeaponAndCraft>();
        pool = PoolingSystem.Instance;
        SelectionWeapon(0);
        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;
    }


    public void SelectionWeapon (int IdWeapon)
    {

        switch (IdWeapon)
        {
            case 0:
                Hand.SetActive(true);
                WeaponOne.SetActive(false);
                WeaponTwo.SetActive(false);
                break;
            case 1:

                Hand.SetActive(false);
                WeaponOne.SetActive(true);
                WeaponTwo.SetActive(false);
                break;
            case 2:

                Hand.SetActive(false);
                WeaponOne.SetActive(false);
                WeaponTwo.SetActive(true);
                break;
            default:
                break;
        }

    }

    public void PlayerWeapon (string nameWeapon, int category, int level , float amunition)
    {
        if (category == 0)
        {
            if (Hand.transform.GetChild(0).childCount == 0)
            {
                AddWeapon(nameWeapon, Hand.transform.GetChild(0), level, category, amunition);

                return;
            }
            else
            {
                if (Hand.transform.GetChild(0).GetChild(0).name.Equals(nameWeapon + "(Clone)"))
                {

                    if (Hand.GetComponent<handWeapon>().WeaponAmmunition == 1)
                    {
                         RemoveWeapon(Hand.transform.GetChild(0).GetChild(0), nameWeapon, category);
                        AddWeapon(nameWeapon, Hand.transform.GetChild(0), level, category, amunition);
                   
                    }
                    else
                    {
                        Hand.GetComponent<handWeapon>().WeaponAmmunition = 1;
                    
                    }

                    return;
                }
                else
                {
                    RemoveWeapon(Hand.transform.GetChild(0).GetChild(0), nameWeapon, category);
                    AddWeapon(nameWeapon, Hand.transform.GetChild(0), level, category, amunition);
                    return;
                }


            }

        }
        else
        {
            if (WeaponOne.transform.childCount == 0)
            {
                AddWeapon(nameWeapon, WeaponOne.transform, level, category,amunition);


                return;
            }
            else
            {
                if (WeaponOne.transform.GetChild(0).name.Equals(nameWeapon + "(Clone)"))
                {

                  
                    if (WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition == 1)
                    {
                        RemoveWeapon(WeaponOne.transform.GetChild(0), nameWeapon, category);
                        AddWeapon(nameWeapon, WeaponOne.transform, level, category, amunition);
                    }
                    else
                    {
                        WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
                    }
                    return;
                }
            }
            if (WeaponTwo.transform.childCount == 0)
            {
                AddWeapon(nameWeapon, WeaponTwo.transform, level, category,amunition);

                return;
            }
            else
            {
                if (WeaponTwo.transform.GetChild(0).name.Equals(nameWeapon + "(Clone)"))
                {


                    if (WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition == 1)
                    {
                        RemoveWeapon(WeaponTwo.transform.GetChild(0), nameWeapon, category);
                        AddWeapon(nameWeapon, WeaponTwo.transform, level, category, amunition);
                    }
                    else
                    {
                        WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
                    }
                    return;
                }
                else
                {
                    RemoveWeapon(WeaponTwo.transform.GetChild(0), nameWeapon,category);

                    AddWeapon(nameWeapon, WeaponTwo.transform, level, category,amunition);
                    return;

                }




            }







        }

    }

    public void AddWeapon (string name, Transform pos, int level, int category,float amuni)
    {

        GameObject weapon = pool.InstantiateAPS(name, pos.position, pos.rotation, pos.gameObject);

        if (category == 1)
        {
            weapon.GetComponent<BulletSystem>().level = level;
            weapon.GetComponent<BulletSystem>().WeaponAmmunition = amuni;
        }
        else
        {
            Hand.GetComponent<handWeapon>().level = level;
            Hand.GetComponent<handWeapon>().WeaponAmmunition = amuni;
        }


    }
    public void RemoveWeapon (Transform Weapon, string nameWe,int category)
    {
        if(category == 0)
        {
            _checkInWeaponCraft.OldWeapon(nameWe,null , Hand.GetComponent<handWeapon>(), player.transform.position + new Vector3(4,0,4));
        }
        else
        {
            _checkInWeaponCraft.OldWeapon(nameWe, Weapon.GetComponent<BulletSystem>(),null, player.transform.position);
        }

    
        Weapon.gameObject.DestroyAPS();
        Weapon.transform.SetParent(AdvancedPoolingSystem);

    }


    public void Ammunition (float value)
    {

        image.fillAmount = value;


    }

}
