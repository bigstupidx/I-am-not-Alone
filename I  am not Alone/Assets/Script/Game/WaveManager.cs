using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AC.LSky;

[System.Serializable]
public class Wave
{
    public float Day;
    public float Night;
    public float[] countZombie;
    public GameObject[] ZombiePref;

}

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
    public List<Wave> wave = new List<Wave>();
    private List<SpawnerZombie> spawerZombie = new List<SpawnerZombie>();
    private List<WaveParams> waveParams = new List<WaveParams>();
    [Space(5)]
    float NightTime;
    public LSkyTOD _lskyTod;
    public LSky _lsky;

    public int levelWave;
    bool waveLevelUp;
    bool day;
    bool night;
    bool startWave;
    // Use this for initialization
    void Start ()
    {

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
        GenerateParamsWave(levelWave);
    }

    public void SkyParamsWave ()
    {


        if (_lsky.IsNight)
        {

            if (night)
            {
                day = true;
                waveLevelUp = true;
                _lskyTod.dayInSeconds = wave[levelWave].Night * 2;
                night = false;
                startWave = true;
            }
          
        }


        if (_lsky.IsDay)
        {
            if (day)
            {
                if (waveLevelUp)
                {
                    levelWave++;
                    waveLevelUp = false;
                }
                night = true;
                _lskyTod.dayInSeconds = wave[levelWave].Day * 2;
                day = false;
            }
            if (_lskyTod.CurrentHour == 17.00)
            {
                if (startWave)
                {
                    Debug.Log(_lskyTod.CurrentHour);
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
    }

    void GenerateParamsWave (int w)
    {

        {

            for (int i = 0; i < wave[w].ZombiePref.Length; i++)
            {


                waveParams.Add(new WaveParams((wave[w].Night - 20) / wave[w].countZombie[i]));


            }
            Debug.Log(w);

            NightTime = wave[w].Night - 20f;




        }
    }
    void CreatePrefab (int w)
    {
        if (_lsky.IsNight)
        {
            Debug.Log(w);
            NightTime -= Time.deltaTime;
            //  if (timersecond <= NightTime)
            if (NightTime > 0)
            {
                for (int i = 0; i < waveParams.Count; i++)
                {

                    waveParams[i].TimerCreate -= Time.deltaTime;

                    if (waveParams[i].TimerCreate <= 0)
                    {
                        int li = Random.Range(0, spawerZombie.Count);

                        spawerZombie[li].CreateZombie(wave[w].ZombiePref[i].gameObject);

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
}

