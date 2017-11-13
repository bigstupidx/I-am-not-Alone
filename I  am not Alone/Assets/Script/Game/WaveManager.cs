using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC.LSky;
using UnityEngine.UI;
using UnityEngine.Playables;
[System.Serializable]
public class Wave
{
    public float Day;
    public float Night;
    public List<int> countZombie = new List<int>();
    public List<GameObject> ZombiePref = new List<GameObject>();

}
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
    [HideInInspector]
    public List<Wave> wave = new List<Wave>();
    [Space(5)]

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
    public bool harfMode = false;
    SwitchMode switchMode;
    [HideInInspector]
    public List<GameObject> lightAllScene = new List<GameObject>();
    public CheckInWeaponAndCraft _weaponcraft;
    public StartSceneObjectRandom _startObject;
    public Text GhostCounter;
    public Text GhoustWave;
    PlayableDirector m_playebleDirector;
    // Use this for initialization
    void Start ()
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

        _lskyTod.timeline = 6.0f;
        levelWave = 0;
        waveLevelUp = false;
        day = true;
        startWave = true;
        SkyParamsWave();

        switchMode.BuildMOdeMenu(true);
        GenerateParamsWaveForHardMode(wave.Count - 1);
    }

    public void SkyParamsWave ()
    {


        if (_lsky.IsNight)
        {

            if (night)
            {
                //  _startObject.StartRandomSituation(levelWave);
                day = true;
                waveLevelUp = true;
                _lskyTod.dayInSeconds = wave[levelWave].Night * 2;
                night = false;
                startWave = true;
                switchMode.BuildMOdeMenu(false);
                GhoustWave.gameObject.SetActive(true);
                GhoustWave.text = "WAVE " + levelWave;
                m_playebleDirector.Play();
                for (int i = 0; i < lightAllScene.Count; i++)
                {
                    if (lightAllScene[i].GetComponent<Light>())
                    {
                        lightAllScene[i].GetComponent<Light>().enabled = true;
                    }
                    //else
                    //{
                    //    lightAllScene[i].GetComponent<Renderer>().enabled = true;
                    //}
                }
                for (int i = 0; i < wave[levelWave].countZombie.Count; i++)
                {
                    GhostCounter.text = (int.Parse(GhostCounter.text) + wave[levelWave].countZombie[i]).ToString();

                }
            }
            //if (GhostCounter.text == "0")
            //{
            //    wave[levelWave].Night = 2;
            //}
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
                    //else
                    //{
                    //lightAllScene[i].GetComponent<Renderer>().enabled = false;
                    //}
                }
                if (waveLevelUp)
                {

                    if (!harfMode)
                    {
                        levelWave++;
                    }
                    GhostCounter.text = "0";
                    if (levelWave > wave.Count - 1)
                    {

                        harfMode = true;
                        levelWave = Random.Range(0, wave.Count - 1);
                    }
                    switchMode.BuildMOdeMenu(true);
                    waveLevelUp = false;
                    _weaponcraft.PlusAndUpdateMoneyPlayer();
                }
                night = true;
                _lskyTod.dayInSeconds = wave[levelWave].Day * 2;
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

        SkyParamsWave();

        CreatePrefab(levelWave);
        HardModeCreatePreafab(wave.Count - 1);

    }

    void GenerateParamsWave (int w)
    {

        {

            for (int i = 0; i < wave[w].ZombiePref.Count; i++)
            {


                waveParams.Add(new WaveParams((wave[w].Night - 20) / wave[w].countZombie[i]));


            }




            NightTime = wave[w].Night - 20.0f;


        }
    }
    void GenerateParamsWaveForHardMode (int w)
    {

        {

            for (int i = 0; i < wave[w].ZombiePref.Count; i++)
            {


                waveParamsHard.Add(new WaveParams((wave[w].Night - 20) / wave[w].countZombie[i]));


            }







        }
    }
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

                        spawerZombie[li].CreateZombie(wave[w].ZombiePref[i].gameObject, _lsky);

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
    void HardModeCreatePreafab (int w)
    {
        if (harfMode)
        {
            if (_lsky.IsNight)
            {


                for (int i = 0; i < waveParamsHard.Count; i++)
                {

                    waveParamsHard[i].TimerCreate -= Time.deltaTime;

                    if (waveParamsHard[i].TimerCreate <= 0)
                    {
                        int li = Random.Range(0, spawerZombie.Count);

                        spawerZombie[li].CreateZombie(wave[w].ZombiePref[i].gameObject, _lsky);

                        waveParamsHard[i].TimerCreate = waveParamsHard[i].InstantiationTimer;

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
}

