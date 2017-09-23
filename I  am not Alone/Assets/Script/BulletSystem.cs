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
    Transform AdvancedPoolingSystem;
    float timer;

    private void OnEnable ()
    {
        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        UpdateWeapon();
        timer = 0;
        _weaponController.Ammunition(WeaponAmmunition);
        l = false;
    }
    private void Update ()
    {


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
        if (Input.GetMouseButtonUp(0))
        {
            bullet.Stop();
            l = false;
        }
        if (Input.GetMouseButtonDown(0))
        {

            l = true;
            bullet.Play();

        }



        //  Debug.Log("play");

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






