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



    [Range(0, 3)]
    public int WeightWeapon;
    public float intervalWeaponAmmunition = 0.5f;
    public AudioClip weaponSound;
    public ParticleSystem bullet;
    public ParticleSystem gunMiscle;
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
    float timerShoot;
    public float timeBetweenBullets = 0.05f;
    public float effectsDisplayTime = 0.2f;
    public Light gunLight;
    Light SpotlightFace;
    AudioSource gunAudio;
    public bool fireGun;
    public Transform buttonWeapon;
    private void OnEnable ()
    {
        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        selectionWeaponPlay = GameObject.Find("WeaponController").GetComponent<SelectionWeaponForPC>();

        gunLight = GetComponent<Light>();

        gunAudio = GetComponent<AudioSource>();
        UpdateWeapon();
        timer = 0;
        _weaponController.Ammunition(WeaponAmmunition);
        l = false;

        SpotlightFace = GameObject.FindGameObjectWithTag("Player").transform.Find("SpotlightFace").GetComponent<Light>();

    }

    private void Update ()
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

                _weaponController.ResetWeapon();
                Destroy(buttonWeapon.gameObject);

                gameObject.DestroyAPS();
                transform.SetParent(AdvancedPoolingSystem);
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
		  
	}else{
         if (selectionWeaponPlay.Fire1) 
        }




                if (fireGun)
        {

             if (selectionWeaponPlay.Fire1) 
            {
                gunLight.enabled = true;
                SpotlightFace.enabled = true;


                gunMiscle.Stop();
                gunMiscle.Play();
           
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
                SpotlightFace.enabled = true;


                gunMiscle.Stop();
                gunMiscle.Play();
               
                l = true;
                bullet.Play();
                timerShoot = 0f;
            } 
	}

        }

#else
        if (fireGun)
        {

            if (Input.GetButton("Fire1"))
            {
                gunLight.enabled = true;
                SpotlightFace.enabled = true;


                gunMiscle.Stop();
                gunMiscle.Play();

                l = true;
                bullet.Play();
                timerShoot = 0f;
            }
        }
        else
        {
            if (Input.GetButton("Fire1") & timerShoot >= timeBetweenBullets && Time.timeScale != 0)
            {
                gunLight.enabled = true;
                SpotlightFace.enabled = true;

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






