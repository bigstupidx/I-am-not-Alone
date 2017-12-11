using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
[System.Serializable]
public class CraftParams
{

    public GameObject ItemCraft;
    public GameObject PanelUIForCraft;

    public int Floor;
    public CraftParams (GameObject item, GameObject panel, int floor)
    {
        this.ItemCraft = item;
        this.PanelUIForCraft = panel;
        Floor = floor;
    }


}





public class SwitchMode : MonoBehaviour
{

    public GameObject BuildMode;

    public GameObject ButtonCraft;

    public Text CounterZombie;

    [Space(15)]
    [Header("panelGoods")]
    public List<Text> panelGoods = new List<Text>();






    [Space(15)]
    public List<GameObject> interActivePrefab = new List<GameObject>();
    [Space(5)]
    // Use this for initialization
    public List<CraftParams> craft = new List<CraftParams>();

    public CraftItem CraftItemBuildNowStatic;


    public CraftItem CraftItemBuildNowDinamic;



    public BetweenFloor betweenFloor;

    AudioSource source;
    int wood = 0;
    int metal = 0;
    int glass = 0;
    int electric = 0;
    bool l;
    PlayableDirector m_director;
    private void Start ()
    {
        source = GetComponent<AudioSource>();
        ButtonCraft.SetActive(false);
        l = true;
        m_director = ButtonCraft.GetComponent<PlayableDirector>();
    }
    // Update is called once per frame

    public void BuildMOdeMenu (bool rog)
    {
        if (rog)
        {

            BuildMode.SetActive(true);
            CounterZombie.gameObject.SetActive(false);
            //Hand.SetActive(false);
            //HandWeapon.SetActive(false);
        }
        else
        {


            BuildMode.SetActive(false);
            CounterZombie.gameObject.SetActive(true);
            //Hand.SetActive(true);
            //HandWeapon.SetActive(true);
        }
        l = rog;
        CheckInBuiltWalls(rog);
    }



    public void CheckInBuiltWalls (bool visible)
    {

        if (l)
        {
            for (var i = craft.Count - 1; i > -1; i--)
            {
                if (craft[i].ItemCraft == null)
                    craft.RemoveAt(i);
            }
            for (int i = 0; i < craft.Count; i++)
            {
                if (craft[i].Floor == 2 & betweenFloor.FloorCraft)
                {
                    craft[i].ItemCraft.SetActive(visible);
                    craft[i].PanelUIForCraft.SetActive(visible);
                }
                else
                {
                    if (craft[i].Floor == 1 & betweenFloor.FloorCraft)
                    {
                        craft[i].ItemCraft.SetActive(false);
                        craft[i].PanelUIForCraft.SetActive(false);
                    }

                }

                if (craft[i].Floor == 1 & !betweenFloor.FloorCraft)
                {
                    craft[i].ItemCraft.SetActive(visible);
                    craft[i].PanelUIForCraft.SetActive(visible);
                }
                else
                {
                    if (craft[i].Floor == 2 & !betweenFloor.FloorCraft)
                    {
                        craft[i].ItemCraft.SetActive(false);
                        craft[i].PanelUIForCraft.SetActive(false);
                    }
                }








                if (CraftItemBuildNowDinamic != null & !visible)
                {
                    CraftItemBuildNowDinamic.Item.CheckOFToggle();
                    CraftItemBuildNowDinamic.gameObject.DestroyAPS();
                    CraftItemBuildNowDinamic.GetComponent<Indicator>().IndicatorSetActive(false, 0);

                }
            }
        }
        else
        {
            for (var i = craft.Count - 1; i > -1; i--)
            {
                if (craft[i].ItemCraft == null)
                    craft.RemoveAt(i);
            }
            for (int i = 0; i < craft.Count; i++)
            {

                craft[i].ItemCraft.SetActive(false);
                craft[i].PanelUIForCraft.SetActive(false);


            }









            if (CraftItemBuildNowDinamic != null & !visible)
            {
                CraftItemBuildNowDinamic.Item.CheckOFToggle();
                CraftItemBuildNowDinamic.gameObject.DestroyAPS();
                //    CraftItemBuildNowDinamic.GetComponent<Indicator>().IndicatorSetActive(false, 0);

            }
        }



    }




    public void ButtonCraftItemNow ()
    {
        if (CraftItemBuildNowStatic)
        {

            CheckInpurChasingPower(CraftItemBuildNowStatic.CountWoodForCreate, CraftItemBuildNowStatic);
        }
        else
        {

            CheckInpurChasingPower(CraftItemBuildNowDinamic.CountWoodForCreate, CraftItemBuildNowDinamic);

        }


        source.Play();
    }


    public void ButtonYes ()
    {

        source.Play();
    }


    void CheckInpurChasingPower (List<ItemForbuild> _itemForbuild, CraftItem c)
    {
        wood = 0;
        metal = 0;
        glass = 0;
        electric = 0;






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
                    m_director.Play();
                    return;
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
                    m_director.Play();
                    return;
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
                    m_director.Play();
                    return;
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
                    m_director.Play();
                    return;
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
                    m_director.Play();
                    return;
                }

            }


        }

        c.BuildContruction(true);


    }
    public void ChangeMaterial ()
    {
        panelGoods[0].text = (int.Parse(panelGoods[0].text) - wood).ToString();
        panelGoods[1].text = (int.Parse(panelGoods[1].text) - metal).ToString();
        panelGoods[2].text = (int.Parse(panelGoods[2].text) - glass).ToString();
        panelGoods[3].text = (int.Parse(panelGoods[3].text) - electric).ToString();


        wood = 0;
        metal = 0;
        glass = 0;
        electric = 0;
    }
}