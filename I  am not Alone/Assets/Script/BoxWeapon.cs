using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
public class BoxWeapon : MonoBehaviour
{


    [Space(10)]
    PoolingSystem pool;
    public bool StartGoods;
    public bool Materials;
    public bool Interactive;
    WeaponController _weaponController;
    public string nameWeapon;
    public int categoryWeapon;
    public int level;
    public Transform textGuiPanelGoods;
    SwitchMode buildMode;
    public Transform gridBuild;
    GameObject player;
    public float WeaponAmunition;
    CheckInWeaponAndCraft _checkInWeaponAndCraft;
    
    [Space(5)]
    public PlayableDirector[] m_director;
    public GameObject[] traningActive;

    float timer;
    SphereCollider thisCollider;
    AudioSource weaponAudio;
    AudioSource MaterialsAudio;
    AudioSource InteractiveAudio;
    private bool createBoxWeapon;
    private float patrons;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        pool = PoolingSystem.Instance;
        _checkInWeaponAndCraft = _weaponController.GetComponent<CheckInWeaponAndCraft>();
        Physics.IgnoreCollision(transform.parent.GetComponent<Collider>(), player.GetComponent<Collider>());

        weaponAudio = player.transform.Find("AudioBox").GetChild(0).GetComponent<AudioSource>();
        MaterialsAudio = player.transform.Find("AudioBox").GetChild(1).GetComponent<AudioSource>();
        InteractiveAudio = player.transform.Find("AudioBox").GetChild(2).GetComponent<AudioSource>();
    }

    private void OnEnable ()
    {





        player = GameObject.FindGameObjectWithTag("Player");

        weaponAudio = player.transform.Find("AudioBox").GetChild(0).GetComponent<AudioSource>();
        MaterialsAudio = player.transform.Find("AudioBox").GetChild(1).GetComponent<AudioSource>();
        InteractiveAudio = player.transform.Find("AudioBox").GetChild(2).GetComponent<AudioSource>();
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        pool = PoolingSystem.Instance;
    }
    private void OnTriggerEnter (Collider other)
    {

      
            if (other.CompareTag("Player"))
            {

                if (m_director.Length != 0)
                {
                    if (traningActive.Length != 0)
                    {
                        for (int i = 0; i < traningActive.Length; i++)
                        {
                            traningActive[i].SetActive(true);

                        }
                    }

                    for (int i = 0; i < m_director.Length; i++)
                    {
                        m_director[i].gameObject.SetActive(true);
                        m_director[i].Play();
                    }
                }

                if (StartGoods)
                {
                    Randomarams();
                    TakeGood();

                    Destroy(transform.parent.gameObject);



                }
                else
                {

                    TakeGood();

                    transform.parent.gameObject.DestroyAPS();

                }




            }
        

        if (other.CompareTag("AI"))
        {
            Physics.IgnoreCollision(transform.parent.GetComponent<Collider>(), other.GetComponent<Collider>());

        }
    }
    void TakeGood ()
    {
        if (Materials)
        {
            Materials = false;
            textGuiPanelGoods.GetComponent<Text>().text = ((int.Parse(textGuiPanelGoods.GetComponent<Text>().text) + level)).ToString();
            textGuiPanelGoods.GetComponent<PlayableDirector>().Play();

            InteractiveAudio.Play();
        }
        //else if (Interactive)
        //{
        //    if (gridBuild.Find(textGuiPanelGoods.name + "(Clone)") == null)
        //    {
        //        textGuiPanelGoods.Find("Params").GetChild(0).GetComponent<Text>().text = level.ToString();

        //        Instantiate(textGuiPanelGoods, gridBuild.position, gridBuild.rotation, gridBuild);


        //    }
        //    else
        //    {

        //        gridBuild.Find(textGuiPanelGoods.name + "(Clone)").Find("Params").GetChild(0).GetComponent<Text>().text = ((int.Parse(textGuiPanelGoods.Find("Params").GetChild(0).GetComponent<Text>().text) + level)).ToString();
        //    }

        //    Interactive = false;
        //    InteractiveAudio.Play();
        //}

        else
        {


            weaponAudio.Play();
            _weaponController.PlayerWeapon(nameWeapon, level, WeaponAmunition);

        }

    }

    void Randomarams ()
    {
        //int u = 0;
        createBoxWeapon = false;
        for (int i = 0; i < _weaponController.Weapons.Length; i++)
        {
            if (_weaponController.Weapons[i].transform.childCount != 0)
            {

                patrons += _weaponController.Weapons[i].transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition;
                if (patrons < 0.6f)
                {
                    createBoxWeapon = true;
                }

            }

        }
        //if (_checkInWeaponAndCraft.WeaponBought.Count != 0)
        //{
        //    u = Random.Range(0, 9);

        //}
        //else
        //{
        //    u = Random.Range(6, 9);
        //}


        if (createBoxWeapon)
        {
            int l = Random.Range(0, _checkInWeaponAndCraft.WeaponBought.Count);

            categoryWeapon = _checkInWeaponAndCraft.WeaponBought[l].category;
            nameWeapon = _checkInWeaponAndCraft.WeaponBought[l].nameWeapon;

            level = _checkInWeaponAndCraft.WeaponBought[l].levelWeapon;
        }
        else
        {
            Materials = true;
            int m = Random.Range(0, 5);

            if (m == 0 || m == 1)
            {
                m = 0;
            }
            if (m == 2)
            {
                m -= 1;
            }
            if (m == 3)
            {
                m -= 1;
            }
            if (m == 4)
            {
                m -= 1;
            }

            textGuiPanelGoods = buildMode.panelGoods[m].transform;
            level = Random.Range(1, 4);
        }


        //if (u >= 0 && u <= 5)
        //{
        //    int l = Random.Range(0, _checkInWeaponAndCraft.WeaponBought.Count);

        //    categoryWeapon = _checkInWeaponAndCraft.WeaponBought[l].category;
        //    nameWeapon = _checkInWeaponAndCraft.WeaponBought[l].nameWeapon;

        //    level = _checkInWeaponAndCraft.WeaponBought[l].levelWeapon;



        //}
        //else if (u > 5 && u <= 8)
        //{
        //    Materials = true;
        //    int m = Random.Range(0, 5);

        //    if (m == 0 && m == 1)
        //    {
        //        m = 0;
        //    }
        //    if (m == 2)
        //    {
        //        m -= 1;
        //    }
        //    if (m == 3)
        //    {
        //        m -= 1;
        //    }
        //    if (m == 4)
        //    {
        //        m -= 1;
        //    }

        //    textGuiPanelGoods = buildMode.panelGoods[m].transform;
        //    level = Random.Range(1, 4);
        //}










    }

}
