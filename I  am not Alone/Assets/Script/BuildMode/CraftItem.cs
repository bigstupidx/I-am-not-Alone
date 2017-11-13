using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public bool ItemDown;
    public SelectContructionForCreate Item;

    [HideInInspector]
    public bool _StartHisEffect = false;

    public GameObject[] NavmeshLinkWindow;
    CheckGround checkGround;
    //   public GameObject[] NavmeshLinkWindowOffToIntoTrigger;
    bool ColliderTrue;
    Health health;
    PoolingSystem pool;
    float timer;
    bool ground;
    BoxCollider thisCollider;
    private SwitchMode buildMode;
    private Renderer rend;
    PoolingSystem poolsistem;
    Indicator indicator;
    Ray ItemRay = new Ray();                       // A ray from the gun end forwards.
    RaycastHit hit;
    GameObject player;
    NavMeshObstacle obstacle;
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
        player = GameObject.FindGameObjectWithTag("Player");
        pointForMenu = transform.Find("PointForMenu").transform;
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        if (transform.GetComponent<NavMeshObstacle>())
        {
            obstacle = GetComponent<NavMeshObstacle>();
            obstacle.enabled = false;
        }

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
        poolsistem = PoolingSystem.Instance;

        if (!BuildStatic)
        {
            checkGround = transform.Find("RayTransform").GetComponent<CheckGround>();
            rigid = GetComponent<Rigidbody>();
            thisCollider = GetComponent<BoxCollider>();
            if (Interactive)
            {
                //  timer = 0.4f;
                //   ColliderTrue = true;
                rigid.useGravity = true;
                thisCollider.enabled = true;
            }
            else
            {
                ColliderTrue = true;
                rigid.useGravity = false;
                thisCollider.enabled = false;
                timer = 0.2f;
            }

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

    public void RenderOff ()
    {
        transform.GetComponent<Collider>().enabled = false;
        if (transform.GetChild(0).GetComponent<Collider>())
        {
            transform.GetChild(0).GetComponent<Collider>().enabled = false;
        }

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            if (transform.GetChild(0).GetChild(i).GetComponent<Renderer>())
            {
                rend = transform.GetChild(0).GetChild(i).GetComponent<Renderer>();
                rend.enabled = false;
            }
            for (int l = 0; l < transform.GetChild(0).GetChild(i).childCount; l++)
            {
                if (transform.GetChild(0).GetChild(i).GetChild(l).GetComponent<Renderer>())
                {
                    rend = transform.GetChild(0).GetChild(i).GetChild(l).GetComponent<Renderer>();
                    rend.enabled = false;
                }
            }

        }
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
            health.MaxHealth = health.CurHelth = LevelUpdate[level] * 1000.0f;
        }
        if (CustomScript)
        {
            turretsAi.damagePerShot = LevelUpdate[level];
            health.MaxHealth = health.CurHelth = LevelUpdate[level] * 1000f;
        }
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            if (transform.GetChild(0).GetChild(i).GetComponent<Renderer>())
            {
                rend = transform.GetChild(0).GetChild(i).GetComponent<Renderer>();
                rend.enabled = true;
                rend.sharedMaterial = materials[0];
            }
        }

        //indicator.IndicatorSetActive(true, 0);
        //indicator.IndicatorOffScreen();
        if (BuildStatic)
        {
            // indicator.IndicatorSetActive(true, 0);
            if (gameObject.layer == 0)
            {
                gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            }
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
                    //for (int i = 0; i < NavmeshLinkWindowOffToIntoTrigger.Length; i++)
                    //{
                    //    NavmeshLinkWindowOffToIntoTrigger[i].SetActive(true);
                    //}
                }
            }
        }
        else
        {
            if (ItemDown)
            {
                Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
            }
            rigid.isKinematic = false;
        }
        Built = false;
    }



    public void BuildContruction (bool b)
    {
        if (b)
        {
            ground = true;


            //if (BuildStatic)
            //{
            //    ground = true;
            //}
            //else
            //{

            //    ground = checkGround.GroundTrigger;

            //}



            if (ground)
            {
                for (int i = 0; i < transform.GetChild(0).childCount; i++)
                {
                    if (transform.GetChild(0).GetChild(i).GetComponent<Renderer>())
                    {
                        rend = transform.GetChild(0).GetChild(i).GetComponent<Renderer>();


                        rend.sharedMaterial = materials[1];
                        GameObject createEffect = poolsistem.InstantiateAPS("CreateEffect", transform.position, Quaternion.identity);
                        ParticleSystem ps = createEffect.GetComponent<ParticleSystem>();
                        var sh = ps.shape;
                        sh.shapeType = ParticleSystemShapeType.MeshRenderer;
                        sh.meshRenderer = transform.GetChild(0).GetChild(i).GetComponent<MeshRenderer>();
                    }


                    if (transform.GetChild(0).GetChild(i).childCount != 0)
                    {
                        for (int l = 0; l < transform.GetChild(0).GetChild(i).childCount; l++)
                        {
                            transform.GetChild(0).GetChild(i).GetChild(l).gameObject.SetActive(true);
                        }
                    }
                }

                if (gameObject.layer == 2)
                {
                    gameObject.layer = LayerMask.NameToLayer("Default");
                }
                for (var i = buildMode.craft.Count - 1; i > -1; i--)
                {
                    if (buildMode.craft[i].ItemCraft == null)
                        buildMode.craft.RemoveAt(i);
                }
                Built = true;
                buildMode.ButtonCraft.SetActive(false);

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
                    //indicator.IndicatorSetActive(false, 1);
                    //indicator.IndicatorSetActive(true, 2);
                    if (obstacle)
                    {
                        obstacle.enabled = true;
                    }
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
              


                if (buildMode.CraftItemBuildNowDinamic)
                {
                    buildMode.CraftItemBuildNowDinamic.Item.itemCreate.DestroyAPS();
                    buildMode.CraftItemBuildNowDinamic.Item.CheckOFToggle();
                    buildMode.CraftItemBuildNowDinamic = null;
                }

                if (BuildStatic)
                {
                    buildMode.CraftItemBuildNowStatic = this.gameObject.GetComponent<CraftItem>();
                    buildMode.ButtonCraft.SetActive(true);
                }
            }
            else
            {
                if (!BuildStatic)
                {
                    Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
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

                    if (buildMode.CraftItemBuildNowDinamic)
                    {
                        buildMode.CraftItemBuildNowDinamic.Item.itemCreate.DestroyAPS();
                        buildMode.CraftItemBuildNowDinamic.Item.CheckOFToggle();
                        buildMode.CraftItemBuildNowDinamic = null;
                    }

                }
                if (Built)
                {
                    buildMode.ButtonCraft.SetActive(false);
                }

            }
            if (other.CompareTag("AI"))
            {




                if (Built)
                {
                    if (other.GetComponent<ZombieLevel1>().JointWindow)
                    {

                        health.HelthDamage(0.03f, false,transform.position);
                        Animator anim = other.GetComponent<ZombieLevel1>().m_animator;
                        anim.SetTrigger("window");
                        OffMeshLinkData data = other.GetComponent<ZombieLevel1>().agent.currentOffMeshLinkData;
                        Vector3 startPos = other.GetComponent<ZombieLevel1>().agent.transform.position;
                        Vector3 endPos = data.endPos + Vector3.up * other.GetComponent<ZombieLevel1>().agent.baseOffset;
                        float normalizedTime = 0.0f;
                        while (normalizedTime < 1.0f)
                        {
                            float yOffset = 2.0f * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
                            other.GetComponent<ZombieLevel1>().agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                            normalizedTime += Time.deltaTime / 0.5f;

                        }
                        other.GetComponent<ZombieLevel1>().timerStop = 1.0f;



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

                        other.GetComponent<Health>().HelthDamage(damage, false,transform.position);
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
                            other.GetComponent<Health>().HelthDamage(damage, false,transform.position);
                            health.HelthDamage(0.1f, false, transform.position);
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
            if (buildMode.CraftItemBuildNowDinamic)
            {
                buildMode.CraftItemBuildNowDinamic.Item.itemCreate.DestroyAPS();
                buildMode.CraftItemBuildNowDinamic.Item.CheckOFToggle();
                buildMode.CraftItemBuildNowDinamic = null;
            }
            buildMode.CraftItemBuildNowStatic = null;

            buildMode.ButtonCraft.SetActive(false);

        }
    }

    public void His ()
    {
        hisEffectPrefabPoolForDestroy = pool.InstantiateAPS(hisEffectPrefab.name, transform.GetChild(0).GetChild(0).position, Quaternion.identity);
        hisEffectPrefabPoolForDestroy.GetComponent<GameObjectScaled>().scaleX = transform.GetChild(0).GetChild(0).GetComponent<GameObjectScaled>().scaleX;
        transform.GetChild(0).GetChild(0).GetComponent<GameObjectScaled>().prefabEffect = hisEffectPrefabPoolForDestroy;
        _StartHisEffect = true;
        indicator.IndicatorSetActive(false, 0);
    }

    public void BlowUp ()
    {
        health.MySelfDestroyer();

        AddExposionForce(transform.position);

       // indicator.IndicatorSetActive(false, 0);
    }

    void AddExposionForce (Vector3 centre)
    {
        Collider[] thingsHit = UnityEngine.Physics.OverlapSphere(transform.position, ExplosionRadios, effectLayer);
        foreach (Collider hit in thingsHit)
        {

            if (hit.GetComponent<Health>() != null)
            {
                hit.GetComponent<Health>().HelthDamage(damage, false,transform.position);

            }
            if (hit.GetComponent<Rigidbody>() != null)
            {
                if (hit.CompareTag("AI"))
                {
                    hit.GetComponent<ZombieLevel1>().agent.enabled = false;
                    hit.GetComponent<ZombieLevel1>().RigidExplosion = true;

                    hit.GetComponent<Rigidbody>().isKinematic = false;
                    hit.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, centre, ExplosionRadios, 1, ForceMode.Impulse);

                }
                else
                {

                }
                hit.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, centre, ExplosionRadios, 1, ForceMode.Impulse);

            }
        }
    }

    //private void OnCollisionEnter (Collision collision)
    //{
    //    ground = true;
    //}
    //private void OnCollisionExit (Collision collision)
    //{

    //    ground = false;
    //}
}
