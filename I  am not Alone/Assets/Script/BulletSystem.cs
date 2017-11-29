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
    public Vector3 WeaponPOsition;
    WeaponController _weaponController;
    SelectionWeaponForPC selectionWeaponPlay;
    Transform AdvancedPoolingSystem;
    float timer;
    float timerShoot;
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
        transform.LookAt(_weaponController.targetWeapon);


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


                    DisableEffects();
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

#if UNITY_ANDROID
        if (!selectionWeaponPlay.Fire1)
#else
        if (Input.GetMouseButtonUp(0))
#endif


        {
            bullet.Stop();
            l = false;
        }
#if UNITY_ANDROID
        if (fireGun)
        {
            if (selectionWeaponPlay.Fire1)
            {
                gunLight.enabled = true;

                selectionWeaponPlay.Fire1 = true;

                gunMiscle.Stop();
                gunMiscle.Play();
                if (!gunAudio.isPlaying)
                {
                    gunAudio.Play();
                }
                l = true;
                bullet.Play();
                timerShoot = 0f;
            }

        }
        else
        {
            if (selectionWeaponPlay.Fire1)




                if (fireGun)
                {

                    if (selectionWeaponPlay.Fire1)
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
                        timerShoot = 0f;
                    }
                }
                else
                {
                    if (selectionWeaponPlay.Fire1)
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
        }

#else
        {
            if (Input.GetButtonUp("Fire1"))
            {
                selectionWeaponPlay.Fire1 = false;
            }
            if (fireGun)
            {

                if (Input.GetButton("Fire1"))
                {
                    gunLight.enabled = true;

                    selectionWeaponPlay.Fire1 = true;

                    gunMiscle.Stop();
                    gunMiscle.Play();
                    if (!gunAudio.isPlaying)
                    {
                        gunAudio.Play();
                    }
                    l = true;
                    bullet.Play();
                    timerShoot = 0f;
                }
            }
            else

            if (Input.GetButton("Fire1") & timerShoot >= timeBetweenBullets && Time.timeScale != 0)
            {
                gunLight.enabled = true;

                selectionWeaponPlay.Fire1 = true;
                gunAudio.Play();
                gunMiscle.Stop();
                gunMiscle.Play();

                l = true;
                bullet.Play();


                timerShoot = 0f;
            }


        }
#endif







        //   }


        if (timerShoot >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();

        }


    }

    public void DisableEffects ()
    {
        // Disable the line renderer and the light.

        gunLight.enabled = false;
        //    SpotlightFace.enabled = false;
    }
    void UpdateWeapon ()
    {
        intervalWeaponAmmunition = updateWeapon[level].intervalWeaponAmmunition;
        particleCol.bulletDamage = updateWeapon[level].damage;
    }


}






