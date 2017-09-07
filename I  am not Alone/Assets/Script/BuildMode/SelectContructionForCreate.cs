using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class SelectContructionForCreate : MonoBehaviour
{

    SwitchMode switchMode;
    GameObject panelChooseConstruction;
    PoolingSystem pool;

    [HideInInspector]
    public GameObject itemCreate;
    [HideInInspector]
    public Text NameDinamicCraftItem;
    Transform player;
    [HideInInspector]
    public Transform counterText;
    private Toggle toggle;
    public Text level;
    // Use this for initialization
    void Start ()
    {
        counterText = transform.Find("Params").transform;

        switchMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        panelChooseConstruction = GameObject.Find("panelChooseConstruction");
        pool = PoolingSystem.Instance;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        toggle = GetComponent<Toggle>();
    }

    public void InterectiveChangeParamsOrDestroy ()
    {
        if (int.Parse(counterText.GetChild(0).GetComponent<Text>().text) <= 0)
        {

            Destroy(this.gameObject);
        }

    }
    public void CheckOFToggle ()
    {
        itemCreate = null;
        toggle.isOn = false;
    }

    public void SelectByElement (Toggle tog)
    {
        if (tog.isOn)
        {
            if (itemCreate == null)
            {
                itemCreate = pool.InstantiateAPS(NameDinamicCraftItem.text, new Vector3(player.position.x + 0.6f, player.position.y, player.position.z), player.rotation);
                switchMode.CraftItemBuildNowDinamic = itemCreate.GetComponent<CraftItem>();
                if (!itemCreate.GetComponent<CraftItem>().Interactive)
                {
                    itemCreate.GetComponent<CraftItem>().level = int.Parse(level.text); 
                }
                itemCreate.GetComponent<CraftItem>().Item = gameObject.GetComponent<SelectContructionForCreate>();
                itemCreate.GetComponent<Indicator>().IndicatorOffscreen(true, 0);
            }

        }
        else
        {
            if (itemCreate != null)
            {
                itemCreate.DestroyAPS();
                itemCreate.GetComponent<Indicator>().IndicatorOffscreen(false, 0);
                switchMode.craft.Remove(switchMode.craft.Find(obj => obj.ItemCraft.name == itemCreate.name));
                itemCreate = null;
            }
        }

    }
}
