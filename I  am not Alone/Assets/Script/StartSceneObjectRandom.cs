using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneObjectRandom : MonoBehaviour
{
    public Transform startRandom;
    public Transform inWave;
    public WaveManager _waveManager;
    int randomObjectActive;
    int randomObjectActiveForWave;
    public bool RandomSituationStart;
    int LevelwaveStart;
    bool startSituation;
    float timerToStart;
    float timeToHunt;
    // Use this for initialization
    void Start ()
    {

        randomObjectActive = Random.Range(0, startRandom.childCount);

        LevelwaveStart = Random.Range(0, _waveManager.wave.Count);
        randomObjectActiveForWave = Random.Range(0, inWave.childCount);
        for (int i = 0; i < startRandom.childCount; i++)
        {
            if (i != randomObjectActive)
            {
                Destroy(startRandom.GetChild(i).gameObject);
            }
        }



    }


    private void Update ()
    {
        if (startSituation)
        {
            timerToStart += Time.deltaTime;

            if (timerToStart >= timeToHunt)
            {
                int l = Random.Range(0, 2);
                if (l == 0)
                {
                    for (int i = 0; i < inWave.childCount; i++)
                    {

                        inWave.GetChild(i).gameObject.SetActive(true);

                        timerToStart = 0;
                        startSituation = false;
                        return;
                    }
                }
                else
                {
                    timerToStart = 0;
                    startSituation = false;
                    return;
                }
            }

        }
    }
    public void StartRandomSituation (int level)
    {
        if (level == LevelwaveStart)
        {
            startSituation = true;
            timeToHunt = _waveManager.wave[level].Night / 2 - Random.Range(0, 15);


        }




    }

}
