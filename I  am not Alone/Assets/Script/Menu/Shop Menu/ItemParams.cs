using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemParams : MonoBehaviour
{
    public bool ItemCraft;
    public int levelItem;
    public Text UnlockOrUpgrade;
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
                UnlockOrUpgrade.text = "Unlock";

            }
            else
            {
                UnlockOrUpgrade.text = "Upgrade";

            }
            upgradeImage.fillAmount = levelItem / maxLevelUpgrade;
            textCoast.text = coast[levelItem];
        }
        else
        {
            textCoast.text = "";
            UnlockOrUpgrade.text = "Full";
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
                        db.InsertDBWeapon(weaponName.text, 1, category);
                    }
                    else
                    {
                        db.InsertDBCraft(weaponName.text, 1);
                    }
                    levelItem += 1;
                    MyMoney.text = (int.Parse(MyMoney.text) - int.Parse(coast.text)).ToString();
                    db.UpdateMoney(MyMoney.text);
                }
                else
                {
                    levelItem += 1;
                    MyMoney.text = (int.Parse(MyMoney.text) - int.Parse(coast.text)).ToString();
                    if (ItemCraft)
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


