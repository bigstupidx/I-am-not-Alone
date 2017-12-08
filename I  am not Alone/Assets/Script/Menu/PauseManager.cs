using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class PauseManager : MonoBehaviour
{

    //public AudioMixerSnapshot paused;
    //public AudioMixerSnapshot unpaused;
    public Canvas loadPanel;
    AsyncOperation async;
    public Slider progressSlider;
    private void Awake ()
    {
        Time.timeScale = 0;
        //if (Advertisement.isSupported)
        //{
        //    Advertisement.Initialize("1557198", false);


        //}
        //else
        //{
        //    Debug.Log("platform is not Supported");
        //}
    }
    public void ButtonMenu ()
    {


        Pause();

    }
    public void HINTSOFF ()
    {
        Time.timeScale = 1;
    }

    public void Pause ()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;



    }

    public void Quit ()
    {
        loadPanel.enabled = true;
        StartCoroutine(Load("Menu"));
        PlayerPrefs.Save();
        Time.timeScale = 1;
        //if (Advertisement.isSupported)
        //{
        //    Advertisement.Initialize("1557198", false);
        //    if (Advertisement.IsReady())
        //    {
        //        Advertisement.Show("video", new ShowOptions() { resultCallback = HandleadResult });

        //    }

        //}
        //else
        //{
        //    Debug.Log("platform is not Supported");
        //}


    }
    IEnumerator Load (string i)
    {

        async = SceneManager.LoadSceneAsync(i);
        //async.allowSceneActivation = false;
        while (!async.isDone)
        {


            //progressSlider.value = async.progress;


            while (!async.isDone)
            {
                float progress = Mathf.Clamp01(async.progress / .9f);
                progressSlider.value = progress;
                //  if(progressSlider.value == 0.9f){
                // progressSlider.value = 1.0f;
                //  async.allowSceneActivation = true;
                //}
                yield return null;
            }
            //  

        }
    }
    //private void HandleadResult (ShowResult result)
    //{
    //    switch (result)
    //    {
    //        case ShowResult.Failed:
    //            Debug.Log("player failde  launch");
    //            StartCoroutine(Load("Menu"));
    //            Time.timeScale = 1;
    //            break;
    //        case ShowResult.Skipped:
    //            Debug.Log("player did not fully watch the ad");
    //            StartCoroutine(Load("Menu"));
    //            Time.timeScale = 1;
    //            break;
    //        case ShowResult.Finished:
    //            Debug.Log("player Gains +5 gems");
    //            StartCoroutine(Load("Menu"));
    //            Time.timeScale = 1;
    //            break;
    //        default:
    //            break;
    //    }

    //}
}
