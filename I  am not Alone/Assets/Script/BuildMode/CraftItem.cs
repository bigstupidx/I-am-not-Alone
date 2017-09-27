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
    public bool CustomScript;
    public GameObject hisEffectPrefab;
    public GameObject hisEffectPrefabPoolForDestroy;
    public bool mySelf;
    public bool BlowUpEffect;
    [Space(2)]
    public float damage;
    public float ExplosionRadios;
    public float ExplosionForce;
    public LayerMask effectLayer;
    [Space(15)]
    public List<ItemForbuild> CountWoodForCreate = new List<ItemForbuild>();
    public List<float> LevelUpdate = new List<float>();
    public int level;
    public bool Built;
    public int Floor;
    public Material[] materials;
    [HideInInspector]
    public Transform pointForMenu;
    public TurretsAi turretsAi;
    [HideInInspector]
    public Rigidbody rigid;
    [HideInInspector]
    public SelectContructionForCreate Item;

    [HideInInspector]
    public bool _StartHisEffect = false;

    public GameObject[] NavmeshLinkWindow;
    public GameObject[] NavmeshLinkWindowOffToIntoTrigger;
    bool ColliderTrue;
    Health health;
    PoolingSystem pool;
    float timer;
    bool ground;
    BoxCollider thisCollider;
    private SwitchMode buildMode;
    private Renderer rend;
    private GameObject MainCanvas;
    Indicator indicator;
    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < CountWoodForCreate.Count; i++)
        {
            CountWoodForCreate[i].Start();
        }
        pool = PoolingSystem.Instance;

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
    private void Update ()
    {
        if (ColliderTrue)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                rigid.useGravity = true;
                ColliderTrue = false;
                thisCollider.enabled = true;
            }
        }
    }

    private void OnEnable ()
    {

        if (!BuildStatic)
        {
            rigid = GetComponent<Rigidbody>();
            thisCollider = GetComponent<BoxCollider>();
            timer = 0.2f;
            ColliderTrue = true;
            rigid.useGravity = false;
            thisCollider.enabled = false;
        }
        else
        {
            ColliderTrue = false;

        }

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
            health.MaxHealth = health.CurHelth = LevelUpdate[level];
        }
        if (DamageObject)
        {
            damage = LevelUpdate[level];
            health.MaxHealth = health.CurHelth = LevelUpdate[level] * 10;
        }
        if (CustomScript)
        {
            turretsAi.damagePerShot = LevelUpdate[level];
            health.MaxHealth = health.CurHelth = LevelUpdate[level] * 10;
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

                    if (transform.GetChild(0).GetChild(i).childCount != 0)
                    {
                        for (int l = 0; l < transform.GetChild(0).GetChild(i).childCount; l++)
                        {
                            transform.GetChild(0).GetChild(i).GetChild(l).gameObject.SetActive(true);
                        }
                    }
                }
                for (var i = buildMode.craft.Count - 1; i > -1; i--)
                {
                    if (buildMode.craft[i].ItemCraft == null)
                        buildMode.craft.RemoveAt(i);
                }
                Built = true;


                buildMode.ChangeMaterial();
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
                    buildMode.craft.Remove(buildMode.craft.Find(obj => obj.ItemCraft.name == gameObject.name));
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
        if (other.CompareTag("AI"))
        {


            if (Interactive)
            {
                other.GetComponent<ZombieLevel1>().agent.speed -= 3;

            }
        }
    }
    private void OnTriggerStay (Collider other)
    {

        if (!DamageObject)
        {
            if (other.CompareTag("Player"))
            {

                if (!Built)
                {
                    BuildContruction(Built);
                }
                if (Built)
                {
                    buildMode.ButtonCraft.SetActive(false);
                }

            }
            if (other.CompareTag("AI"))
            {




                if (other.GetComponent<ZombieLevel1>().JointWindow)
                {
                    if (NavmeshLinkWindowOffToIntoTrigger.Length != 0)
                    {
                        for (int i = 0; i < NavmeshLinkWindowOffToIntoTrigger.Length; i++)
                        {
                            other.GetComponent<ZombieLevel1>().timerStop = 1.0f;
                            other.GetComponent<ZombieLevel1>().TransformRotation(transform.parent);
                            health.HelthDamage(0.05f,false);
                            NavmeshLinkWindowOffToIntoTrigger[i].SetActive(false);
                        }

                    }
                }



            }
        }
        else
        {
            if (other.CompareTag("AI"))
            {

                if (hisEffect)
                {

                    if (_StartHisEffect)
                    {

                        other.GetComponent<Health>().HelthDamage(damage, false);
                    }
                }
                else
                {
                    if (!BlowUpEffect)
                    {

                        if (mySelf)
                        {
                            health.MySelfDestroyer();
                            AddExposionForce(transform.position);

                        }
                        else
                        {
                            other.GetComponent<Health>().HelthDamage(damage, false);
                        }




                    }
                }







            }

        }


    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("AI"))
        {



            //    other.GetComponent<ZombieLevel1>().WinDowAttack = false;

            other.GetComponent<ZombieLevel1>().agent.speed = other.GetComponent<ZombieLevel1>().standartSpeed;



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
                hit.GetComponent<Health>().HelthDamage(damage, false);

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
