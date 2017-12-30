using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemParams : MonoBehaviour
{
    public bool ItemCraft;
    public int levelItem;
    public Text Upgrade;
    public GameObject Unlock;
    public Text Full;
    public Text textCoast;
    public Text weaponName;
    public float maxLevelUpgrade = 10;
    public Text MyMoney;
    public Image upgradeImage;
    public int category;
    public List<string> coast = new List<string>();
    public GameObject ButtonPlus;
    MyMainMenu menu;
    SaveData save;
    //  DbGame db;
    // Use this for initialization
    void Start ()
    {
        //    db = GameObject.Find("MenuController").GetComponent<DbGame>();
        save = GameObject.Find("MenuController").GetComponent<SaveData>();
    
        // db.OpenDB("DBGame.db");
        IntializedParams();

    }

    public void IntializedParams ()
    {
        menu = GameObject.Find("MenuController").GetComponent<MyMainMenu>();
        if (levelItem < maxLevelUpgrade)
        {

            if (levelItem == 0)
            {
                ButtonPlus.SetActive(false);
                Unlock.gameObject.SetActive(true);
                Upgrade.gameObject.SetActive(false);
                Full.gameObject.SetActive(false);
            }
            else
            {
                ButtonPlus.SetActive(true);
                Unlock.gameObject.SetActive(false);
                Upgrade.gameObject.SetActive(true);
                Full.gameObject.SetActive(false);

            }
            upgradeImage.fillAmount = levelItem / maxLevelUpgrade;
            textCoast.text = coast[levelItem];
        }
        else
        {
            textCoast.text = "";
            Unlock.gameObject.SetActive(false);
            Upgrade.gameObject.SetActive(false);
            Full.gameObject.SetActive(true);
        }
        menu.MidLevel.Add(levelItem);
        menu.UpdateMoneyADsAndShare();
    }

    public void ButtonUpgrade (Text coast)
    {
        if (levelItem < maxLevelUpgrade)
        {
            if (int.Parse(coast.text) > int.Parse(MyMoney.text)) { return; }
            else
            {
                if (levelItem == 0)
                {
                    if (ItemCraft)
                    {

                        save.InsertDBCraft(weaponName.text, 1);
                    }
                    else
                    {
                        save.InsertDBWeapon(weaponName.text, 1, category);
                    }
                    levelItem += 1;
                    MyMoney.text = (int.Parse(MyMoney.text) - int.Parse(coast.text)).ToString();
                    //db.UpdateMoney(MyMoney.text);
                    save.UpdateMoney(MyMoney.text);
                }
                else
                {
               
                    levelItem += 1;
                    MyMoney.text = (int.Parse(MyMoney.text) - int.Parse(coast.text)).ToString();
                    if (!ItemCraft)
                    {
                        save.UpdateDBWeapon(weaponName.text, levelItem);
                    }
                    else
                    {
                        save.UpdateDBCraft(weaponName.text, levelItem);
                    }
                    // db.UpdateMoney(MyMoney.text);
                    save.UpdateMoney(MyMoney.text);
                }


                IntializedParams();
            }
        }


    }
}


