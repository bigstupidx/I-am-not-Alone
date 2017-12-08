using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneObjectRandom : MonoBehaviour
{
    public Transform startRandom;
    public Transform inWave;
   
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

     //   randomObjectActive = Random.Range(0, startRandom.childCount);

     //   LevelwaveStart = Random.Range(0, _waveManager.wave.Count);
     ////   randomObjectActiveForWave = Random.Range(0, inWave.childCount);
     //   for (int i = 0; i < startRandom.childCount; i++)
     //   {
     //       if (i != randomObjectActive)
     //       {
     //           Destroy(startRandom.GetChild(i).gameObject);
     //       }
     //   }



    }




}
