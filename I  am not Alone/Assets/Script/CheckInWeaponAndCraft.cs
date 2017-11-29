using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckInWeaponAndCraft : MonoBehaviour
{
    public bool ShopOrNot;
    public Transform gridShop;
    public Transform gridBuildMenuForStart;
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
    DbGame db;
    GameObject buttonWeapon;

    SelectionWeaponForPC selectionWeaponPC;
    void Start ()
    {
        db = GetComponent<DbGame>();
        db.OpenDB("DBGame.db");
        db.GetWeaponBought();
        db.GetCraftItemBought();
        db.GetMoney();
        if (ShopOrNot)
        {
            CheckInBoughtItem();
        }
        else
        {
            selectionWeaponPC = GetComponent<SelectionWeaponForPC>();
            buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
            weaponControll = GameObject.Find("WeaponController").GetComponent<WeaponController>();
            if (CraftItemBought.Count != 0)
            {
                AddItemStartWeapon();
                AddItemStartItem();
            }

        }
        pool = PoolingSystem.Instance;
    }

    private void OnEnable ()
    {
        pool = PoolingSystem.Instance;
    }



    public void PlusAndUpdateMoneyPlayer ()
    {

        db.UpdateMoney(MyMoney.text);
    }

    public void CreateBoxItem (Vector3 pos, int _makeMaterial)
    {

        try
        {
            GameObject box = pool.InstantiateAPS("BoxWithWeapon", pos, Quaternion.identity);
            box.transform.GetChild(0).GetComponent<BoxWeapon>().Materials = true;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().textGuiPanelGoods = buildMode.panelGoods[_makeMaterial].transform;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().level = Random.Range(0, 4);
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

    public void OldWeapon (string nameWeapon, BulletSystem bul, handWeapon hand, Vector3 pos)
    {
        try
        {
            GameObject box = pool.InstantiateAPS("BoxWithMyWeapon", pos, Quaternion.identity);

            box.transform.GetChild(0).GetComponent<BoxWeapon>().categoryWeapon = WeaponBought.Find(obj => obj.nameWeapon == nameWeapon).category;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().nameWeapon = WeaponBought.Find(obj => obj.nameWeapon == nameWeapon).nameWeapon;
            box.transform.GetChild(0).GetComponent<BoxWeapon>().level = WeaponBought.Find(obj => obj.nameWeapon == nameWeapon).levelWeapon;
        }
        catch (System.Exception)
        {


            Debug.Log("Ошибка при спаунинге box");
        }

    }
    void AddItemStartItem ()
    {

        for (int i = 0; i < CraftItemBought.Count; i++)
        {
            if (CraftItemBought[i] != null)
            {

                GameObject l = Instantiate(CraftGuiPrefabStart.Find((obj => obj.name.Equals(CraftItemBought[i].nameWeapon))), gridBuildMenuForStart.position, gridBuildMenuForStart.rotation, gridBuildMenuForStart);

            }
        }
    }
    void AddItemStartWeapon ()
    {

        for (int i = 0; i < WeaponBought.Count; i++)
        {
            if (WeaponBought[i] != null)
            {

                GameObject l = Instantiate(WeaponGuiPrefab.Find((obj => obj.name.Equals(WeaponBought[i].nameWeapon))), gridWeaponMenu.position, gridWeaponMenu.rotation, gridWeaponMenu);

            }
        }
    }
    void CheckInBoughtItem ()
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

    public void StartGame ()
    {
        for (int i = 0; i < addItemCraft.Count; i++)
        {
            if (addItemCraft[i] != null)
            {

                GameObject l = Instantiate(CraftGuiPrefab.Find((obj => obj.name.Equals(addItemCraft[i]))), gridBuildMenu.position, gridBuildMenu.rotation, gridBuildMenu);
                l.GetComponent<SelectContructionForCreate>().level.text = (CraftItemBought[i].levelWeapon).ToString();
                if (gridBuildMenu.GetComponent<ToggleGroup>())
                {
                    l.GetComponent<Toggle>().group = gridBuildMenu.GetComponent<ToggleGroup>();
                }
            }
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

    public void AddWeapon (string name, Transform pos, int level, float amuni)
    {




        GameObject weapon = pool.InstantiateAPS(name, pos.position, pos.rotation, pos.gameObject);
        //weapon.transform.LookAt(weaponControll.Iktarget.GetChild(0));
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