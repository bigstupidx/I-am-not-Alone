using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxWeapon : MonoBehaviour
{

    PoolingSystem pool;
    public bool Materials;
    public bool Interactive;
    WeaponController _weaponController;
    public string nameWeapon;
    public int categoryWeapon;
    public int level;
    public Transform textGuiPanelGoods;
    SwitchMode buildMode;
    public Transform gridBuild;
    // Use this for initialization
    void Start ()
    {
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        _weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
        pool = PoolingSystem.Instance;
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

            if (Materials)
            {
                Materials = false;
                textGuiPanelGoods.GetComponent<Text>().text = ((int.Parse(textGuiPanelGoods.GetComponent<Text>().text) + level)).ToString();
            }
            else if (Interactive)
            {
                if(gridBuild.Find(textGuiPanelGoods.name + "(Clone)")== null)
                {
                    textGuiPanelGoods.Find("Params").GetChild(0).GetComponent<Text>().text =level.ToString();

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

            transform.parent.gameObject.DestroyAPS();


        }
    }


}
