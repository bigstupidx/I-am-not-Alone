using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;
using Facebook.Unity;
using UnityEngine.Advertisements;



public class MyMainMenu : MonoBehaviour
{

    public string sceneName;
    public Text myMoney;
    public GameObject loadPanel;
    [HideInInspector]
    bool byeScene;
    //  DbGame db;
    AsyncOperation async;
    public Slider progressSlider;
    PlayableDirector coinPlayeble;
    public int MoneyAd = 750;
    public int MoneyShare = 400;
    SaveData save;
    // Use this for initialization
    private void Start ()
    {
        save = GetComponent<SaveData>();
        if (!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            FB.ActivateApp();
        }
        //  db = GetComponent<DbGame>();
        //   db.OpenDB("DBGame.db");
        //  db.GetMoney();
        save.GetMoney();
        coinPlayeble = myMoney.GetComponent<PlayableDirector>();
    }


    public void ButtonPlay ()
    {



        if (!sceneName.Equals(""))
        {
            loadPanel.SetActive(true);
            StartCoroutine(Load(sceneName));

        }


    }

    public void Share ()
    {
        FB.ShareLink(contentTitle: "I am  not Alone", contentDescription: "Hello  this is my first Game", callback: OnShare);
    }
    private void OnShare (IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("ShareLink error: " + result.Error);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log(result.PostId);
        }
        else
        {
            myMoney.text = (int.Parse(myMoney.text) + MoneyShare).ToString();
            save.UpdateMoney(myMoney.text);
            // db.UpdateMoney(myMoney.text);
            //  coinPlayeble.Play();
            Debug.Log("share succeed");
        }
    }

    public void CheckInPrice (Text price)
    {

        try
        {
            if (int.Parse(myMoney.text) >= int.Parse(price.text))
            {
                myMoney.text = (int.Parse(myMoney.text) - int.Parse(price.text)).ToString();
                //    db.UpdateMoney(myMoney.text);
                save.UpdateMoney(myMoney.text);
                byeScene = true;
            }
            else { byeScene = false; return; }
        }
        catch (System.Exception)
        {
            byeScene = true;
        }


    }

    public void UnClockScene (Text sceneName)
    {
        //db = GetComponent<DbGame>();
        //db.OpenDB("DBGame.db");
        if (byeScene)
        {


            save.InsertDBSceneName(sceneName.text);
            // db.InsertDBSceneName(sceneName.text);

        }

    }
    public void ImageEnable (Transform locked)
    {
        if (byeScene)
        {

            locked.gameObject.SetActive(false);
            byeScene = false;
        }

    }


    public void SelectScene (Text _sceneName)
    {

        sceneName = _sceneName.text;

    }

    public void ButtonBackToMainMenu ()
    {

        loadPanel.SetActive(true);
        StartCoroutine(Load("Menu"));

    }
    public void ButtonShop ()
    {
        loadPanel.SetActive(true);
        StartCoroutine(Load("Shop"));


    }
    public void ShowAds ()
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
                break;
            case ShowResult.Skipped:
                Debug.Log("player did not fully watch the ad");
                break;
            case ShowResult.Finished:
                Debug.Log("player Gains +500 gems");
                myMoney.text = (int.Parse(myMoney.text) + MoneyAd).ToString();
                save.UpdateMoney(myMoney.text);
                coinPlayeble.Play();
                break;
            default:
                break;
        }

    }
}



