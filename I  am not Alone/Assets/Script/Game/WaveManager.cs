using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC.LSky;
using UnityEngine.UI;
using UnityEngine.Playables;

[System.Serializable]
public class WaveParams
{


    public float InstantiationTimer;
    public float TimerCreate;
    float CountInstantiation;
    //   public int CreatePrefabinOneInterval;

    public WaveParams (float instancetimer)
    {
        InstantiationTimer = instancetimer;
        TimerCreate = 0;

        //  CreatePrefabinOneInterval = createrefabInterval;

    }

}


public class WaveManager : MonoBehaviour
{




    [Space(5)]
    public GhostWaveCreater ghostCreater;
    private List<SpawnerZombie> spawerZombie = new List<SpawnerZombie>();
    private List<WaveParams> waveParams = new List<WaveParams>();
    private List<WaveParams> waveParamsHard = new List<WaveParams>();
    [Space(5)]
    float NightTime;
    public LSkyTOD _lskyTod;
    public LSky _lsky;

    public int levelWave;
    bool waveLevelUp;
    bool day;
    bool night;
    bool startWave;
    // public bool harfMode = false;
    SwitchMode switchMode;
    [HideInInspector]
    public List<GameObject> lightAllScene = new List<GameObject>();
    public CheckInWeaponAndCraft _weaponcraft;

    public Text GhostCounter;
    public Text GhoustWave;
    PlayableDirector m_playebleDirector;
    int wavenumberForText;
    AudioSource source;
    public GameObject Winner;
    public GameObject dualjoy;
    // Use this for initialization
    private void Start ()
    {
        if (ghostCreater)
        {
            init();
        }
    }

    public void init ()
    {
        GameObject[] light = GameObject.FindGameObjectsWithTag("LightInscene");
        for (int i = 0; i < light.Length; i++)
        {
            lightAllScene.Add(light[i]);
        }
        m_playebleDirector = GhoustWave.GetComponent<PlayableDirector>();

        switchMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();

        for (int i = 0; i < transform.childCount; i++)
        {

            spawerZombie.Add(transform.GetChild(i).GetComponent<SpawnerZombie>());
        }
        source = GetComponent<AudioSource>();
        _lskyTod.timeline = 6.0f;
        levelWave = 0;
        waveLevelUp = false;
        day = true;
        startWave = true;
        SkyParamsWave();

        switchMode.BuildMOdeMenu(true);

    }

    public void SkyParamsWave ()
    {


        if (_lsky.IsNight)
        {

            if (night)
            {

                day = true;
                waveLevelUp = true;
                _lskyTod.dayInSeconds = ghostCreater.wave[levelWave].Night * 2;
                night = false;
                startWave = true;
                switchMode.BuildMOdeMenu(false);
                switchMode.CraftItemBuildNowDinamic = null;
                GhoustWave.gameObject.SetActive(true);
                wavenumberForText = levelWave + 1;
                GhoustWave.text = "WAVE " + wavenumberForText;
                m_playebleDirector.Play();
                source.Play();
                for (int i = 0; i < lightAllScene.Count; i++)
                {
                    if (lightAllScene[i].GetComponent<Light>())
                    {
                        lightAllScene[i].GetComponent<Light>().enabled = true;
                    }

                }
                for (int i = 0; i < ghostCreater.wave[levelWave].countZombie.Count; i++)
                {
                    GhostCounter.text = (int.Parse(GhostCounter.text) + ghostCreater.wave[levelWave].countZombie[i]).ToString();

                }
            }

        }


        if (_lsky.IsDay)
        {
            if (day)
            {


                for (var i = lightAllScene.Count - 1; i > -1; i--)
                {
                    if (lightAllScene[i] == null)
                        lightAllScene.RemoveAt(i);
                }
                for (int i = 0; i < lightAllScene.Count; i++)
                {
                    if (lightAllScene[i].GetComponent<Light>())
                    {
                        lightAllScene[i].GetComponent<Light>().enabled = false;
                    }

                }
                if (waveLevelUp)
                {


                    levelWave++;
                    ghostCreater.UpdateLevelWaverPrefs(levelWave);


                    GhostCounter.text = "0";

                    switchMode.BuildMOdeMenu(true);
                    waveLevelUp = false;
                    _weaponcraft.PlusAndUpdateMoneyPlayer();
                }
                night = true;
                _lskyTod.dayInSeconds = ghostCreater.wave[levelWave].Day * 2;
                day = false;
            }
            if (_lskyTod.CurrentHour == 17.00)
            {
                if (startWave)
                {


                    waveParams.Clear();
                    GenerateParamsWave(levelWave);
                    startWave = false;

                }
            }
        }



    }

    // Update is called once per frame
    void Update ()
    {
        if (levelWave >= ghostCreater.wave.Count)
        {

            dualjoy.SetActive(false);
            Winner.SetActive(true);
        }
        else
        {
            SkyParamsWave();


            CreatePrefab(levelWave);


        }
        //    HardModeCreatePreafab(wave.Count - 1);

    }

    void GenerateParamsWave (int w)
    {

        {

            int ghosts = ghostCreater.wave[w].ZombiePref.Count;
            for (int i = 0; i < ghosts; i++)
            {


                waveParams.Add(new WaveParams((ghostCreater.wave[w].Night / 3) / ghostCreater.wave[w].countZombie[i]));


            }




            NightTime = ghostCreater.wave[w].Night / 3;


        }
    }
    //void GenerateParamsWaveForHardMode (int w)
    //{

    //    {

    //        for (int i = 0; i < wave[w].ZombiePref.Count; i++)
    //        {


    //            waveParamsHard.Add(new WaveParams((wave[w].Night - 60) / wave[w].countZombie[i]));


    //        }







    //    }
    //}
    void CreatePrefab (int w)
    {
        if (_lsky.IsNight)
        {
            NightTime -= Time.deltaTime;
            if (NightTime >= 0)
            {
                for (int i = 0; i < waveParams.Count; i++)
                {

                    waveParams[i].TimerCreate -= Time.deltaTime;

                    if (waveParams[i].TimerCreate <= 0)
                    {
                        int li = Random.Range(0, spawerZombie.Count);


                        spawerZombie[li].CreateZombie(ghostCreater.wave[w].ZombiePref[i].gameObject, _lsky);

                        waveParams[i].TimerCreate = waveParams[i].InstantiationTimer;

                    }
                    else
                    {
                        continue;
                    }



                }
            }
        }

    }
    public void StartWave ()
    {
        _lskyTod.dayInSeconds = 4;
    }
    //void HardModeCreatePreafab (int w)
    //{
    //    if (harfMode)
    //    {
    //        if (_lsky.IsNight)
    //        {


    //            for (int i = 0; i < waveParamsHard.Count; i++)
    //            {

    //                waveParamsHard[i].TimerCreate -= Time.deltaTime;

    //                if (waveParamsHard[i].TimerCreate <= 0)
    //                {
    //                    int li = Random.Range(0, spawerZombie.Count);

    //                    spawerZombie[li].CreateZombie(wave[w].ZombiePref[i].gameObject, _lsky);

    //                    waveParamsHard[i].TimerCreate = waveParamsHard[i].InstantiationTimer;

    //                }
    //                else
    //                {
    //                    continue;
    //                }



    //            }
    //        }
    //    }

    //}

}

