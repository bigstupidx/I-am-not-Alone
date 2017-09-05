using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{

    public bool staticObject = true;

    public Image TargetSprite;





    Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) * 0.5f;
    [HideInInspector]
    public Image _targetSpriteOfPool;
    public Image BlowUpYes;
    [HideInInspector]
    public Image _blowUpYes;
    private Vector3 offScreen = new Vector3(-100, -100, 0);
    GameObject player;
    private Rect centerRect;
    private Canvas canvas;
    Vector3 screenpos;
    CraftItem item;
    SwitchMode switchMode;
    // Use this for initialization
    void Awake ()
    {
        canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        item = GetComponent<CraftItem>();
        switchMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        _targetSpriteOfPool = Instantiate(TargetSprite, offScreen, Quaternion.Euler(new Vector3(0, 0, 0))) as Image;
        if (item.BlowUpEffect || item.hisEffect)
        {
            _blowUpYes = Instantiate(BlowUpYes, offScreen, Quaternion.Euler(new Vector3(0, 0, 0))) as Image;
            _blowUpYes.rectTransform.parent = canvas.transform;
            _blowUpYes.name = _blowUpYes + transform.name;
            _blowUpYes.enabled = true;
        }
        _targetSpriteOfPool.rectTransform.parent = canvas.transform;
        _targetSpriteOfPool.name = _targetSpriteOfPool + transform.name;

        centerRect.width = 1280;
        centerRect.height = 800;
        _targetSpriteOfPool.enabled = false;

        centerRect.position = new Vector2((screenCenter.x - centerRect.width / 2), screenCenter.y - centerRect.height / 2);

        if (staticObject)
        {
            for (int i = 0; i < _targetSpriteOfPool.transform.Find("Counter").childCount; i++)
            {
                _targetSpriteOfPool.transform.Find("Counter").GetChild(i).GetComponent<Text>().text = item.CountWoodForCreate[i].CountMaterial.ToString();

            }
            for (int i = 0; i < _targetSpriteOfPool.transform.Find("Names").childCount; i++)
            {
                _targetSpriteOfPool.transform.Find("Names").GetChild(i).GetComponent<Text>().text = item.CountWoodForCreate[i].NameMaterial.ToString();

            }
        }
        else
        {
            if (item.BlowUpEffect)
            {
                Button btn = _blowUpYes.GetComponent<Button>();
                btn.onClick.AddListener(item.BlowUp);
            }

            else if (item.hisEffect)
            {
                Button btn1 = _blowUpYes.GetComponent<Button>();
                btn1.onClick.AddListener(item.His);
            }
            Button btn2 = _targetSpriteOfPool.GetComponent<Button>();
            btn2.onClick.AddListener(switchMode.ButtonYes);
        }


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

                if (item.BlowUpEffect || item.hisEffect)
                {
                    _blowUpYes.rectTransform.position = new Vector3(screenpos.x, screenpos.y, screenpos.z);
                }

            }
            else
            {

                _targetSpriteOfPool.rectTransform.position = offScreen;
                if (item.BlowUpEffect || item.hisEffect)
                {
                    _blowUpYes.rectTransform.position = offScreen;
                }
            }
        }
        else
        {

            _targetSpriteOfPool.rectTransform.position = offScreen;
            if (item.BlowUpEffect || item.hisEffect)
            {
                _blowUpYes.rectTransform.position = offScreen;
            }
        }




    }

    public void IndicatorOffscreen ()
    {
        _targetSpriteOfPool.rectTransform.position = offScreen;
    }
}

