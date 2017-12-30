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
    public float MoneyAd = 1000;
    public float MoneyShare = 1500;
    public float MiddleLevelWeapon;
    SaveData save;
    public List<int> MidLevel = new List<int>();
    CheckInWeaponAndCraft checkInWeaponAndCraft;
    public Text aDText;
    public Text shareText;
    public Button PlayButton;
    // Use this for initialization
    private void Start ()
    {
        checkInWeaponAndCraft = GetComponent<CheckInWeaponAndCraft>();
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
        save.GetInventoryForMenu();
        coinPlayeble = myMoney.GetComponent<PlayableDirector>();



    }


    public void UpdateMoneyADsAndShare ()
    {
        MiddleLevelWeapon = 0;
        for (int i = 0; i < MidLevel.Count; i++)
        {

            MiddleLevelWeapon += MidLevel[i];
        }
        MiddleLevelWeapon = MiddleLevelWeapon / MidLevel.Count;

        int a = (int)System.Math.Round(MiddleLevelWeapon);

        if (MidLevel.Count == 10)
        {

            if (a != 0)
            {
                MoneyAd *= a;
                MoneyShare *= a;

                aDText.text = "+ " + MoneyAd.ToString();
                shareText.text = "+ " + MoneyShare.ToString();
            }
        }
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
        FB.ShareLink(contentTitle: "Plinth",
            contentURL: new System.Uri("http://n3k.ca"),
            contentDescription: "Hello  this is my first Game", callback: OnShare);
        
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
                myMoney.text = (int.Parse(myMoney.text) + MoneyAd).ToString();
                save.UpdateMoney(myMoney.text);
                coinPlayeble.Play();
                break;
            case ShowResult.Finished:
                Debug.Log("player Gains + gems");
                ;
                myMoney.text = (int.Parse(myMoney.text) + MoneyAd).ToString();
                save.UpdateMoney(myMoney.text);
                coinPlayeble.Play();
                break;
            default:
                break;
        }

    }
}



