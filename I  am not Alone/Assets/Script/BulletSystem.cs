using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UpdateWeapon
{
    public float intervalWeaponAmmunition = 1;

    public float damage;

    public UpdateWeapon (float intervalAmuni, float _damage)
    {

        this.intervalWeaponAmmunition = intervalAmuni;
        this.damage = _damage;

    }
}



public class BulletSystem : MonoBehaviour
{





    public float intervalWeaponAmmunition = 0.5f;

    public ParticleSystem bullet;
    public MyParticleCollision particleCol;
    // последсвите от попадания



    public float WeaponAmmunition = 1;
    public int level;


    public List<UpdateWeapon> updateWeapon = new List<UpdateWeapon>();
    bool l;

    WeaponController _weaponController;
    SelectionWeaponForPC selectionWeaponPlay;
    Transform AdvancedPoolingSystem;
    float timer;
    float timeBetweenBullets = 0.15f;
    float effectsDisplayTime = 0.2f;
    Light gunLight;
    private void OnEnable ()
    {
        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        selectionWeaponPlay = GameObject.Find("WeaponController").GetComponent<SelectionWeaponForPC>();
        gunLight = GetComponent<Light>();
        UpdateWeapon();
        timer = 0;
        _weaponController.Ammunition(WeaponAmmunition);
        l = false;
    }

    private void Update ()
    {

        timer += Time.deltaTime;
        BulettAttack();
        if (l)
        {

            timer = Time.deltaTime;
            WeaponAmmunition -= timer * intervalWeaponAmmunition;

            _weaponController.Ammunition(WeaponAmmunition);
            if (WeaponAmmunition <= 0)
            {



                gameObject.DestroyAPS();
                transform.SetParent(AdvancedPoolingSystem);
            }
        }
    }


    public void BulettAttack ()
    {
        //  BulletVisualMaterial(bulletDamage);
        //if (!selectionWeaponPlay.Fire1)
        if (Input.GetMouseButtonUp(0))
        {
            bullet.Stop();
            l = false;
        }
        //if (selectionWeaponPlay.Fire1)
        if (Input.GetMouseButtonDown(0))
        {
            if (timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                gunLight.enabled = true;
                timer = 0f;
            }

            l = true;
            bullet.Play();

        }


        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();
        }

        //  Debug.Log("play");

    }

    public void DisableEffects ()
    {
        // Disable the line renderer and the light.

        gunLight.enabled = false;
    }
    void UpdateWeapon ()
    {
        intervalWeaponAmmunition = updateWeapon[level].intervalWeaponAmmunition;
        particleCol.bulletDamage = updateWeapon[level].damage;
    }






    //void OnParticleCollision (GameObject other)
    //{

    //    Debug.Log(other.name);
    //    if (other.tag == "CraftMode")
    //    {




    //        if (other.transform.root.name != transform.name)
    //        {
    //            other.GetComponent<Health>().Helth(bulletDamage);

    //        }






    //    }


    //    if (other.tag == "Things")
    //    {



    //        if (other.transform.root.name != transform.name)
    //        {
    //            other.GetComponent<Health>().Helth(bulletDamage);


    //        }







    //    }

    //    int safeLength = GetComponent<ParticleSystem>().GetSafeCollisionEventSize();
    //    if (collisionEvents.Length < safeLength)
    //        collisionEvents = new ParticleCollisionEvent[safeLength];
    //    int numCollisionEvents = GetComponent<ParticleSystem>().GetCollisionEvents(other, collisionEvents);
    //    int i = 0;
    //    while (i < numCollisionEvents)
    //    {
    //        Vector3 collisionHitLoc = collisionEvents[i].intersection;

    //        //  Instantiate(smoke, collisionHitLoc, Quaternion.identity);
    //        i++;

    //    }

    //}
}






