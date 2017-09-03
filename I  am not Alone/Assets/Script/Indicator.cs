using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{

    public Image TargetSprite;





    Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) * 0.5f;

    public Image _targetSpriteOfPool;

    private Vector3 offScreen = new Vector3(-100, -100, 0);
    GameObject player;
    private Rect centerRect;
    private Canvas canvas;
    Vector3 screenpos;
    CraftItem item;

    // Use this for initialization
    void Awake ()
    {
        canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        item = GetComponent<CraftItem>();
        _targetSpriteOfPool = Instantiate(TargetSprite, offScreen, Quaternion.Euler(new Vector3(0, 0, 0))) as Image;

        _targetSpriteOfPool.rectTransform.parent = canvas.transform;
        _targetSpriteOfPool.name = _targetSpriteOfPool + transform.name;
        
        centerRect.width = 1280;
        centerRect.height = 800;
        _targetSpriteOfPool.enabled = false;
        centerRect.position = new Vector2((screenCenter.x - centerRect.width / 2), screenCenter.y - centerRect.height / 2);
     
        for (int i = 0; i < _targetSpriteOfPool.transform.Find("Counter").childCount; i++)
        {
            _targetSpriteOfPool.transform.Find("Counter").GetChild(i).GetComponent<Text>().text = item.CountWoodForCreate[i].CountMaterial.ToString();

        }
        for (int i = 0; i < _targetSpriteOfPool.transform.Find("Names").childCount; i++)
        {
            _targetSpriteOfPool.transform.Find("Names").GetChild(i).GetComponent<Text>().text = item.CountWoodForCreate[i].NameMaterial.ToString();

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



            }
            else
            {

                _targetSpriteOfPool.rectTransform.position = offScreen;

            }
        }
        else
        {

            _targetSpriteOfPool.rectTransform.position = offScreen;

        }




    }

    public void IndicatorOffscreen ()
    {
        _targetSpriteOfPool.rectTransform.position = offScreen;
    }
}

