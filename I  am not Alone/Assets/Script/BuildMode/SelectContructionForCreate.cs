using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class SelectContructionForCreate : MonoBehaviour
{

    SwitchMode switchMode;

    PoolingSystem pool;

    [HideInInspector]
    public GameObject itemCreate;

    public Text NameDinamicCraftItem;
    Transform instanceCreatePlayer;

    public Transform counterText;
    private Toggle toggle;
    public Text level;

    // Use this for initialization
    void Start ()
    {
        pool = PoolingSystem.Instance;
        counterText = transform.Find("Params").transform;

        switchMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();


        instanceCreatePlayer = GameObject.FindGameObjectWithTag("Player").transform.Find("instanceCreate");

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
                itemCreate = pool.InstantiateAPS(NameDinamicCraftItem.text, new Vector3(instanceCreatePlayer.transform.position.x, instanceCreatePlayer.transform.position.y + 0.5f, instanceCreatePlayer.transform.position.z), instanceCreatePlayer.rotation);

                CraftItem craft = itemCreate.GetComponent<CraftItem>();

                switchMode.CraftItemBuildNowDinamic = itemCreate.GetComponent<CraftItem>();
                switchMode.ButtonCraft.SetActive(true);
                if (!craft.Interactive)
                {
                    craft.level = int.Parse(level.text);
                }
                craft.Item = gameObject.GetComponent<SelectContructionForCreate>();
                //     itemCreate.GetComponent<Indicator>().IndicatorSetActive(true, 0);
            }

        }
        else
        {
            if (itemCreate != null)
            {
                switchMode.ButtonCraft.SetActive(false);
                switchMode.CraftItemBuildNowDinamic = null;
                itemCreate.DestroyAPS();
                //    itemCreate.GetComponent<Indicator>().IndicatorSetActive(false, 0);
                //   switchMode.craft.Remove(switchMode.craft.Find(obj => obj.ItemCraft.name == itemCreate.name));
                itemCreate = null;
            }
        }

    }
}
