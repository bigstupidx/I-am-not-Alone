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
    public List<Text> panelGoods = new List<Text>();

        



    bool l;
    [Space(15)]
    public List<GameObject> interActivePrefab = new List<GameObject>();
    [Space(5)]
    // Use this for initialization
    public List<CraftParams> craft = new List<CraftParams>();

    public CraftItem CraftItemBuildNowStatic;


    public CraftItem CraftItemBuildNowDinamic;
    public Button buttonAction;
    public bool openOrClosedDoor = false;
    public Animator Door;
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
                CraftItemBuildNowDinamic.Item.CheckOFToggle();
                CraftItemBuildNowDinamic.gameObject.DestroyAPS();
                CraftItemBuildNowDinamic.GetComponent<Indicator>().IndicatorSetActive(false, 0);
           
            }
        }


    }


    public void DoorAnimator()
    {
        openOrClosedDoor =! openOrClosedDoor;
        if (Door != null)
        {
            Door.SetBool("openOrClosed", openOrClosedDoor);

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


    void CheckInpurChasingPower (List<ItemForbuild> _itemForbuild, CraftItem c)
    {
        int wood = 0;
        int metal = 0;
        int glass = 0;
        int electric = 0;
        





        for (int i = 0; i < _itemForbuild.Count; i++)
        {

            if (_itemForbuild[i].NameMaterial.Equals("Woods"))
            {
                if (_itemForbuild[i].CountMaterial <= int.Parse(panelGoods[0].text))
                {
                    wood = _itemForbuild[i].CountMaterial;
                    continue;
                }
                else
                {
                
                    return ;
                }



            }
            if (_itemForbuild[i].NameMaterial.Equals("Metals"))
            {
                if (_itemForbuild[i].CountMaterial <= int.Parse(panelGoods[1].text))
                {
                    metal = _itemForbuild[i].CountMaterial;
                    continue;
                }
                else
                {
                    return ;
                }

            }
            if (_itemForbuild[i].NameMaterial.Equals("Glasses"))
            {
                if (_itemForbuild[i].CountMaterial <= int.Parse(panelGoods[2].text))
                {
                    glass = _itemForbuild[i].CountMaterial;
                    continue;
                }
                else
                {
                    return ;
                }

            }
            if (_itemForbuild[i].NameMaterial.Equals("Electrics"))
            {
                if (_itemForbuild[i].CountMaterial <= int.Parse(panelGoods[3].text))
                {
                    electric = _itemForbuild[i].CountMaterial;
                      continue;
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
        panelGoods[0].text = (int.Parse(panelGoods[0].text) - wood).ToString();
        panelGoods[1].text = (int.Parse(panelGoods[1].text) - metal).ToString();
        panelGoods[2].text = (int.Parse(panelGoods[2].text) - glass).ToString();
        panelGoods[3].text = (int.Parse(panelGoods[3].text) - electric).ToString();
        c.BuildContruction(true);
        wood = 0;
        metal = 0;
        glass = 0;
        electric = 0;


    }
}
