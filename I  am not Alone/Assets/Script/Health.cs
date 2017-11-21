using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.AI;
using UnityEngine.Playables;

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
    public Image damageImage;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public Image HealthPlayer;
    public ParticleSystem blood;
    public ParticleSystem WoodDoor;
    public GameObject imageGameOver;

    [Space(15)]
    [Header("For Ai")]
    public GameObject[] enablesBoody;
    public GameObject patAi1;
    public GameObject patAi2;

    public SkinnedMeshRenderer skinnedMesh;

    public bool OrRandom;
    public int MoneyAi;
    bool damaged;
    [Space(15)]
    [Header("For door")]

    public AudioClip doorHitAudio;

    [Space(15)]
    PoolingSystem poolsistem;
    SwitchMode buildMode;
    CraftItem _craftItem;
    CheckInWeaponAndCraft checkWeaponAndCraft;
    WeaponController weaponsControll;
    private GameObject destroyAi;
    AudioSource sourceDestraction;
    AudioSource staticAudio;
    Transform playerG;
    bool SoundTrue;
    float timer;
    private Rigidbody rigid;
    Animator m_anim;
    Text ghostCounter;
    WaveManager waveManager;
    bool isdead;
    PlayableDirector m_director;
    private void Start ()
    {

        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        checkWeaponAndCraft = GameObject.Find("WeaponController").GetComponent<CheckInWeaponAndCraft>();
        weaponsControll = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        staticAudio = GameObject.Find("StaticAudio").GetComponent<AudioSource>();
        playerG = GameObject.FindGameObjectWithTag("Player").transform;
        ghostCounter = buildMode.CounterZombie.GetComponent<Text>();
        rigid = GetComponent<Rigidbody>();
        waveManager = GameObject.Find("Spawner").GetComponent<WaveManager>();
        m_director = checkWeaponAndCraft.MyMoney.GetComponent<PlayableDirector>();
        if (!transform.CompareTag("Player"))
        {


            sourceDestraction = GetComponent<AudioSource>();
            if (!sourceDestraction)
            {
                sourceDestraction = transform.parent.GetComponent<AudioSource>();
            }
        }

        if (transform.parent != null)
        {
            _craftItem = transform.parent.GetComponent<CraftItem>();
        }

        poolsistem = PoolingSystem.Instance;
        if (transform.CompareTag("Player"))
        {
            HealthPlayer.fillAmount = CurHelth / MaxHealth;
            blood.Stop();
            blood.Clear();
        }

        if (transform.CompareTag(Tags.AI))
        {

            CurHelth = MaxHealth;

        }
    }
    private void OnEnable ()
    {
        if (transform.CompareTag(Tags.AI))
        {
            transform.tag = Tags.AI;
            CurHelth = MaxHealth;
            EnebledPhysics(true);
            if (patAi1)
            {
                patAi1.SetActive(true);
                patAi2.SetActive(true);
            }
            isdead = false;
            if (enablesBoody.Length != 0)
            {
                for (int i = 0; i < enablesBoody.Length; i++)
                {
                    enablesBoody[i].SetActive(true);
                }
            }
        }
    }





    private void Update ()
    {


        if (damaged)
        {
            if (transform.CompareTag("Player"))
            {
                if (damaged)
                {
                    // ... set the colour of the damageImage to the flash colour.
                    damageImage.color = flashColour;
                }
                // Otherwise...
                else
                {
                    // ... transition the colour back to clear.
                    damageImage.color = Color.Lerp(damageImage.color, Color.clear, 5.0f * Time.deltaTime);
                }

                // Reset the damaged flag.
                damaged = false;
            }
        }
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
        destroyAi = poolsistem.InstantiateAPS("SmallExplosionEffect", transform.position, Quaternion.identity);
        SoundTrue = true;
        timer = 0;
        _craftItem = GetComponent<CraftItem>();
        _craftItem.RenderOff();
        destroyAi.PlayEffect(30);

    }

    public void HelthDamage (float damage, bool playerAttack, Vector3 pointhitGhost)
    {


        CurHelth -= damage;
        if (transform.CompareTag(Tags.Things))
        {
            if (WoodDoor)
            {
                if (!sourceDestraction.isPlaying)
                {
                    sourceDestraction.PlayOneShot(doorHitAudio);
                }

                //    WoodDoor.transform.position = pointhitGhost;
                //    WoodDoor.transform.LookAt(pointhitGhost);
                WoodDoor.Play();

            }
        }
        if (transform.CompareTag("Player"))
        {
            damaged = true;
            HealthPlayer.fillAmount = CurHelth / MaxHealth;
            blood.Play();
        }
        if (transform.CompareTag("AI"))
        {
            blood.transform.position = pointhitGhost;

            // And play the particles.
            blood.Play();
            if (CurHelth < MaxHealth / 2)
            {

                if (patAi1)
                {
                    patAi1.SetActive(false);
                }
            }
            if (CurHelth < MaxHealth / 3)
            {
                if (patAi2)
                {
                    patAi2.SetActive(false);
                }
            }
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




                //   checkWeaponAndCraft.CreateBoxItem(transform.position,MakeMaterial);
                sourceDestraction.Play();

                if (!nothing)
                {
                    checkWeaponAndCraft.CreateBoxInterActive(transform.position);
                }
                EnebledPhysics(false);
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

                if (isdead)
                {
                    return;
                }
                destroyAi = poolsistem.InstantiateAPS("DethGhost", transform.position, Quaternion.identity);
                ZombieLevel1 zombie = GetComponent<ZombieLevel1>();




                //for (int i = 0; i < enablesBoody.Length; i++)
                //{
                //    enablesBoody[i].SetActive(false);
                //}

                //  zombie.m_animator.SetTrigger("die");
                //  EnebledPhysics(false);


                if (playerAttack)
                {
                    checkWeaponAndCraft.MyMoney.text = (int.Parse(checkWeaponAndCraft.MyMoney.text) + MoneyAi).ToString();
                    m_director.Play();
                }

                if (weaponsControll.weaponPanel.childCount != 0)
                {
                    int r = Random.Range(0, 2);
                    if (r == 1)
                    {
                        if (OrRandom)
                        {
                            int i = Random.Range(0, 2);
                            int l = Random.Range(0, 4);

                            MakeMaterial = l;
                            if (i == 0)
                            {
                                checkWeaponAndCraft.CreateBoxItem(transform.position, MakeMaterial);
                            }
                            else if (i == 1)
                            {
                                checkWeaponAndCraft.CreateBoxWeapon(transform.position);
                            }
                        }
                    }
                }
                else
                {
                    checkWeaponAndCraft.CreateBoxWeapon(transform.position);
                }

                sourceDestraction.clip = zombie.zombieDeth;

                weaponsControll.autolookEnemy.TargetAi = null;
                weaponsControll.autolookEnemy.WeaponNull();
                sourceDestraction.Play();
                // Destroy(zombie);



                gameObject.DestroyAPS();







                isdead = true;
                // transform.tag = "CraftMode";
                transform.position = new Vector3(0, -85f, 0);

                if (waveManager._lsky.IsNight)
                {

                    ghostCounter.text = (int.Parse(ghostCounter.text) - 1).ToString();
                    if (ghostCounter.text.Equals("0"))
                    {
                        waveManager._lskyTod.dayInSeconds = 2.0f;
                    }
                }





                return;
            }
            if (transform.CompareTag("WallCrash"))
            {
                EnebledPhysics(false);
                sourceDestraction.Play();
                if (CraftItemStaticForWallCrash)
                {
                    CraftItemStaticForWallCrash.SetActive(true);
                }
                destroyAi = poolsistem.InstantiateAPS("SmallExplosionEffectForZombie", transform.position, Quaternion.identity);
                Destroy(gameObject, sourceDestraction.clip.length);

                timer = 0;

            }
            if (transform.CompareTag("CraftFromMenu"))
            {
                destroyAi = poolsistem.InstantiateAPS("SmallExplosionEffect", transform.position, Quaternion.identity);
                _craftItem = GetComponent<CraftItem>();
                sourceDestraction.Play();
                SoundTrue = true;
                _craftItem.RenderOff();

                timer = 0;
            }



            // destroyAi.PlayEffect(30);
        }
    }
    void EnebledPhysics (bool active)
    {
        transform.GetComponent<Collider>().enabled = active;
        if (transform.GetComponent<NavMeshObstacle>())
        {
            transform.GetComponent<NavMeshObstacle>().enabled = active;
        }
        if (transform.GetComponent<Renderer>())
        {
            transform.GetComponent<Renderer>().enabled = active;
        }


        if (transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Renderer>())
                {
                    transform.GetChild(i).GetComponent<Renderer>().enabled = active;
                }
            }
        }
    }




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




