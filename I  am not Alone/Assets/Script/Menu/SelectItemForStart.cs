using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemForStart : MonoBehaviour
{
    CheckInWeaponAndCraft checkWeapon;
    Toggle tog;
    public string nameItem;
    PlayerHealth health;
    SwitchMode buildControll;
    public bool EasyDifficulty;
    public bool MiddleDifficulty;
    public bool HardDifficulty;
    public bool VeryHardDifficulty;

    public Text EasyDifficultyText;
    public Text MiddleDifficultyText;
    public Text HardDifficultyText;
    public Text VeryHardDifficultyText;
    public Toggle[] dificultButton;
    // Use this for initialization
    void Start ()
    {
        if (EasyDifficulty)
        {
            if (PlayerPrefs.HasKey("EasyDifficulty"))
            {

                GetLevelWave(PlayerPrefs.GetString("EasyDifficulty"), EasyDifficultyText, 1);

            }
            else
            {
                PlayerPrefs.SetString("EasyDifficulty", "0");
            }
        }
        if (MiddleDifficulty)
        {

            if ((PlayerPrefs.HasKey("MiddleDifficulty")))
            {
                GetLevelWave(PlayerPrefs.GetString("MiddleDifficulty"), MiddleDifficultyText, 2);
            }
            else
            {
                PlayerPrefs.SetString("MiddleDifficulty", "0");
            }

        }
        if (HardDifficulty)
        {

            if ((PlayerPrefs.HasKey("HardDifficulty")))
            {
                GetLevelWave(PlayerPrefs.GetString("HardDifficulty"), HardDifficultyText, 3);
            }
            else
            {
                PlayerPrefs.SetString("HardDifficulty", "0");
            }

        }
        if (VeryHardDifficulty)
        {

            if ((PlayerPrefs.HasKey("VeryHardDifficulty")))
            {
                GetLevelWave(PlayerPrefs.GetString("VeryHardDifficulty"), VeryHardDifficultyText, 3);
            }
            else
            {
                PlayerPrefs.SetString("VeryHardDifficulty", "0");
            }


        }

        PlayerPrefs.Save();
        health = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerHealth>();
        checkWeapon = GameObject.Find("WeaponController").GetComponent<CheckInWeaponAndCraft>();
        tog = GetComponent<Toggle>();
        buildControll = GameObject.Find("BuildController").GetComponent<SwitchMode>();
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



    public void UpHealth (Toggle h)
    {
        if (h.isOn)
        {
            health.CurHelth = 150;
            health.MaxHealth = 150;
            health.UpdateHealth(0);
            for (int i = 0; i < buildControll.panelGoods.Count; i++)
            {
                buildControll.panelGoods[i].text = "0";
            }
        }
        else
        {
            health.CurHelth = 100;
            health.MaxHealth = 100;
            health.UpdateHealth(0);
        }
    }




    public void SelectItemCraft (Toggle g)
    {
        if (g.isOn)
        {
            if (checkWeapon.addItemCraft.Count < 5)
            {
                checkWeapon.addItemCraft.Add(nameItem);
            }
            else
            {
                tog.isOn = false;
            }

        }
        else
        {
            checkWeapon.addItemCraft.Remove(nameItem);
        }

    }
    public void SelectWeapon (Toggle g)
    {
        if (g.isOn)
        {
            if (checkWeapon.addItemCraftWeapon.Count < 3)
            {
                checkWeapon.addItemCraftWeapon.Add(nameItem);
            }
            else
            {
                tog.isOn = false;
            }
        }
        else
        {
            checkWeapon.addItemCraftWeapon.Remove(nameItem);
        }
    }

}
