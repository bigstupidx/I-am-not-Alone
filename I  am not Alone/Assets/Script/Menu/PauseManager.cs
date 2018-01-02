using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using GoogleMobileAds;
using GoogleMobileAds.Api;
public class PauseManager : MonoBehaviour
{

    //public AudioMixerSnapshot paused;
    //public AudioMixerSnapshot unpaused;
    public Canvas loadPanel;
    AsyncOperation async;
    public Slider progressSlider;
    private bool isPaused;
    public static int sundayCount;
    private RewardBasedVideoAd rewardBasedvideoAd;

    private void Awake ()
    {
        rewardBasedvideoAd = RewardBasedVideoAd.Instance;
        LoadRewardBasedAD();
        Time.timeScale = 0;
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
    public void HINTSOFF ()
    {
        Time.timeScale = 1;
    }
    void OnApplicationFocus (bool hasFocus)
    {
        Pause();
    }

    void OnApplicationPause (bool pauseStatus)
    {
        Pause();
    }
    public void Pause ()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;



    }




    public void Sunday ()
    {


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


    private void ShowGooldeAds ()
    {
        if (rewardBasedvideoAd.IsLoaded())
        {
            rewardBasedvideoAd.Show();
        }
        else
        {
            Debug.Log("not show  google ads");
        }
    }

    void LoadRewardBasedAD ()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-4636534738677317/1781030357";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            string appId = "unexpected_platform";
#endif


        rewardBasedvideoAd.LoadAd(new AdRequest.Builder().Build(), appId);
        // Initialize the Google Mobile Ads SDK.
        //   MobileAds.Initialize(appId);
    }

    public void Quit ()
    {
        loadPanel.enabled = true;
        StartCoroutine(Load("Menu"));
        PlayerPrefs.Save();
        Time.timeScale = 1;
        ShowGooldeAds();
        //int i = 0;
        //i = Random.Range(0, 2);
        //if (i == 1)
        //{

        //}
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
    private void HandleadResult (ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.Log("player failde  launch");

                Time.timeScale = 1;
                break;
            case ShowResult.Skipped:
                Debug.Log("player did not fully watch the ad");

                Time.timeScale = 1;
                break;
            case ShowResult.Finished:
                Debug.Log("player Gains +5 gems");

                Time.timeScale = 1;
                break;
            default:
                break;
        }

    }
}
