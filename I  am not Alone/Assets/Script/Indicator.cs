using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{

    public bool staticObject = true;

    public Image TargetSprite;





    Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) * 0.5f;

    public Image _targetSpriteOfPool;
    //  public Image BlowUpYes;

    //   public Image _blowUpYes;
    private Vector3 offScreen = new Vector3(-2000, -2000, -1200);
    GameObject player;
    private Rect centerRect;
    private Canvas canvas;
    Vector3 screenpos;
    CraftItem item;
    SwitchMode switchMode;
    // Use this for initialization
    void Awake ()
    {
        centerRect.width = Screen.width / 2;
        centerRect.height = Screen.height / 2;
        canvas = GameObject.Find("HUDCanvas").GetComponent<Canvas>();
        item = GetComponent<CraftItem>();
        switchMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        _targetSpriteOfPool = Instantiate(TargetSprite, offScreen, Quaternion.Euler(new Vector3(0, 0, 0))) as Image;
        //if (item.BlowUpEffect || item.hisEffect)
        //{
        //    _blowUpYes = Instantiate(BlowUpYes, offScreen, Quaternion.Euler(new Vector3(0, 0, 0))) as Image;
        //    _blowUpYes.rectTransform.parent = canvas.transform.GetChild(0);


        //}
        _targetSpriteOfPool.rectTransform.parent = canvas.transform.GetChild(0);





        centerRect.position = new Vector2((screenCenter.x - centerRect.width / 2), screenCenter.y - centerRect.height / 2);

        //if (staticObject)
        //{
        for (int i = 0; i < _targetSpriteOfPool.transform.Find("Counter").childCount; i++)
        {
            _targetSpriteOfPool.transform.Find("Counter").GetChild(i).GetComponent<Text>().text = item.CountWoodForCreate[i].CountMaterial.ToString();

        }
        //for (int i = 0; i < _targetSpriteOfPool.transform.Find("Names").childCount; i++)
        //{
        //    _targetSpriteOfPool.transform.Find("Names").GetChild(i).GetComponent<Text>().text = item.CountWoodForCreate[i].NameMaterial.ToString();

        //}
        //}
        //else
        //{
        //    if (item.BlowUpEffect)
        //    {
        //        Button btn = _blowUpYes.GetComponent<Button>();
        //        btn.onClick.AddListener(item.BlowUp);
        //    }

        //    else if (item.hisEffect)
        //    {
        //        Button btn1 = _blowUpYes.GetComponent<Button>();
        //        btn1.onClick.AddListener(item.His);
        //    }
        //    Button btnswitchMode = _targetSpriteOfPool.GetComponent<Button>();
        //    btnswitchMode.onClick.AddListener(switchMode.ButtonYes);

        //}


    }

    // Update is called once per frame
    void Update ()
    {


        screenpos = Camera.main.WorldToScreenPoint(item.pointForMenu.position);

        PlaceIndicators();



    }





    void PlaceIndicators ()
    {




        //if onscreen
        if (screenpos.z > 0 && screenpos.x < Screen.width && screenpos.x > 0 && screenpos.y < Screen.height && screenpos.y > 0)
        {


            //if in the center rect
            if (centerRect.Contains(screenpos))
            {


                _targetSpriteOfPool.rectTransform.position = screenpos;

                //if (item.BlowUpEffect || item.hisEffect)
                //{
                //    _blowUpYes.rectTransform.position = new Vector3(screenpos.x, screenpos.y, screenpos.z);
                //}

            }
            else
            {

                _targetSpriteOfPool.rectTransform.position = offScreen;
                //if (item.BlowUpEffect || item.hisEffect)
                //{
                //    _blowUpYes.rectTransform.position = offScreen;
                //}
            }
        }
        else
        {

            _targetSpriteOfPool.rectTransform.position = offScreen;
            //if (item.BlowUpEffect || item.hisEffect)
            //{
            //    _blowUpYes.rectTransform.position = offScreen;
            //}
        }




    }
    public void IndicatorOffScreen ()
    {
        _targetSpriteOfPool.rectTransform.position = offScreen;
        // _targetSpriteOfPool.gameObject.SetActive(visible);
        //if (BlowUpYes != null)
        //{
        //     _blowUpYes.rectTransform.position = offScreen;
        //  //  _blowUpYes.gameObject.SetActive(visible);
        //}
    }
    public void IndicatorSetActive (bool visible, int i)
    {


        if (i == 0)
        {
            //      _targetSpriteOfPool.rectTransform.position = offScreen;
            _targetSpriteOfPool.gameObject.SetActive(visible);
            //if (BlowUpYes != null)
            //{
            //    //   _blowUpYes.rectTransform.position = offScreen;
            //    _blowUpYes.gameObject.SetActive(visible);
            //}
        }
        else if (i == 1)
        {
            _targetSpriteOfPool.gameObject.SetActive(visible);
        }
        else if (i == 2)
        {
            //if (BlowUpYes != null)
            //{
            //    //   _blowUpYes.rectTransform.position = offScreen;
            //    _blowUpYes.gameObject.SetActive(visible);
            //}
        }
    }



}


