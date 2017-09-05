using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class CraftParams {

    public GameObject ItemCraft;
    public GameObject PanelUIForCraft;

    public CraftParams(GameObject item, GameObject panel)
    {
        this.ItemCraft = item;
        this.PanelUIForCraft = panel;

    }


}





public class SwitchMode : MonoBehaviour
{

    public GameObject BuildMode;
    public GameObject PlayerMode;
    public GameObject ButtonCraft;
    public GameObject Hand;
    public GameObject HandWeapon;

    [Space(15)]
    [Header("panelGoods")]
    public Text CountWoods;
    public Text CountMetals;
    public Text CountGlasses;
    public Text CountElectrics;
        



    bool l;
    // Use this for initialization
    public List<CraftParams> craft = new List<CraftParams>();

    public CraftItem CraftItemBuildNowStatic;


    public CraftItem CraftItemBuildNowDinamic;
 
    private void Start ()
    {

        ButtonCraft.SetActive(false);
        l = false;
       
    }
    // Update is called once per frame
    void Update ()
    {

        if (Input.GetButtonDown("Build"))
        {
            l = !l;
            if (l)
            {

                BuildMode.SetActive(true);
                PlayerMode.SetActive(false);
                Hand.SetActive(false);
                HandWeapon.SetActive(false);
            }
            else
            {

                BuildMode.SetActive(false);
                PlayerMode.SetActive(true);
                Hand.SetActive(true);
                HandWeapon.SetActive(true);
            }
            CheckInBuiltWalls(l);
        }

    }

    public void BuildMOdeMenu (Toggle rog)
    {
        if (rog.isOn)
        {

            BuildMode.SetActive(true);
            PlayerMode.SetActive(false);
            Hand.SetActive(false);
            HandWeapon.SetActive(false);
        }
        else
        {

            BuildMode.SetActive(false);
            PlayerMode.SetActive(true);
            Hand.SetActive(true);
            HandWeapon.SetActive(true);
        }
        CheckInBuiltWalls(rog.isOn);
    }

   
    void CheckInBuiltWalls (bool visible)
    {

        for (int i = 0; i < craft.Count; i++)
        {
            craft[i].ItemCraft.SetActive(visible);
          craft[i].PanelUIForCraft.SetActive(visible);
            if(CraftItemBuildNowDinamic != null & !visible)
            {
                CraftItemBuildNowDinamic.gameObject.DestroyAPS();
                CraftItemBuildNowDinamic.GetComponent<Indicator>()._targetSpriteOfPool.gameObject.SetActive(false);
                CraftItemBuildNowDinamic.GetComponent<Indicator>()._blowUpYes.gameObject.SetActive(false);
            }
        }


    }


    public void ButtonCraftItemNow ()
    {
   
        CheckInpurChasingPower(CraftItemBuildNowStatic.CountWoodForCreate, CraftItemBuildNowStatic);
      
    
    }


    public void ButtonYes ()
    {
    
        CheckInpurChasingPower(CraftItemBuildNowDinamic.CountWoodForCreate, CraftItemBuildNowDinamic);
     
      
    }

    /// <summary>
    /// Добавить что бы отнимала 

    /// </summary>
    /// <param name="_itemForbuild"></param>
    /// <param name="c"></param>
    void CheckInpurChasingPower (List<ItemForbuild> _itemForbuild, CraftItem c)
    {
        int wood = 0;
        int metal = 0;
        int glass = 0;
        int electric = 0;
        int interactive = 0;





        for (int i = 0; i < _itemForbuild.Count; i++)
        {

            if (_itemForbuild[i].NameMaterial.Equals("Woods"))
            {
                if (_itemForbuild[i].CountMaterial <= int.Parse(CountWoods.text))
                {
                    wood = _itemForbuild[i].CountMaterial;
                }
                else
                {
                
                    return ;
                }



            }
            if (_itemForbuild[i].NameMaterial.Equals("Metals"))
            {
                if (_itemForbuild[i].CountMaterial <= int.Parse(CountMetals.text))
                {
                    metal = _itemForbuild[i].CountMaterial;
                }
                else
                {
                    return ;
                }

            }
            if (_itemForbuild[i].NameMaterial.Equals("Glasses"))
            {
                if (_itemForbuild[i].CountMaterial <= int.Parse(CountGlasses.text))
                {
                    glass = _itemForbuild[i].CountMaterial;
                }
                else
                {
                    return ;
                }

            }
            if (_itemForbuild[i].NameMaterial.Equals("Electrics"))
            {
                if (_itemForbuild[i].CountMaterial <= int.Parse(CountElectrics.text))
                {
                    electric = _itemForbuild[i].CountMaterial;
                }
                else
                {
                    return ;
                }

            }
            if (_itemForbuild[i].NameMaterial.Equals("Interactive"))
            {
                if (_itemForbuild[i].CountMaterial <= int.Parse(c.Item.counterText.GetChild(i).GetComponent<Text>().text))
                {
                    c.Item.counterText.GetChild(i).GetComponent<Text>().text = (int.Parse(c.Item.counterText.GetChild(i).GetComponent<Text>().text) - _itemForbuild[i].CountMaterial).ToString();
                }
                else
                {
                    return;
                }

            }


        }
        CountWoods.text = (int.Parse(CountWoods.text) - wood).ToString();
        CountMetals.text = (int.Parse(CountMetals.text) - metal).ToString();
        CountGlasses.text = (int.Parse(CountGlasses.text) - glass).ToString();
        CountElectrics.text = (int.Parse(CountElectrics.text) - electric).ToString();
        c.BuildContruction(true);
        wood = 0;
        metal = 0;
        glass = 0;
        electric = 0;


    }
}
