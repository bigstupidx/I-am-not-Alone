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
        //  Time.timeScale = 0;
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("1557198", false);


        }
        else
        {
            Debug.Log("platform is not Supported");
        }
    }
    public void ButtonMenu ()
    {


        Pause();

    }

    public void Pause ()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        Lowpass();

    }

    void Lowpass ()
    {
        //if (Time.timeScale == 0)
        //{
        //    paused.TransitionTo(.01f);
        //}

        //else

        //{
        //    unpaused.TransitionTo(.01f);
        //}
    }

    public void Quit ()
    {
        loadPanel.enabled = true;
        StartCoroutine(Load("Menu"));
        Time.timeScale = 0;
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("1557198", false);
            if (Advertisement.IsReady())
            {
                Advertisement.Show("video", new ShowOptions() { resultCallback = HandleadResult });

            }

        }
        else
        {
            Debug.Log("platform is not Supported");
        }


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
    private void HandleadResult (ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.Log("player failde  launch");
                StartCoroutine(Load("Menu"));
                Time.timeScale = 0;
                break;
            case ShowResult.Skipped:
                Debug.Log("player did not fully watch the ad");
                StartCoroutine(Load("Menu"));
                Time.timeScale = 0;
                break;
            case ShowResult.Finished:
                Debug.Log("player Gains +5 gems");
                StartCoroutine(Load("Menu"));
                Time.timeScale = 0;
                break;
            default:
                break;
        }

    }
}
