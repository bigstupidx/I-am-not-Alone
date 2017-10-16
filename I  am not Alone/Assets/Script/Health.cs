using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class Health : MonoBehaviour
{
    [Header("настроки здоровья")]

    public float MaxHealth = 100.0f;
    public float CurHelth = 100.0f;
    public bool CollisionDestroy = false;
    [Space(15)]
    [Header("For Craft")]
    [Header("Woods,Metals,Glasses,Electrics,Interactive")]
    public bool nothing;
    public int MakeMaterial;
    public GameObject CraftItemStaticForWallCrash;


    [Space(15)]
    [Header("For Player")]
    public Image HealthPlayer;
    public ParticleSystem blood;
    public GameObject imageGameOver;
    [Space(15)]
    [Header("For Ai")]
    public bool WeaponBox;
    public bool MaterialBox;
    public bool InterectiveBox;
    public bool OrRandom;
    public int MoneyAi;


    PoolingSystem poolsistem;
    SwitchMode buildMode;
    CraftItem _craftItem;
    CheckInWeaponAndCraft checkWeaponAndCraft;
    WeaponController weaponsControll;
    private GameObject destroyAi;
    AudioSource sourceDestraction;
    AudioSource staticAudio;
    Transform player;
    bool SoundTrue;
    float timer;
    private Rigidbody rigid;
    private void Start ()
    {
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        checkWeaponAndCraft = GameObject.Find("WeaponController").GetComponent<CheckInWeaponAndCraft>();
        weaponsControll = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        staticAudio = GameObject.Find("StaticAudio").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody>();
        sourceDestraction = GetComponent<AudioSource>();
        if (!sourceDestraction)
        {
            sourceDestraction = transform.parent.GetComponent<AudioSource>();
        }
        if (transform.parent != null)
        {
            _craftItem = transform.parent.GetComponent<CraftItem>();
        }

        poolsistem = PoolingSystem.Instance;
        if (transform.CompareTag("Player"))
        {
            HealthPlayer.fillAmount = CurHelth / MaxHealth;

        }

    }


    private void Update ()
    {
        if (SoundTrue)
        {
            timer += Time.deltaTime;
            if (timer >= sourceDestraction.clip.length)
            {
                if (_craftItem)
                {
                    _craftItem.DefaultOptions();
                }
                else
                {
                    _craftItem = GetComponent<CraftItem>();
                    _craftItem.DefaultOptions();
                }




                this.gameObject.DestroyAPS();
                _craftItem._StartHisEffect = false;

                SoundTrue = false;
            }
        }
    }
    // Use this for initialization

    public void MySelfDestroyer ()
    {
        sourceDestraction.Play();
        destroyAi = poolsistem.InstantiateAPS("SmallExplosionEffectForZombie", transform.position, Quaternion.identity);
        SoundTrue = true;
        timer = 0;
        _craftItem = GetComponent<CraftItem>();
        _craftItem.RenderOff();
        destroyAi.PlayEffect(30);

    }

    public void HelthDamage (float damage, bool player)
    {


        CurHelth -= damage;

        if (transform.CompareTag("Player"))
        {
            HealthPlayer.fillAmount = CurHelth / MaxHealth;
            blood.Play();
        }
        if (transform.CompareTag("AI"))
        {
            blood.Play();
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
                imageGameOver.SetActive(true);

                Time.timeScale = 0;

            }
            if (transform.CompareTag("Things"))
            {

                destroyAi = poolsistem.InstantiateAPS("DestroyObject", transform.position, Quaternion.identity);
              
                ParticleSystem ps = destroyAi.GetComponent<ParticleSystem>();
                var sh = ps.shape;
                sh.shapeType = ParticleSystemShapeType.MeshRenderer;
              
                if (transform.childCount != 0)
                {
                    if (transform.GetChild(0).GetComponent<MeshRenderer>())
                    {
                        sh.meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();

                    }
                }
                else
                {
                    sh.meshRenderer = transform.GetComponent<MeshRenderer>();
                }
                //   checkWeaponAndCraft.CreateBoxItem(transform.position,MakeMaterial);
                sourceDestraction.Play();

                if (!nothing)
                {
                    checkWeaponAndCraft.CreateBoxInterActive(transform.position);
                }
                EnebledPhysics();
                Destroy(gameObject, sourceDestraction.clip.length);

            }
            if (transform.CompareTag("CraftMode"))
            {
                _craftItem = GetComponent<CraftItem>();
                if (!_craftItem)
                {
                    _craftItem = transform.parent.GetComponent<CraftItem>();

                }
                if (_craftItem.BuildStatic)
                {
                    _craftItem.DefaultOptions();
                }
                else
                {
                    _craftItem.RenderOff();
                    timer = 0;
                    SoundTrue = true;
                }

                sourceDestraction.Play();



                destroyAi = poolsistem.InstantiateAPS("SmallExplosionEffectForZombie", transform.position, Quaternion.identity);
            }
            if (transform.CompareTag("AI"))
            {

                ZombieLevel1 zombie = GetComponent<ZombieLevel1>();

                EnebledPhysics();
                destroyAi = poolsistem.InstantiateAPS("SmallExplosionEffectForZombie", transform.position, Quaternion.identity);

                if (player)
                {
                    checkWeaponAndCraft.MyMoney.text = (int.Parse(checkWeaponAndCraft.MyMoney.text) + MoneyAi).ToString();
                }

                int r = Random.Range(0, 2);
                if (r == 1)
                {
                    if (OrRandom)
                    {
                        int i = Random.Range(0, 10);
                        int l = Random.Range(0, 4);

                        MakeMaterial = l;
                        if (i == 0 || i == 1)
                        {
                            checkWeaponAndCraft.CreateBoxItem(transform.position, MakeMaterial);
                        }
                        else if (i >= 2 && i <= 4)
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

                sourceDestraction.clip = zombie.zombieDeth;

                weaponsControll.WeaponOne.GetComponent<AutoLookonEnemy>().TargetAi = null;
                weaponsControll.WeaponTwo.GetComponent<AutoLookonEnemy>().TargetAi = null;
                sourceDestraction.Play();
                Destroy(zombie);
                transform.tag = "CraftMode";
                Destroy(gameObject, sourceDestraction.clip.length);

            }
            if (transform.CompareTag("WallCrash"))
            {
                EnebledPhysics();
                sourceDestraction.Play();
                if (CraftItemStaticForWallCrash)
                {
                    CraftItemStaticForWallCrash.SetActive(true);
                }
                Destroy(gameObject, sourceDestraction.clip.length);

                timer = 0;

            }
            if (transform.CompareTag("CraftFromMenu"))
            {
                _craftItem = GetComponent<CraftItem>();
                sourceDestraction.Play();
                SoundTrue = true;
                _craftItem.RenderOff();
                destroyAi = poolsistem.InstantiateAPS("SmallExplosionEffectForZombie", transform.position, Quaternion.identity);
                timer = 0;
            }



            destroyAi.PlayEffect(30);
        }
    }
    void EnebledPhysics ()
    {
        transform.GetComponent<Collider>().enabled = false;
        transform.GetComponent<Renderer>().enabled = false;
        if (transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Renderer>())
                {
                    transform.GetChild(i).GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }


    //private void OnCollisionEnter (Collision collision)
    //{
    //    if (CollisionDestroy)
    //    {
    //        poolsistem.InstantiateAPS("SmallExplosionEffectForZombie", transform.position, Quaternion.identity);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter (Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (CollisionDestroy)
            {

                staticAudio.Play();
                Destroy(gameObject);
            } 
        }
    }
}




