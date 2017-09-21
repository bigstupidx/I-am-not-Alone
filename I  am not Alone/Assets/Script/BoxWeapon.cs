using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxWeapon : MonoBehaviour
{

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
    CheckInWeaponAndCraft _checkInWeaponAndCraft;
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        pool = PoolingSystem.Instance;
        _checkInWeaponAndCraft = _weaponController.GetComponent<CheckInWeaponAndCraft>();
     
    }

    private void OnEnable ()
    {
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        pool = PoolingSystem.Instance;
    }
    private void OnTriggerEnter (Collider other)
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
        }

        else
        {
            _weaponController.PlayerWeapon(nameWeapon, categoryWeapon, level);
        }
    }

    void Randomarams ()
    {
        int u = Random.Range(0, 3);

        switch (u)
        {
            case 0:
                Materials = true;
                textGuiPanelGoods = buildMode.panelGoods[Random.Range(0, 5)].transform;
                level = Random.Range(0, 4);
                break;
            case 1:
                int i = Random.Range(0, buildMode.interActivePrefab.Count);

                Interactive = true;
                gridBuild = _checkInWeaponAndCraft.gridBuildMenu;
                textGuiPanelGoods = buildMode.interActivePrefab[i].transform;
                level = 1;

                break;
            case 2:


                int l = Random.Range(0, _checkInWeaponAndCraft.WeaponBought.Count);

                categoryWeapon = _checkInWeaponAndCraft.WeaponBought[l].category;
                nameWeapon = _checkInWeaponAndCraft.WeaponBought[l].nameWeapon;
                level = _checkInWeaponAndCraft.WeaponBought[l].levelWeapon;
                break;
            default:
                break;
        }




    }
}
