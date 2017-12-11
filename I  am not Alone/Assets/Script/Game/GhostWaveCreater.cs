using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    void OnApplicationPause (bool pauseStatus)
    {
        if (pauseStatus)
        {

            PlayerPrefs.Save();
        }
    }

    public void UpdateLevelWaverPrefs (int levelWave)
    {
        if (EasyDifficulty)
        {



            if (PlayerPrefs.HasKey("EasyDifficulty"))
            {
                int i = int.Parse(PlayerPrefs.GetString("EasyDifficulty"));

                if (i <= levelWave)
                {

                    if (levelWave == 10)
                    {
                        checkWeaponAndCraft.MyMoney.text = (int.Parse(checkWeaponAndCraft.MyMoney.text) + 7000).ToString();
                    }
                    PlayerPrefs.SetString("EasyDifficulty", levelWave.ToString());
                    PlayerPrefs.Save();


                }
            }


        }
        if (MiddleDifficulty)
        {
            if (PlayerPrefs.HasKey("MiddleDifficulty"))
            {

                int i = int.Parse(PlayerPrefs.GetString("MiddleDifficulty"));
                if (i <= levelWave)
                {

                    if (levelWave == 10)
                    {
                        checkWeaponAndCraft.MyMoney.text = (int.Parse(checkWeaponAndCraft.MyMoney.text) + 14000).ToString();
                    }
                    PlayerPrefs.SetString("MiddleDifficulty", levelWave.ToString());
                    PlayerPrefs.Save();
                }
            }


        }
        if (HardDifficulty)
        {

            if (PlayerPrefs.HasKey("HardDifficulty"))
            {
                int i = int.Parse(PlayerPrefs.GetString("HardDifficulty"));
                if (i <= levelWave)
                {

                    if (levelWave == 10)
                    {
                        checkWeaponAndCraft.MyMoney.text = (int.Parse(checkWeaponAndCraft.MyMoney.text) + 21000).ToString();
                    }
                    PlayerPrefs.SetString("HardDifficulty", levelWave.ToString());
                    PlayerPrefs.Save();
                }

            }

        }
        if (VeryHardDifficulty)
        {

            if (PlayerPrefs.HasKey("VeryHardDifficulty"))
            {
                int i = int.Parse(PlayerPrefs.GetString("VeryHardDifficulty"));
                if (i <= levelWave)
                {

                    if (levelWave == 10)
                    {
                        checkWeaponAndCraft.MyMoney.text = (int.Parse(checkWeaponAndCraft.MyMoney.text) + 30000).ToString();
                    }
                    PlayerPrefs.SetString("VeryHardDifficulty", levelWave.ToString());
                    PlayerPrefs.Save();
                }
            }


        }

    }
}




