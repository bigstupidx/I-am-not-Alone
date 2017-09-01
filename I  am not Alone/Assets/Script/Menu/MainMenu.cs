using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public string sceneName;
    public Text myMoney;

    [HideInInspector]
    bool byeScene;
    DbGame db;
    // Use this for initialization
    private void Start ()
    {
        db = GetComponent<DbGame>();
        db.OpenDB("DBGame.db");
        db.GetMoney();
    
    }


    public void ButtonPlay ()
    {


        SceneManager.LoadSceneAsync(sceneName);


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
        SceneManager.LoadSceneAsync(0);


    }
    public void ButtonShop ()
    {
        SceneManager.LoadSceneAsync(1);


    }
}

