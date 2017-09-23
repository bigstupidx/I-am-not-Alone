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

    public void Start ()
    {

        if (wood) NameMaterial = "Woods";

        if (metal) NameMaterial = "Metals";

        if (glass) NameMaterial = "Glasses";

        if (electric) NameMaterial = "Electrics";

        if (interactive) NameMaterial = "Interactive";


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
    public GameObject hisEffectPrefabPoolForDestroy;
    public bool mySelf;
    public bool BlowUpEffect;
    [Space(2)]
    public int damage;
    public float ExplosionRadios;
    public float ExplosionForce;
    public LayerMask effectLayer;
    [Space(15)]
    public List<ItemForbuild> CountWoodForCreate = new List<ItemForbuild>();
    public List<int> LevelHealth = new List<int>();
    public int level;
    public bool Built;
    public int Floor;
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
    bool ground;
    public GameObject[] NavmeshLinkWindow;
    public GameObject[] NavmeshLinkWindowOffToIntoTrigger;
    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < CountWoodForCreate.Count; i++)
        {
            CountWoodForCreate[i].Start();
        }
        pool = PoolingSystem.Instance;
        rigid = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        if (health == null)
        {
            health = transform.GetChild(0).GetComponent<Health>();
        }
        indicator = GetComponent<Indicator>();
        MainCanvas = GameObject.Find("MainCanvas");
        pointForMenu = transform.Find("PointForMenu").transform;
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();


        DefaultOptions();
    }


    public void DefaultForParticle ()
    {
        indicator.IndicatorSetActive(false, 0);
    }
    public void DefaultOptions ()
    {
        if (!Interactive)
        {
            ground = true;
            health.MaxHealth = health.CurHelth = LevelHealth[level];
        }
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            rend = transform.GetChild(0).GetChild(i).GetComponent<Renderer>();
            rend.enabled = true;
            rend.sharedMaterial = materials[0];
        }
        indicator.IndicatorOffScreen();
        indicator.IndicatorSetActive(true, 0);
        if (BuildStatic)
        {
            // indicator.IndicatorSetActive(true, 0);
            buildMode.craft.Add(new CraftParams(this.gameObject, indicator._targetSpriteOfPool.gameObject, Floor));
            ground = true;
            gameObject.SetActive(false);
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            if (NavmeshLinkWindow.Length != 0)
            {
                for (int i = 0; i < NavmeshLinkWindow.Length; i++)
                {
                    NavmeshLinkWindow[i].SetActive(true);
                }
                for (int i = 0; i < NavmeshLinkWindowOffToIntoTrigger.Length; i++)
                {
                    NavmeshLinkWindowOffToIntoTrigger[i].SetActive(true);
                }
            }
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
            if (Interactive)
            {
                ground = true;
            }
            if (BuildStatic)
            {
                ground = true;
            }
            if (ground)
            {
                for (int i = 0; i < transform.GetChild(0).childCount; i++)
                {
                    rend = transform.GetChild(0).GetChild(i).GetComponent<Renderer>();


                    rend.sharedMaterial = materials[1];


                }
                Built = true;
                buildMode.craft.Remove(buildMode.craft.Find(obj => obj.ItemCraft.name == gameObject.name));


                if (!BuildStatic)
                {
                    if (Interactive)
                    {
                        Item.InterectiveChangeParamsOrDestroy();
                    }
                    buildMode.CraftItemBuildNowDinamic = null;
                    rigid.isKinematic = true;
                    Item.CheckOFToggle();
                    indicator.IndicatorSetActive(false, 1);
                    indicator.IndicatorSetActive(true, 2);
                }
                else
                {
                    indicator.IndicatorSetActive(false, 0);
                    if (NavmeshLinkWindow.Length != 0)
                    {
                        for (int i = 0; i < NavmeshLinkWindow.Length; i++)
                        {
                            NavmeshLinkWindow[i].SetActive(false);
                        } 
                    }
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
                if (Built)
                {
                    buildMode.ButtonCraft.SetActive(false);
                }

            }
            if (other.CompareTag("AI"))
            {


                if (NavmeshLinkWindowOffToIntoTrigger.Length !=0)
                {
                    for (int i = 0; i < NavmeshLinkWindowOffToIntoTrigger.Length; i++)
                    {
                        other.GetComponent<ZombieLevel1>().timerStop = 1.0f;
                        other.GetComponent<ZombieLevel1>().TransformRotation(transform.parent);
                        health.HelthDamage(0.05f);
                          NavmeshLinkWindowOffToIntoTrigger[i].SetActive(false);
                    }

                }



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
        if (other.CompareTag("AI"))
        {



            other.GetComponent<ZombieLevel1>().WinDowAttack = false;





        }
        if (other.CompareTag("Player"))
        {

            buildMode.ButtonCraft.SetActive(false);
        }
    }

    public void His ()
    {
        hisEffectPrefabPoolForDestroy = pool.InstantiateAPS(hisEffectPrefab.name, transform.position, transform.rotation);
        _StartHisEffect = true;
        indicator.IndicatorSetActive(false, 0);
    }

    public void BlowUp ()
    {
        health.MySelfDestroyer();

        AddExposionForce(transform.position);
        indicator.IndicatorSetActive(false, 0);
    }

    void AddExposionForce (Vector3 centre)
    {
        Collider[] thingsHit = UnityEngine.Physics.OverlapSphere(transform.position, ExplosionRadios, effectLayer);
        foreach (Collider hit in thingsHit)
        {
            if (hit.GetComponent<Health>() != null)
            {
                hit.GetComponent<Health>().HelthDamage(damage);

            }
            if (hit.GetComponent<Rigidbody>() != null)
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, centre, ExplosionRadios, 1, ForceMode.Impulse);

            }
        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        ground = true;
    }
    private void OnCollisionExit (Collision collision)
    {
     
        ground = false;
    }
}
