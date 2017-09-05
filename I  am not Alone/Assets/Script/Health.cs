using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("настроки здоровья")]

    public float MaxHealth = 100.0f;
    public float CurHelth = 100.0f;

    public GameObject[] explosion;

    PoolingSystem poolsistem;
    SwitchMode buildMode;
    CraftItem _craftItem;

    private void Start ()
    {
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
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

        CurHelth = 100;
    }

    public void HelthDamage (int damage)
    {


        CurHelth -= damage;
        if (transform.CompareTag("Player"))
        {

            //     shipguiController.Healthometer.fillAmount = CurHelth / MaxHealth;

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

                //     shipguiController.Healthometer.fillAmount = CurHelth / MaxHealth;

            }

            if (transform.CompareTag("CraftMode"))
            {

                _craftItem.DefaultOptions();

                CurHelth = 100;
                poolsistem.InstantiateAPS("SmallExplosionEffect", transform.position, Quaternion.identity);
            }
            if (transform.CompareTag("AI"))
            {

                _craftItem = GetComponent<CraftItem>();
                _craftItem.DefaultOptions();
                poolsistem.InstantiateAPS("SmallExplosionEffect", transform.position, Quaternion.identity);
                this.gameObject.DestroyAPS();
                _craftItem._StartHisEffect = false;
                CurHelth = 100;


            }


            else
            {

                poolsistem.InstantiateAPS("SmallExplosionEffect", transform.position, Quaternion.identity);

                Destroy(gameObject);
            }



        }
    }
}




