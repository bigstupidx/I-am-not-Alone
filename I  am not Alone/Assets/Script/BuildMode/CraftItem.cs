using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class ItemForbuild
{
    public bool wood;
    public bool metal;
    public bool glass;
    public bool electric;
    public bool interactive;
    [HideInInspector]
    public string NameMaterial;
    public int CountMaterial;

    public ItemForbuild (bool w, bool m, bool g, bool e, bool i, string name, int count)
    {
        wood = w; if (w) name = "Woods";
        metal = m; if (w) name = "Metals";
        glass = g; if (w) name = "Glasses";
        electric = e; if (w) name = "Electrics";
        interactive = i; if (w) name = "Interactive";
        NameMaterial = name;
        CountMaterial = count;
    }


}






public class CraftItem : MonoBehaviour
{
    public bool BuildStatic = true;
    public bool Interactive;
    [Space(5)]
    public bool DamageObject;
    public bool hisEffect;
    public GameObject hisEffectPrefab;
    public bool mySelf;
    public bool BlowUpEffect;
    [Space(2)]
    public int damage;
    public float ExplosionRadios;
    public float ExplosionForce;
    public LayerMask effectLayer;
    [Space(15)]
    public List<ItemForbuild> CountWoodForCreate = new List<ItemForbuild>();
    public List<int> Level = new List<int>();
    public bool Built;

    public Material[] materials;
    [HideInInspector]
    public Transform pointForMenu;
    private SwitchMode buildMode;
    private Renderer rend;
    private GameObject MainCanvas;
    Indicator indicator;
    [HideInInspector]
    public Rigidbody rigid;
    [HideInInspector]
    public SelectContructionForCreate Item;
    Health health;
    PoolingSystem pool;
    [HideInInspector]
    public bool _StartHisEffect = false;
    // Use this for initialization
    void Start ()
    {
   
        pool = PoolingSystem.Instance;
        rigid = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
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

        }
        indicator.IndicatorOffscreen();
        if (BuildStatic)
        {
            buildMode.craft.Add(new CraftParams(this.gameObject, indicator._targetSpriteOfPool.gameObject));

            gameObject.SetActive(false);
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            rigid.isKinematic = false;
        }
        Built = false;
    }



    public void BuildContruction (bool b)
    {
        if (b)
        {
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                rend = transform.GetChild(0).GetChild(i).GetComponent<Renderer>();


                rend.sharedMaterial = materials[1];

                Built = false;
                buildMode.craft.Remove(buildMode.craft.Find(obj => obj.ItemCraft.name == gameObject.name));
                indicator._targetSpriteOfPool.gameObject.SetActive(false);

                if (!BuildStatic)
                {
                    if (Interactive)
                    {
                        Item.InterectiveChangeParamsOrDestroy();
                    }
                    buildMode.CraftItemBuildNowDinamic = null;
                    Item.itemCreate = null;
                    rigid.isKinematic = true;
                }
                else
                {
                    transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (!Built)
            {
                buildMode.CraftItemBuildNowStatic = this.gameObject.GetComponent<CraftItem>();
                if (BuildStatic)
                {
                    buildMode.ButtonCraft.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerStay (Collider other)
    {

        if (!DamageObject)
        {
            if (other.CompareTag("Player"))
            {

                BuildContruction(Built);

            }
            if (other.CompareTag("AI"))
            {







            }
        }
        else
        {
            if (other.CompareTag("AI"))
            {
                if (_StartHisEffect)
                {

                    other.GetComponent<Health>().HelthDamage(damage);
                }
                else if (!BlowUpEffect)
                {

                    if (mySelf)
                    {
                        health.MySelfDestroyer();

                    }


                    AddExposionForce(transform.position);

                }




            }

        }


    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {

            buildMode.ButtonCraft.SetActive(false);
        }
    }

    public void His ()
    {
        pool.InstantiateAPS(hisEffectPrefab.name, transform.position, transform.rotation);
        _StartHisEffect = true;
        indicator._blowUpYes.gameObject.SetActive(false);
    }

    public void BlowUp ()
    {
        health.MySelfDestroyer();
        //  other.GetComponent<Health>().HelthDamage(damage);
        AddExposionForce(transform.position);
        indicator._blowUpYes.gameObject.SetActive(false);
    }

    void AddExposionForce (Vector3 centre)
    {
        Collider[] thingsHit = UnityEngine.Physics.OverlapSphere(transform.position, ExplosionRadios, effectLayer);
        foreach (Collider hit in thingsHit)
        {
            if (hit.GetComponent<Rigidbody>() != null)
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, centre, ExplosionRadios, 1, ForceMode.Impulse);
                hit.GetComponent<Health>().HelthDamage(damage);
            }
        }
    }
}
