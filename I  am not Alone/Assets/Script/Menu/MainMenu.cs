using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    // Use this for initialization
    private void Start ()
    {
        db = GetComponent<DbGame>();
        db.OpenDB("DBGame.db");
        db.GetMoney();
    
    }


    public void ButtonPlay ()
    {


        loadPanel.SetActive(true);
        StartCoroutine(Load(sceneName));

    }



    public void CheckInPrice (Text price)
    {
        if (int.Parse(myMoney.text) > int.Parse(price.text))
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

