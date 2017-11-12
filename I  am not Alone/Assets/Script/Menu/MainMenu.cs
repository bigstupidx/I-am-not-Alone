using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Playables;
public class MainMenu : MonoBehaviour
{

    public string sceneName;
    public Text myMoney;
    public GameObject loadPanel;
    [HideInInspector]
    bool byeScene;
    DbGame db;
    AsyncOperation async;
    public Slider progressSlider;
    PlayableDirector coinPlayeble;
    // Use this for initialization
    private void Start ()
    {
        db = GetComponent<DbGame>();
        db.OpenDB("DBGame.db");
        db.GetMoney();
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



    public void CheckInPrice (Text price)
    {
        if (int.Parse(myMoney.text) >= int.Parse(price.text))
        {
            myMoney.text = (int.Parse(myMoney.text) - int.Parse(price.text)).ToString();
            db.UpdateMoney(myMoney.text);
            byeScene = true;
        }
        else { byeScene = false; return; }

    }

    public void UnClockScene (Text sceneName)
    {
        db = GetComponent<DbGame>();
        db.OpenDB("DBGame.db");
        if (byeScene)
        {



            db.InsertDBSceneName(sceneName.text);
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
                break;
            case ShowResult.Skipped:
                Debug.Log("player did not fully watch the ad");
                break;
            case ShowResult.Finished:
                Debug.Log("player Gains +500 gems");
                myMoney.text = (int.Parse(myMoney.text) + 500).ToString();
                db.UpdateMoney(myMoney.text);
                coinPlayeble.Play();
                break;
            default:
                break;
        }

    }
}

