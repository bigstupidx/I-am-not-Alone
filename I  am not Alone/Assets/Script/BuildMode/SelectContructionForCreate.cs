using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class GuiParams {

  public   bool wood ;
    public bool metal ;
    public bool glass ;
    public bool electric;
    public bool interactive;
    public string CountElement;
    public GuiParams(bool w,bool m,bool g,bool e, bool i, string counter)
    {
        wood = w;
        metal = m;
        glass = g;
        electric = e;
        interactive = i;
        CountElement = counter;
    }

}



public class SelectContructionForCreate : MonoBehaviour
{
    public bool InteractiveGui;
    SwitchMode switchMode;
    GameObject panelChooseConstruction;
    PoolingSystem pool;
    public List<GuiParams> ElementParams = new List<GuiParams>();
    [HideInInspector]
    public GameObject itemCreate;
    [HideInInspector]
    public Text NameDinamicCraftItem;
    Transform player;
    [HideInInspector]
     public Transform counterText;

    // Use this for initialization
    void Start ()
    {
        counterText = transform.Find("Params").transform;
        for (int i = 0; i < counterText.childCount; i++)
        {
            counterText.GetChild(i).GetComponent<Text>().text = ElementParams[i].CountElement;
        }
        switchMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        panelChooseConstruction = GameObject.Find("panelChooseConstruction");
        pool = PoolingSystem.Instance;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    public void InterectiveChangeParamsOrDestroy ()
    {
        if (int.Parse(counterText.GetChild(0).GetComponent<Text>().text) == 0)
        {
            ElementParams.RemoveAt(0);
            Destroy(this.gameObject);
        }
    
    }


    public void SelectByElement (Toggle tog)
    {
        if (tog.isOn)
        {
            if (itemCreate == null)
            {
                itemCreate = pool.InstantiateAPS(NameDinamicCraftItem.text, new Vector3(player.position.x, player.position.y, player.position.z), player.rotation);
                switchMode.CraftItemBuildNowDinamic = itemCreate.GetComponent<CraftItem>();
                itemCreate.GetComponent<CraftItem>().Item = gameObject.GetComponent<SelectContructionForCreate>();
                itemCreate.GetComponent<Indicator>()._targetSpriteOfPool.gameObject.SetActive(true);
            }

        }
        else
        {
            if (itemCreate != null)
            {
                itemCreate.DestroyAPS();
                itemCreate.GetComponent<Indicator>()._targetSpriteOfPool.gameObject.SetActive(false);
                itemCreate.GetComponent<Indicator>()._blowUpYes.gameObject.SetActive(false);
                switchMode.craft.Remove(switchMode.craft.Find(obj => obj.ItemCraft.name == itemCreate.name));
                itemCreate = null;
            }
        }

    }
}
