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
    public bool TriggerTrue;
    bool triggerEnable = false;
    float timer;
    SphereCollider thisCollider;
    AudioSource weaponAudio;
    AudioSource MaterialsAudio;
    AudioSource InteractiveAudio;
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        pool = PoolingSystem.Instance;
        _checkInWeaponAndCraft = _weaponController.GetComponent<CheckInWeaponAndCraft>();
        Physics.IgnoreCollision(transform.parent.GetComponent<Collider>(), player.GetComponent<Collider>());
        thisCollider = GetComponent<SphereCollider>();
        weaponAudio = player.transform.Find("AudioBox").GetChild(0).GetComponent<AudioSource>();
        MaterialsAudio = player.transform.Find("AudioBox").GetChild(1).GetComponent<AudioSource>();
        InteractiveAudio = player.transform.Find("AudioBox").GetChild(2).GetComponent<AudioSource>();
    }
    private void Update ()
    {
        if (TriggerTrue)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                triggerEnable = true;
                TriggerTrue = false;
                thisCollider.enabled = true;
            }
        }
    }

    private void OnEnable ()
    {
        thisCollider = GetComponent<SphereCollider>();
        timer = 1.5f;
        TriggerTrue = true;
        triggerEnable = false;
        thisCollider.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
      
        weaponAudio = player.transform.Find("AudioBox").GetChild(0).GetComponent<AudioSource>();
        MaterialsAudio = player.transform.Find("AudioBox").GetChild(1).GetComponent<AudioSource>();
        InteractiveAudio = player.transform.Find("AudioBox").GetChild(2).GetComponent<AudioSource>();
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        pool = PoolingSystem.Instance;
    }
    private void OnTriggerEnter (Collider other)
    {

        if (triggerEnable)
        {
            if (other.CompareTag("Player"))
            {

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
        else if (Interactive)
        {
            if (gridBuild.Find(textGuiPanelGoods.name + "(Clone)") == null)
            {
                textGuiPanelGoods.Find("Params").GetChild(0).GetComponent<Text>().text = level.ToString();

                Instantiate(textGuiPanelGoods, gridBuild.position, gridBuild.rotation, gridBuild);


            }
            else
            {

                gridBuild.Find(textGuiPanelGoods.name + "(Clone)").Find("Params").GetChild(0).GetComponent<Text>().text = ((int.Parse(textGuiPanelGoods.Find("Params").GetChild(0).GetComponent<Text>().text) + level)).ToString();
            }

            Interactive = false;
            InteractiveAudio.Play();
        }

        else
        {

            weaponAudio.Play();
            _weaponController.PlayerWeapon(nameWeapon, categoryWeapon, level, WeaponAmunition);
        }

    }

    void Randomarams ()
    {
        int u = Random.Range(0, 10);


        if (u >= 0 && u <= 5)
        {
            int l = Random.Range(0, _checkInWeaponAndCraft.WeaponBought.Count);

            categoryWeapon = _checkInWeaponAndCraft.WeaponBought[l].category;
            nameWeapon = _checkInWeaponAndCraft.WeaponBought[l].nameWeapon;

            level = _checkInWeaponAndCraft.WeaponBought[l].levelWeapon;


        }
        else if (u > 5 && u <= 8)
        {
            Materials = true;
            int m = Random.Range(0, 5);

            if (m == 0 && m == 1)
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
        else if (u == 9)
        {
            int i = Random.Range(0, buildMode.interActivePrefab.Count);

            Interactive = true;
            gridBuild = _checkInWeaponAndCraft.gridBuildMenu;
            textGuiPanelGoods = buildMode.interActivePrefab[i].transform;
            level = 1;
        }











    }

}
