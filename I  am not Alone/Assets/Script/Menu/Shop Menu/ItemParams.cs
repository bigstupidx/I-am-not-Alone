using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemParams : MonoBehaviour
{
    public bool ItemCraft;
    public int levelItem;
    public Text Upgrade;
    public Text Unlock;
    public Text Full;
    public Text textCoast;
    public Text weaponName;
    public float maxLevelUpgrade = 10;
    public Text MyMoney;
    public Image upgradeImage;
    public int category;
    public List<string> coast = new List<string>();
 
    
    DbGame db;
    // Use this for initialization
    void Start ()
    {
        db = GameObject.Find("MenuController").GetComponent<DbGame>();
     
      
        db.OpenDB("DBGame.db");
        IntializedParams();

    }

    public void IntializedParams ()
    {
        if (levelItem < maxLevelUpgrade)
        {

            if (levelItem == 0)
            {
                Unlock.gameObject.SetActive(true);
                Upgrade.gameObject.SetActive(false);
                Full.gameObject.SetActive(false);
            }
            else
            {
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
                    
                        db.InsertDBCraft(weaponName.text, 1);
                    }
                    else
                    {
                        db.InsertDBWeapon(weaponName.text, 1, category);
                    }
                    levelItem += 1;
                    MyMoney.text = (int.Parse(MyMoney.text) - int.Parse(coast.text)).ToString();
                    db.UpdateMoney(MyMoney.text);
                }
                else
                {
                    levelItem += 1;
                    MyMoney.text = (int.Parse(MyMoney.text) - int.Parse(coast.text)).ToString();
                    if (!ItemCraft)
                    {
                        db.UpdateDBWeapon(weaponName.text, levelItem);
                    }
                    else
                    {
                        db.UpdateDBCraft(weaponName.text, levelItem);
                    }
                    db.UpdateMoney(MyMoney.text);
                }


                IntializedParams();
            }
        }


    }
}


