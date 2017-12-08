using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC.LSky;
[System.Serializable]
public class Wave
{
    public float Day;
    public float Night;
    public List<int> countZombie = new List<int>();
    public List<GameObject> ZombiePref = new List<GameObject>();

}
public class GhostWaveCreater : MonoBehaviour
{

    public bool EasyDifficulty;
    public bool MiddleDifficulty;
    public bool HardDifficulty;
    public bool VeryHardDifficulty;

    public List<Wave> wave = new List<Wave>();
    // Use this for initialization
    WaveManager waveManager;
    CheckInWeaponAndCraft checkWeaponAndCraft;
    private void Start ()
    {
        checkWeaponAndCraft = GameObject.Find("WeaponController").GetComponent<CheckInWeaponAndCraft>();
        waveManager = GameObject.Find("Spawner").GetComponent<WaveManager>();
    }



    public void FindWaveManeger ()
    {
        waveManager.ghostCreater = transform.GetComponent<GhostWaveCreater>();
        waveManager.init();
    }



    public void UpdateLevelWaverPrefs ()
    {
        if (EasyDifficulty)
        {

            if (PlayerPrefs.GetString("EasyDifficulty") != "")
            {

                int i = int.Parse(PlayerPrefs.GetString("EasyDifficulty"));

                if (i >= int.Parse(PlayerPrefs.GetString("EasyDifficulty")))
                {
                    i++;
                    if (i == 10)
                    {
                        checkWeaponAndCraft.MyMoney.text = (int.Parse(checkWeaponAndCraft.MyMoney.text) + 7000).ToString();
                    }
                    PlayerPrefs.SetString("EasyDifficulty", i.ToString());
                }

            }


        }
        if (MiddleDifficulty)
        {
            if (PlayerPrefs.GetString("MiddleDifficulty") != "")
            {
                int i = int.Parse(PlayerPrefs.GetString("MiddleDifficulty"));
                if (i >= int.Parse(PlayerPrefs.GetString("MiddleDifficulty")))
                {
                    if (i == 10)
                    {
                        checkWeaponAndCraft.MyMoney.text = (int.Parse(checkWeaponAndCraft.MyMoney.text) + 14000).ToString();
                    }
                    PlayerPrefs.SetString("MiddleDifficulty", i++.ToString());
                }
            }
            if (HardDifficulty)
            {
                if (PlayerPrefs.GetString("HardDifficulty") != "")
                {
                    int i = int.Parse(PlayerPrefs.GetString("HardDifficulty"));
                    if (i >= int.Parse(PlayerPrefs.GetString("HardDifficulty")))
                    {
                        if (i == 10)
                        {
                            checkWeaponAndCraft.MyMoney.text = (int.Parse(checkWeaponAndCraft.MyMoney.text) + 21000).ToString();
                        }
                        PlayerPrefs.SetString("HardDifficulty", i++.ToString());
                    }
                }
                if (VeryHardDifficulty)
                {
                    if (PlayerPrefs.GetString("VeryHardDifficulty") != "")
                    {
                        int i = int.Parse(PlayerPrefs.GetString("VeryHardDifficulty"));
                        if (i >= int.Parse(PlayerPrefs.GetString("VeryHardDifficulty")))
                        {
                            if (i == 10)
                            {
                                checkWeaponAndCraft.MyMoney.text = (int.Parse(checkWeaponAndCraft.MyMoney.text) + 30000).ToString();
                            }
                            PlayerPrefs.SetString("VeryHardDifficulty", i++.ToString());
                        }

                    }
                }
            }
        }
    }
}




