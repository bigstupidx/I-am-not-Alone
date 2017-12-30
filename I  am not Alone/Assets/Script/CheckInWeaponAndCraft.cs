using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInWeaponAndCraft : MonoBehaviour
{
    public bool ShopOrNot;
    public Transform gridShop;
 
    public Transform gridBuildMenu;
    public Transform gridWeaponMenu;
    WeaponController weaponControll;
    public List<ParamsDbBoughtWeaponAndCraftItem> WeaponBought = new List<ParamsDbBoughtWeaponAndCraftItem>();
    public List<ParamsDbBoughtWeaponAndCraftItem> CraftItemBought = new List<ParamsDbBoughtWeaponAndCraftItem>();
    public List<GameObject> CraftGuiPrefab = new List<GameObject>();
    public List<GameObject> CraftGuiPrefabStart = new List<GameObject>();
    public List<GameObject> WeaponGuiPrefab = new List<GameObject>();

    public List<string> addItemCraft = new List<string>();
    public List<string> addItemCraftWeapon = new List<string>();
    public Text MyMoney;

    PoolingSystem pool;
    SwitchMode buildMode;
    //   DbGame db;
    SaveData save;
    GameObject buttonWeapon;
    MyMainMenu menu;
    SelectionWeaponForPC selectionWeaponPC;
    PlayerHealth health;
    public GameObject[] ProgresStart;
    public Text[] materialsCounter;
    void Start ()
    {
        pool = PoolingSystem.Instance;
        //  db = GetComponent<DbGame>();
        save = GetComponent<SaveData>();
        save.GetMoney();
        //db.OpenDB("DBGame.db");
        save.GetWeaponBought();
        // db.GetWeaponBought();
        save.GetCraftItemBought();
        save.GetInventory();
        //db.GetCraftItemBought();
        //db.GetMoney();

        if (ShopOrNot)
        {
            menu = GetComponent<MyMainMenu>();
            CheckLevelInBoughtItem();
            CheckInventoryShop();
        }
        else
        {

            selectionWeaponPC = GetComponent<SelectionWeaponForPC>();
            buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
            weaponControll = GameObject.Find("WeaponController").GetComponent<WeaponController>();
            health = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerHealth>();


            if (CraftItemBought.Count == 0)
            {
                gridBuildMenu.gameObject.SetActive(false);


            }


            StartGame();

        }

    }

    private void OnEnable ()
    {
        pool = PoolingSystem.Instance;
    }



    public void PlusAndUpdateMoneyPlayer ()
    {
        save.UpdateMoney(MyMoney.text);

    }

    public void CreateBoxItem (Vector3 pos, int _makeMaterial)
    {

        try
        {
            GameObject box = pool.InstantiateAPS("BoxWithWeapon", pos, Quaternion.identity);
            box.transform.GetChild(0).GetComponent<BoxWeapon>().Materials = true;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().textGuiPanelGoods = buildMode.panelGoods[_makeMaterial].transform;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().level = Random.Range(2, 5);
            box.transform.GetChild(0).GetComponent<BoxWeapon>().StartGoods = false;
        }
        catch (System.Exception)
        {


            Debug.Log("Ошибка при спаунинге CreateBoxItem");


        }
    }
    public void CreateBoxInterActive (Vector3 pos)
    {
        try
        {
            int i = Random.Range(0, buildMode.interActivePrefab.Count);
            GameObject box = pool.InstantiateAPS("BoxWithWeapon", pos + new Vector3(0, 2, 0), Quaternion.identity);
            box.transform.GetChild(0).GetComponent<BoxWeapon>().Interactive = true;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().gridBuild = gridBuildMenu;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().textGuiPanelGoods = buildMode.interActivePrefab[i].transform;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().level = 1;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().StartGoods = false;
        }
        catch (System.Exception)
        {


            Debug.Log("Ошибка при спаунинге CreateBoxInterActive");
        }
    }
    public void CreateBoxWeapon (Vector3 pos)
    {
        try
        {
            int i = Random.Range(0, addItemCraftWeapon.Count);
            GameObject box = pool.InstantiateAPS("BoxWithWeapon", pos, Quaternion.identity);


            box.transform.GetChild(0).GetComponent<BoxWeapon>().nameWeapon = addItemCraftWeapon[i];
            box.transform.GetChild(0).GetComponent<BoxWeapon>().level = WeaponBought.Find(obj => obj.nameWeapon.Equals(addItemCraftWeapon[i])).levelWeapon;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().WeaponAmunition = 1;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().StartGoods = false;
        }
        catch (System.Exception)
        {


            Debug.Log("Ошибка при спаунинге CreateBoxWeapon");
        }
    }




    public void CheckLevelInBoughtItem ()
    {

        for (int i = 0; i < WeaponBought.Count; i++)
        {
            if (WeaponBought[i] != null)
            {


                if (gridShop.Find(WeaponBought[i].nameWeapon))
                {
                    gridShop.Find(WeaponBought[i].nameWeapon).GetComponent<ItemParams>().levelItem = WeaponBought[i].levelWeapon;
                    gridShop.Find(WeaponBought[i].nameWeapon).GetComponent<ItemParams>().IntializedParams();

                }
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
    void CheckInventoryShop ()
    {

        for (int i = 0; i < addItemCraft.Count; i++)
        {
            GameObject l = Instantiate(CraftGuiPrefabStart.Find((obj => obj.name.Equals(addItemCraft[i]))), gridBuildMenu.position, gridBuildMenu.rotation, gridBuildMenu);
            l.name = addItemCraft[i];
        }
        for (int i = 0; i < addItemCraftWeapon.Count; i++)
        {
            buttonWeapon = Instantiate(WeaponGuiPrefab.Find((obj => obj.name.Equals(addItemCraftWeapon[i]))), gridWeaponMenu.position, gridWeaponMenu.rotation, gridWeaponMenu);
            buttonWeapon.name = addItemCraftWeapon[i];
        }
    }

    public void ButtonAddInventory (ItemParams itemParams)
    {
        if (itemParams.ItemCraft)
        {
            if (gridBuildMenu.childCount < 3)
            {

                if (gridBuildMenu.Find(itemParams.weaponName.text) == null)
                {
                    GameObject l = Instantiate(CraftGuiPrefabStart.Find((obj => obj.name.Equals(itemParams.weaponName.text))), gridBuildMenu.position, gridBuildMenu.rotation, gridBuildMenu);
                    l.name = itemParams.weaponName.text;
                    save.InsertInventoryItemCraft(itemParams.weaponName.text, itemParams.levelItem);
                }
            }
        }
        else
        {
            if (gridWeaponMenu.childCount < 3)
            {

                if (gridWeaponMenu.Find(itemParams.weaponName.text) == null)
                {
                    buttonWeapon = Instantiate(WeaponGuiPrefab.Find((obj => obj.name.Equals(itemParams.weaponName.text))), gridWeaponMenu.position, gridWeaponMenu.rotation, gridWeaponMenu);
                    buttonWeapon.name = itemParams.weaponName.text;
                    save.InsertInventoryWeapon(itemParams.weaponName.text, itemParams.levelItem);
                }
            }
        }
    }


    public void StartGame ()
    {

        health.MaxHealth = PlayerPrefs.GetFloat("MaxHealth");
        health.CurHelth = PlayerPrefs.GetFloat("CurHelth");
        ProgresStart[PlayerPrefs.GetInt("ActiveDifficulty")].SetActive(true);
        materialsCounter[0].text = PlayerPrefs.GetString("wood");
        materialsCounter[1].text = PlayerPrefs.GetString("metal");
        materialsCounter[2].text = PlayerPrefs.GetString("provoda");
        materialsCounter[3].text = PlayerPrefs.GetString("electric");




        if (addItemCraft.Count != 0)
        {
            for (int i = 0; i < addItemCraft.Count; i++)
            {


                GameObject l = Instantiate(CraftGuiPrefab.Find((obj => obj.name.Equals(addItemCraft[i]))), gridBuildMenu.position, gridBuildMenu.rotation, gridBuildMenu);
                l.GetComponent<SelectContructionForCreate>().level.text = (CraftItemBought[i].levelWeapon).ToString();
                if (gridBuildMenu.GetComponent<ToggleGroup>())
                {
                    l.GetComponent<Toggle>().group = gridBuildMenu.GetComponent<ToggleGroup>();
                }

            }
        }
        else
        {

            gridBuildMenu.gameObject.SetActive(false);

        }
        for (int i = 0; i < addItemCraftWeapon.Count; i++)
        {
            if (addItemCraftWeapon[i] != null)
            {

                //   GameObject l = Instantiate(weaponControll.WeaponImage.Find((obj => obj.name.Equals(addItemCraftWeapon[i]))), weaponControll.weaponPanel.position, weaponControll.weaponPanel.rotation, weaponControll.weaponPanel);
                buttonWeapon = Instantiate(weaponControll.WeaponImage.Find((obj => obj.name.Equals(addItemCraftWeapon[i]))), weaponControll.weaponPanel);

                AddWeapon(addItemCraftWeapon[i], weaponControll.Weapons[i].transform, WeaponBought.Find(obj => obj.nameWeapon.Equals(addItemCraftWeapon[i])).levelWeapon, 1);

                buttonWeapon.transform.GetChild(0).GetComponent<Toggle>().group = buttonWeapon.transform.parent.GetComponent<ToggleGroup>();
                Button btn = buttonWeapon.GetComponent<Button>();
                switch (i)
                {
                    case 0:
                        btn.onClick.AddListener(selectionWeaponPC.Weapon1);
                        break;
                    case 1:
                        btn.onClick.AddListener(selectionWeaponPC.Weapon2);
                        break;
                    case 2:
                        btn.onClick.AddListener(selectionWeaponPC.Weapon3);
                        break;
                    default:
                        break;
                }

                weaponControll.Weapons[i].transform.GetChild(0).GetComponent<BulletSystem>().buttonWeapon = buttonWeapon.transform;
                weaponControll.Weapons[i].transform.GetChild(0).GetComponent<BulletSystem>().resolution = true;
            }
        }
    }

    void AddWeapon (string name, Transform pos, int level, float amuni)
    {




        GameObject weapon = Instantiate(weaponControll.weaponPreafab.Find(x => x.name.Equals(name)), pos.position, pos.rotation, pos.transform);

        weapon.GetComponent<BulletSystem>().level = level;
        weapon.GetComponent<BulletSystem>().WeaponAmmunition = amuni;




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