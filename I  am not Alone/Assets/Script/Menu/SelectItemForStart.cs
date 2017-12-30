using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemForStart : MonoBehaviour
{
    //   CheckInWeaponAndCraft checkWeapon;
    Toggle tog;
    public bool ShopOrNot;



    public Text EasyDifficultyText;
    public Text MiddleDifficultyText;
    public Text HardDifficultyText;
    public Text VeryHardDifficultyText;
    public Toggle[] dificultButton;
    private string sceneActive;
    // Use this for initialization
    void Start ()
    {


        if (!ShopOrNot)
        {

            PlayerPrefs.Save();
            //  health = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerHealth>();
            //  checkWeapon = GameObject.Find("WeaponController").GetComponent<CheckInWeaponAndCraft>();
            tog = GetComponent<Toggle>();
            //  buildControll = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        }
        PlayerPrefs.SetString("wood", "0");
        PlayerPrefs.SetString("metal", "0");
        PlayerPrefs.SetString("provoda", "0");
        PlayerPrefs.SetString("electric", "0");
        PlayerPrefs.SetFloat("MaxHealth", 100);
        PlayerPrefs.SetFloat("CurHelth", 100);
        if (dificultButton.Length != 0)
        {
            ActiveDifficulty(dificultButton[0].transform);
        }
    }

    public void ButtonDificulty ()
    {
        Restart(sceneActive);
    }


    public void Restart (Text sceneSelect)
    {
        sceneActive = sceneSelect.text;
        if (PlayerPrefs.HasKey("EasyDifficulty" + sceneSelect.text))
        {

            GetLevelWave(PlayerPrefs.GetString("EasyDifficulty" + sceneSelect.text), EasyDifficultyText, 1);

        }
        else
        {
            PlayerPrefs.SetString("EasyDifficulty" + sceneSelect.text, "0");
        }



        if ((PlayerPrefs.HasKey("MiddleDifficulty" + sceneSelect.text)))
        {
            GetLevelWave(PlayerPrefs.GetString("MiddleDifficulty" + sceneSelect.text), MiddleDifficultyText, 2);

        }
        else
        {
            PlayerPrefs.SetString("MiddleDifficulty" + sceneSelect.text, "0");

        }



        if ((PlayerPrefs.HasKey("HardDifficulty" + sceneSelect.text)))
        {
            GetLevelWave(PlayerPrefs.GetString("HardDifficulty" + sceneSelect.text), HardDifficultyText, 3);
        }
        else
        {
            PlayerPrefs.SetString("HardDifficulty" + sceneSelect.text, "0");
        }



        if ((PlayerPrefs.HasKey("VeryHardDifficulty" + sceneSelect.text)))
        {
            GetLevelWave(PlayerPrefs.GetString("VeryHardDifficulty" + sceneSelect.text), VeryHardDifficultyText, 3);
        }
        else
        {


            PlayerPrefs.SetString("VeryHardDifficulty" + sceneSelect.text, "0");


        }

        PlayerPrefs.Save();

    }
    void Restart (string sceneSelect)
    {
         
        if (PlayerPrefs.HasKey("EasyDifficulty" + sceneSelect))
        {

            GetLevelWave(PlayerPrefs.GetString("EasyDifficulty" + sceneSelect), EasyDifficultyText, 1);

        }
        else
        {
            PlayerPrefs.SetString("EasyDifficulty" + sceneSelect, "0");
        }



        if ((PlayerPrefs.HasKey("MiddleDifficulty" + sceneSelect)))
        {
            GetLevelWave(PlayerPrefs.GetString("MiddleDifficulty" + sceneSelect), MiddleDifficultyText, 2);

        }
        else
        {
            PlayerPrefs.SetString("MiddleDifficulty" + sceneSelect, "0");

        }



        if ((PlayerPrefs.HasKey("HardDifficulty" + sceneSelect)))
        {
            GetLevelWave(PlayerPrefs.GetString("HardDifficulty" + sceneSelect), HardDifficultyText, 3);
        }
        else
        {
            PlayerPrefs.SetString("HardDifficulty" + sceneSelect, "0");
        }



        if ((PlayerPrefs.HasKey("VeryHardDifficulty" + sceneSelect)))
        {
            GetLevelWave(PlayerPrefs.GetString("VeryHardDifficulty" + sceneSelect), VeryHardDifficultyText, 3);
        }
        else
        {


            PlayerPrefs.SetString("VeryHardDifficulty" + sceneSelect, "0");


        }

        PlayerPrefs.Save();

    }

    public void ActiveDifficulty (Transform t)
    {
        PlayerPrefs.SetInt("ActiveDifficulty", t.GetSiblingIndex());
    }

    private void GetLevelWave (string v, Text t, int pos)
    {


        if (int.Parse(v) >= 10)
        {
            t.text = "10";


            if (pos <= dificultButton.Length)
            {
                dificultButton[pos].interactable = true;
            }

        }
        else
        {
            t.text = v;


        }



    }


    public void MaterialPlus (Toggle h)
    {
        if (h.isOn)
        {
            PlayerPrefs.SetString("wood", "5");
            PlayerPrefs.SetString("metal", "4");
            PlayerPrefs.SetString("provoda", "4");
            PlayerPrefs.SetString("electric", "1");

        }
        else
        {
            PlayerPrefs.SetString("wood", "0");
            PlayerPrefs.SetString("metal", "0");
            PlayerPrefs.SetString("provoda", "0");
            PlayerPrefs.SetString("electric", "0");


        }
    }

    public void UpHealth (Toggle h)
    {
        if (h.isOn)
        {
            PlayerPrefs.SetFloat("MaxHealth", 150);
            PlayerPrefs.SetFloat("CurHelth", 150);


            //  health.UpdateHealth(0);
            //for (int i = 0; i < buildControll.panelGoods.Count; i++)
            //{
            //    buildControll.panelGoods[i].text = "0";
            //}
        }
        else
        {
            PlayerPrefs.SetFloat("MaxHealth", 100);
            PlayerPrefs.SetFloat("CurHelth", 100);

            //   health.UpdateHealth(0);
        }
    }


    public void DestroyOBject (bool ItemCraft)
    {
        SaveData save = GameObject.Find("MenuController").GetComponent<SaveData>();
        if (ItemCraft)
        {
            save.DeleteInventoryItemCraft(transform.name);
        }
        else
        {
            save.DeleteInventoryWeapon(transform.name);
        }

        Destroy(this.gameObject);
    }



}
