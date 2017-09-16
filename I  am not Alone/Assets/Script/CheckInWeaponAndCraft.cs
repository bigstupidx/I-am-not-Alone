using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckInWeaponAndCraft : MonoBehaviour
{
    public bool ShopOrNot;
    public Transform gridShop;
    public Transform gridBuildMenu;
    public List<ParamsDbBoughtWeaponAndCraftItem> WeaponBought = new List<ParamsDbBoughtWeaponAndCraftItem>();
    public List<ParamsDbBoughtWeaponAndCraftItem> CraftItemBought = new List<ParamsDbBoughtWeaponAndCraftItem>();
    public List<GameObject> CraftGuiPrefab = new List<GameObject>();
    PoolingSystem pool;
    SwitchMode buildMode;
    DbGame db;
    void Start ()
    {
        db = GetComponent<DbGame>();
        db.OpenDB("DBGame.db");
        db.GetWeaponBought();
        db.GetCraftItemBought();
        if (ShopOrNot)
        {
            CheckInBoughtItem();
        }
        else
        {
            buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
            AddItemStart();
        }
        pool = PoolingSystem.Instance;
    }

    private void OnEnable ()
    {
        pool = PoolingSystem.Instance;
    }
    private void Update ()
    {
        if (Input.GetButtonDown("Jump"))
        {

            int i = Random.Range(0, WeaponBought.Count);
            GameObject box = pool.InstantiateAPS("BoxWithWeapon", new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity);
      
            box.transform.GetChild(0).GetComponent<BoxWeapon>().categoryWeapon = WeaponBought[i].category;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().nameWeapon = WeaponBought[i].nameWeapon;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().level = WeaponBought[i].levelWeapon;
        }
    }




    public void CreateBoxItem (Vector3 pos, int _makeMaterial)
    {

        GameObject box = pool.InstantiateAPS("BoxWithWeapon", pos, Quaternion.identity);
        box.transform.GetChild(0).GetComponent<BoxWeapon>().Materials = true;
        box.transform.GetChild(0).GetComponent<BoxWeapon>().textGuiPanelGoods = buildMode.panelGoods[_makeMaterial].transform;
        box.transform.GetChild(0).GetComponent<BoxWeapon>().level = Random.Range(0, 4);
    }
    public void CreateBoxInterActive (Vector3 pos)
    {
        int i = Random.Range(0, buildMode.interActivePrefab.Count);
        GameObject box = pool.InstantiateAPS("BoxWithWeapon", pos, Quaternion.identity);
        box.transform.GetChild(0).GetComponent<BoxWeapon>().Interactive = true;
        box.transform.GetChild(0).GetComponent<BoxWeapon>().gridBuild = gridBuildMenu;
        box.transform.GetChild(0).GetComponent<BoxWeapon>().textGuiPanelGoods = buildMode.interActivePrefab[i].transform;
        box.transform.GetChild(0).GetComponent<BoxWeapon>().level = 1;
    }
    public void CreateBoxWeapon (Vector3 pos)
    {
        int i = Random.Range(0, WeaponBought.Count);
        GameObject box = pool.InstantiateAPS("BoxWithWeapon", pos, Quaternion.identity);
        Debug.Log(WeaponBought[i].nameWeapon + " " + WeaponBought[i].category);
        box.transform.GetChild(0).GetComponent<BoxWeapon>().categoryWeapon = WeaponBought[i].category;
        box.transform.GetChild(0).GetComponent<BoxWeapon>().nameWeapon = WeaponBought[i].nameWeapon;
        box.transform.GetChild(0).GetComponent<BoxWeapon>().level = WeaponBought[i].levelWeapon;

    }
    void AddItemStart ()
    {
        for (int i = 0; i < CraftItemBought.Count; i++)
        {
            if (CraftItemBought[i] != null)
            {

                GameObject l = Instantiate(CraftGuiPrefab.Find((obj => obj.name.Equals(CraftItemBought[i].nameWeapon))), gridBuildMenu.position, gridBuildMenu.rotation, gridBuildMenu);
                l.GetComponent<SelectContructionForCreate>().level.text = (CraftItemBought[i].levelWeapon).ToString();
                l.GetComponent<Toggle>().group = gridBuildMenu.GetComponent<ToggleGroup>();
            }
        }
    }
    void CheckInBoughtItem ()
    {

        for (int i = 0; i < WeaponBought.Count; i++)
        {
            if (WeaponBought[i] != null)
            {

                gridShop.Find(WeaponBought[i].nameWeapon).GetComponent<ItemParams>().levelItem = WeaponBought[i].levelWeapon;
                gridShop.Find(WeaponBought[i].nameWeapon).GetComponent<ItemParams>().IntializedParams();
            }
        }
        for (int i = 0; i < CraftItemBought.Count; i++)
        {
            if (CraftItemBought[i] != null)
            {

                gridShop.Find(CraftItemBought[i].nameWeapon).GetComponent<ItemParams>().levelItem = CraftItemBought[i].levelWeapon;
                gridShop.Find(CraftItemBought[i].nameWeapon).GetComponent<ItemParams>().IntializedParams();
            }
        }

    }


}



public class ParamsDbBoughtWeaponAndCraftItem
{
    public int category;
    public string nameWeapon;
    public int levelWeapon;


    public ParamsDbBoughtWeaponAndCraftItem (string nameweapon, int level, int category)
    {
        this.category = category;
        this.nameWeapon = nameweapon;
        this.levelWeapon = level;

    }
}