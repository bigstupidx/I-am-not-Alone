using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

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
    public Button buttonAds;




    // Use this for initialization
    private void Start ()
    {

        checkInWeaponAndCraft = GetComponent<CheckInWeaponAndCraft>();
        save = GetComponent<SaveData>();
   
        //  db = GetComponent<DbGame>();
        //   db.OpenDB("DBGame.db");
        //  db.GetMoney();
        save.GetMoney();
        save.GetInventoryForMenu();
        coinPlayeble = myMoney.GetComponent<PlayableDirector>();

        if (buttonAds)
        {
            StartCoroutine(Repath());
        }


    }

    private IEnumerator Repath ()
    {
        while (true)
        {
    

            yield return new WaitForSeconds(.5f);
        }
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
            loadPanel.transform.parent.gameObject.SetActive(true);
            loadPanel.SetActive(true);
            StartCoroutine(Load(sceneName));

        }


    }

    public void Share ()
    {

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

}



