using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.ThirdPerson;

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


    Ray rayUp;
    Ray rayForward;
    Ray RayDown;
    RaycastHit hitUp;
    RaycastHit hitForward;
    RaycastHit hitDown;
    public float intervalWeaponAmmunition = 0.5f;
    public AudioClip weaponSound;
    public ParticleSystem bullet;
    public ParticleSystem gunMiscle;
    public MyParticleCollision particleCol;
    // последсвите от попадания


    public Transform leftHand;
    public Transform rightHand;

    public float WeaponAmmunition = 1;
    public int level;


    public List<UpdateWeapon> updateWeapon = new List<UpdateWeapon>();
    bool l;
    [Space(5)]

    public Vector3 WeaponPOsition;

    [Space(5)]
    WeaponController _weaponController;
    SelectionWeaponForPC selectionWeaponPlay;
    Transform AdvancedPoolingSystem;
    float timer;
    float timerShoot;
    float timerStartShoot;
    public float timeBetweenBullets = 0.05f;
    public float effectsDisplayTime = 0.2f;
    public Light gunLight;
    ThirdPersonUserControl usercontrol;
    AudioSource gunAudio;
    public bool fireGun;
    public Transform buttonWeapon;
    public bool resolution = true;

    private void OnEnable ()
    {
        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        selectionWeaponPlay = GameObject.Find("WeaponController").GetComponent<SelectionWeaponForPC>();

        gunLight = GetComponent<Light>();
        usercontrol = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<ThirdPersonUserControl>();
        gunAudio = GetComponent<AudioSource>();
        UpdateWeapon();
        timer = 0;
        _weaponController.Ammunition(WeaponAmmunition);
        l = false;


    }

    public void UpdateAmunition ()
    {
        _weaponController.Ammunition(WeaponAmmunition);
    }

    private void Update ()
    {

        if (resolution)
        {
            timer += Time.deltaTime;
            timerShoot += Time.deltaTime;
            BulettAttack();
            if (l)
            {

                timer = Time.deltaTime;
                WeaponAmmunition -= timer * intervalWeaponAmmunition;

                _weaponController.Ammunition(WeaponAmmunition);
                if (WeaponAmmunition <= 0)
                {



                    resolution = false;
                    gunLight.enabled = false;



                    gunMiscle.Stop();
                    gunMiscle.Stop();

                    gunAudio.Stop();

                    l = false;
                    bullet.Stop();
                    timerShoot = 0f;


                }
            }
        }
    }


    public void BulettAttack ()
    {


        if (!selectionWeaponPlay.Fire1)


        {
            timerStartShoot = 0;
            bullet.Stop();

            l = false;
        }


        if (selectionWeaponPlay.Fire1)




            if (fireGun)
            {




                gunLight.enabled = true;



                gunMiscle.Stop();
                gunMiscle.Play();
                if (!gunAudio.isPlaying)
                {
                    gunAudio.Play();
                }
                l = true;
                bullet.Play();



            }
            else
            {


                timerStartShoot += Time.deltaTime;
                if (timerStartShoot > 0.8f)
                {
                    if (Input.GetButton("Fire1") & timerShoot >= timeBetweenBullets && Time.timeScale != 0)
                    {
                        gunLight.enabled = true;


                        gunAudio.Play();
                        gunMiscle.Stop();
                        gunMiscle.Play();

                        l = true;
                        bullet.Play();
                        timerShoot = 0f;
                    }
                }


            }












        if (timerShoot >= timeBetweenBullets * effectsDisplayTime)
        {

            gunLight.enabled = false;
        }


    }


    void UpdateWeapon ()
    {
        intervalWeaponAmmunition = updateWeapon[level].intervalWeaponAmmunition;
        particleCol.bulletDamage = updateWeapon[level].damage;
    }


}






