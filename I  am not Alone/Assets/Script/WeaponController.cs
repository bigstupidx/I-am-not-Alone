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
    private void OnEnable ()
    {
        pool = PoolingSystem.Instance;
        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;
    }
    // Use this for initialization
    void Start ()
    {
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

    public void PlayerWeapon (string nameWeapon,int category)
    {
        if (category ==0)
        {
            if (Hand.transform.GetChild(0).GetChild(0) == null)
            {
                AddWeapon(nameWeapon, Hand.transform.GetChild(0));

             
            }
            else
            {
                RemoveWeapon(Hand.transform.GetChild(0).GetChild(0));
                AddWeapon(nameWeapon, Hand.transform.GetChild(0));
              
            }

        }
        else
        {

            if(WeaponOne.transform.childCount == 0)
            {
                AddWeapon(nameWeapon, WeaponOne.transform);
                return;
            }
            else
            {
                if (WeaponOne.transform.GetChild(0).name.Equals(nameWeapon + "(Clone)"))
                {
                    Ammunition(1);
                    WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
                    return;
                }
             
            }
            if (WeaponTwo.transform.childCount == 0)
            {
                AddWeapon(nameWeapon, WeaponTwo.transform);
                return;
            }
            else
            {
                if (WeaponTwo.transform.GetChild(0).name.Equals(nameWeapon + "(Clone)"))
                {
                    Ammunition(1);
                    WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
                    return;
                }
            }
            if(WeaponTwo.transform.childCount != 0)
            {
                RemoveWeapon(WeaponTwo.transform.GetChild(0));
                AddWeapon(nameWeapon, WeaponTwo.transform);
            }

        }

    }

    public void AddWeapon (string name,Transform pos)
    {

        GameObject weapon = pool.InstantiateAPS(name, pos.position, pos.rotation);
        weapon.transform.SetParent(pos);
        if (weapon.GetComponent<BulletSystem>() != null)
        {
            weapon.GetComponent<BulletSystem>().WeaponAmmunition = 1;

        }
        else
        {
            weapon.transform.parent.parent.GetComponent<handWeapon>().WeaponAmmunition = 1;
        }
        Ammunition(1);

    }
    public void RemoveWeapon (Transform Weapon)
    {
        Weapon.gameObject.DestroyAPS();
        Weapon.transform.SetParent(AdvancedPoolingSystem);

    }


    public void Ammunition (float value)
    {
        image.fillAmount = value;


    }

}
