using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectContructionForCreate : MonoBehaviour {
    SwitchMode switchMode;
    GameObject panelChooseConstruction;
    PoolingSystem pool;
    GameObject itemCreate;
    Transform player;
    // Use this for initialization
    void Start () {
        switchMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        panelChooseConstruction = GameObject.Find("panelChooseConstruction");
        pool = PoolingSystem.Instance;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	


    public void SelectByElement(Toggle tog)
    {
        if (tog.isOn)
        {
            if (itemCreate == null)
            {
                itemCreate = pool.InstantiateAPS("WoodWall", new Vector3(player.position.x, player.position.y, player.position.z), player.rotation, switchMode.gameObject);
                switchMode.CraftItemBuildNowDinamic = itemCreate.GetComponent<CraftItem>();
            }

        }
        else
        {
            if (itemCreate != null)
            {
                itemCreate.DestroyAPS(); 
            }
        }

    }
}
