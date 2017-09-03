using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemForbuild
{
    public string NameMaterial;
    public int CountMaterial;

    public ItemForbuild (string name, int count)
    {
        NameMaterial = name;
        CountMaterial = count;
    }
}






public class CraftItem : MonoBehaviour
{
    public bool BuildStatic = true;
    public List<ItemForbuild> CountWoodForCreate = new List<ItemForbuild>();
    public bool Built;
    public Material[] materials;
    [HideInInspector]
    public Transform pointForMenu;
    private SwitchMode buildMode;
    private Renderer rend;
    private GameObject MainCanvas;
    Indicator indicator;


    // Use this for initialization
    void Start ()
    {
        indicator = GetComponent<Indicator>();
        MainCanvas = GameObject.Find("MainCanvas");
        pointForMenu = transform.Find("PointForMenu").transform;
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();


        DefaultOptions();
    }


    public void DefaultOptions ()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            rend = transform.GetChild(0).GetChild(i).GetComponent<Renderer>();
            rend.enabled = true;

            rend.sharedMaterial = materials[0];
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;


        }
        buildMode.craft.Add(new CraftParams(this.gameObject, indicator._targetSpriteOfPool.gameObject));
        indicator.IndicatorOffscreen();
        if (BuildStatic)
        {
            gameObject.SetActive(false); 
        }
        Built = false;
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (!Built)
            {
                buildMode.CraftItemBuildNowStatic = this.gameObject.GetComponent<CraftItem>();
                buildMode.ButtonCraft.SetActive(true);
            }
        }
    }
    public void BuildContruction (bool b)
    {
        if (b)
        {
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                rend = transform.GetChild(0).GetChild(i).GetComponent<Renderer>();


                rend.sharedMaterial = materials[1];
                transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
                Built = false;
                buildMode.craft.Remove(buildMode.craft.Find(obj => obj.ItemCraft.name == gameObject.name));
                indicator._targetSpriteOfPool.gameObject.SetActive(false);

            } 
        }
    }

    private void OnTriggerStay (Collider other)
    {
        if (other.CompareTag("Player"))
        {


          
                BuildContruction(Built);
            

        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {

            buildMode.ButtonCraft.SetActive(false);
        }
    }
}
