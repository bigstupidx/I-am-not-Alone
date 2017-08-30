using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSystem : MonoBehaviour
{


    // сколько  урона будет наносить
    public int bulletDamage;






    public bool checkWeapons = false;

    public ParticleSystem bullet;
    // последсвите от попадания
    public GameObject smoke;
    private ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
    public Material[] BulletPowerMaterial;
    private float reloadTimer;
    public float interval;
    void Start ()
    {

        bullet = GetComponent<ParticleSystem>();

        bullet.Stop();

        //  BulletVisualMaterial(bulletDamage);
    }

    private void Update ()
    {
        if (reloadTimer > 0)
        {

          //  bullet.Stop();
            reloadTimer -= Time.deltaTime;
        }
        BulettAttack();
    }

    void BulletVisualMaterial (int dmg)
    {
        if (dmg == 10)
        {
            bullet.GetComponent<ParticleSystemRenderer>().material = BulletPowerMaterial[0];
        }
        else if (dmg == 20)
        {
            bullet.GetComponent<ParticleSystemRenderer>().material = BulletPowerMaterial[1];
        }
        else if (dmg == 30)
        {
            bullet.GetComponent<ParticleSystemRenderer>().material = BulletPowerMaterial[1];
        }
        else if (dmg == 5)
        {
            bullet.GetComponent<ParticleSystemRenderer>().material = BulletPowerMaterial[1];
        }
        else
        {
            bullet.GetComponent<ParticleSystemRenderer>().material = BulletPowerMaterial[1];
        }

    }
    public void BulettAttack ()
    {
        //  BulletVisualMaterial(bulletDamage);
        if (Input.GetMouseButtonUp(0))
        {
            bullet.Stop();

        }
        if (Input.GetMouseButtonDown(0))

        {

            reloadTimer = interval;
            bullet.Play();

          //  Debug.Log("play");



        }




    }




    void OnParticleCollision (GameObject other)
    {

        //if (other.tag == "AIZombie")
        //{
        //    if (other.GetComponent<TheallXp>().CurArmor > 0)
        //    {


        //        if (other.transform.root.name != transform.root.name)
        //        {
        //            other.GetComponent<TheallXp>().Armor(bulletDamage);
        //            other.GetComponent<TheallXp>().resetArmorTimer = 5;
        //            other.GetComponent<TheallXp>().EnemyShip(transform.root);
        //        }

        //    }
        //    if (other.GetComponent<TheallXp>().CurArmor <= 0)
        //    {


        //        if (other.transform.root.name != transform.root.name)
        //        {
        //            other.GetComponent<TheallXp>().Helth(bulletDamage);
        //            other.GetComponent<TheallXp>().resetArmorTimer = 5;
        //            other.GetComponent<TheallXp>().EnemyShip(transform.root);
        //        }

        //    }




        //}


        //if (other.tag == "furniture")
        //{

        //    if (other.GetComponent<TheallXp>().CurArmor > 0)
        //    {



        //        if (other.transform.root.name != transform.root.name)
        //        {
        //            other.GetComponent<TheallXp>().Armor(bulletDamage);


        //        }

        //    }
        //    if (other.GetComponent<TheallXp>().CurArmor <= 0)
        //    {


        //        if (other.transform.root.name != transform.root.name)
        //        {
        //            other.GetComponent<TheallXp>().Helth(bulletDamage);


        //        }

        //    }





        //}

        int safeLength = GetComponent<ParticleSystem>().GetSafeCollisionEventSize();
        if (collisionEvents.Length < safeLength)
            collisionEvents = new ParticleCollisionEvent[safeLength];
        int numCollisionEvents = GetComponent<ParticleSystem>().GetCollisionEvents(other, collisionEvents);
        int i = 0;
        while (i < numCollisionEvents)
        {
            Vector3 collisionHitLoc = collisionEvents[i].intersection;

            Instantiate(smoke, collisionHitLoc, Quaternion.identity);
            i++;

        }
    }
}


