using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponController : MonoBehaviour
{

    public GameObject Hand;

    public Image image;
    public AutoLookonEnemy autolookEnemy;
    public Transform handPlayer;
    public Animator m_anim;
    public GameObject WeaponOne;
    public GameObject WeaponTwo;
    public Transform weaponPanel;
    public List<Sprite> WeaponImage = new List<Sprite>();
    public GameObject WeaponImagepref;
    PoolingSystem pool;
    Transform AdvancedPoolingSystem;
    CheckInWeaponAndCraft _checkInWeaponCraft;
    SelectionWeaponForPC selectionWeaponPC;
    GameObject player;
    GameObject melee;
    GameObject buttonWeaponOne;
    GameObject buttonWeaponTwo;
    Transform left;
    Transform right;
    private void OnEnable ()
    {
        pool = PoolingSystem.Instance;
        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;
    }
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _checkInWeaponCraft = GetComponent<CheckInWeaponAndCraft>();
        pool = PoolingSystem.Instance;
        SelectionWeapon(0);
        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;
        selectionWeaponPC = GetComponent<SelectionWeaponForPC>();
    }


    public void SelectionWeapon (int IdWeapon)
    {

        switch (IdWeapon)
        {
            case 0:
                ResetWeapon();
                Hand.SetActive(true);

                WeaponOne.SetActive(false);
                WeaponTwo.SetActive(false);
                m_anim.SetLayerWeight(1, 0);
                break;
            case 1:
                ResetWeapon();
                Hand.SetActive(false);
                if (WeaponOne.transform.childCount != 0)
                {
                    AnimationWeapon(WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().WeightWeapon);
                  

                }

                WeaponOne.SetActive(true);
                WeaponTwo.SetActive(false);
                break;
            case 2:
                ResetWeapon();
                Hand.SetActive(false);
                WeaponOne.SetActive(false);
                if (WeaponTwo.transform.childCount != 0)
                {
                 


                    AnimationWeapon(WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().WeightWeapon);
                }
                WeaponTwo.SetActive(true);
                break;
            default:
                break;
        }

    }

    public void PlayerWeapon (string nameWeapon, int category, int level, float amunition)
    {

        if (category == 0)
        {
            if (Hand.transform.GetChild(0).childCount == 0)
            {
                AddWeapon(nameWeapon, Hand.transform.GetChild(0), level, category, amunition);

                melee = Instantiate(WeaponImagepref, weaponPanel);
                melee.name = "buttonWeaponTwo" + melee;
                melee.transform.GetChild(0).GetComponent<Image>().sprite = WeaponImage.Find(x => x.name == nameWeapon);
                melee.transform.GetChild(0).GetComponent<Toggle>().group = melee.transform.parent.GetComponent<ToggleGroup>();
                Button btn = melee.GetComponent<Button>();
                btn.onClick.AddListener(selectionWeaponPC.Weapon1);
                Hand.GetComponent<handWeapon>().WeaponAmmunition = 1;
                Hand.GetComponent<handWeapon>().buttonWeapon = melee;
                return;
            }
            else
            {
                if (Hand.transform.GetChild(0).GetChild(0).name.Equals(nameWeapon + "(Clone)"))
                {

                    if (Hand.GetComponent<handWeapon>().WeaponAmmunition == 1)
                    {
                        RemoveWeapon(Hand.transform.GetChild(0).GetChild(0), nameWeapon, category);
                        AddWeapon(nameWeapon, Hand.transform.GetChild(0), level, category, amunition);

                    }
                    else
                    {
                        Hand.GetComponent<handWeapon>().WeaponAmmunition = 1;

                    }


                    return;
                }
                else
                {
                    melee.transform.GetChild(0).GetComponent<Image>().sprite = WeaponImage.Find(x => x.name == nameWeapon);
                    RemoveWeapon(Hand.transform.GetChild(0).GetChild(0), nameWeapon, category);
                    AddWeapon(nameWeapon, Hand.transform.GetChild(0), level, category, 1);
                    Hand.GetComponent<handWeapon>().buttonWeapon = melee;
                    return;
                }


            }

        }
        else
        {
            if (WeaponOne.transform.childCount == 0)
            {
                AddWeapon(nameWeapon, WeaponOne.transform, level, category, amunition);
                WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
                buttonWeaponOne = Instantiate(WeaponImagepref, weaponPanel);
                buttonWeaponOne.name = "buttonWeaponTwo" + WeaponOne;
                buttonWeaponOne.transform.GetChild(0).GetComponent<Image>().sprite = WeaponImage.Find(x => x.name == nameWeapon);
                buttonWeaponOne.transform.GetChild(0).GetComponent<Toggle>().group = buttonWeaponOne.transform.parent.GetComponent<ToggleGroup>();
                Button btn = buttonWeaponOne.GetComponent<Button>();
                btn.onClick.AddListener(selectionWeaponPC.Weapon2);
                WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().buttonWeapon = buttonWeaponOne.transform;
                return;
            }
            else
            {

                if (WeaponOne.transform.GetChild(0).name.Equals(nameWeapon + "(Clone)"))
                {


                    if (WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition == 1.0f)
                    {
                        RemoveWeapon(WeaponOne.transform.GetChild(0), nameWeapon, category);
                        AddWeapon(nameWeapon, WeaponOne.transform, level, category, amunition);
                        WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
                    }
                    else
                    {
                        WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
                    }

                    WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().buttonWeapon = buttonWeaponOne.transform;
                    return;
                }

            }
            if (WeaponTwo.transform.childCount == 0)
            {
                AddWeapon(nameWeapon, WeaponTwo.transform, level, category, amunition);
                WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;

                buttonWeaponTwo = Instantiate(WeaponImagepref, weaponPanel);
                buttonWeaponTwo.name = "buttonWeaponTwo" + WeaponTwo;
                buttonWeaponTwo.transform.GetChild(0).GetComponent<Image>().sprite = WeaponImage.Find(x => x.name == nameWeapon);
                buttonWeaponTwo.transform.GetChild(0).GetComponent<Toggle>().group = buttonWeaponTwo.transform.parent.GetComponent<ToggleGroup>();
                Button btn = buttonWeaponTwo.GetComponent<Button>();
                btn.onClick.AddListener(selectionWeaponPC.Weapon3);
                WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().buttonWeapon = buttonWeaponTwo.transform;
                return;
            }
            else
            {

                if (WeaponTwo.transform.GetChild(0).name.Equals(nameWeapon + "(Clone)"))
                {


                    if (WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition == 1.0f)
                    {
                        RemoveWeapon(WeaponTwo.transform.GetChild(0), nameWeapon, category);
                        AddWeapon(nameWeapon, WeaponTwo.transform, level, category, amunition);
                        WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
                    }
                    else
                    {
                        WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
                    }
                    WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().buttonWeapon = buttonWeaponTwo.transform;
                    return;
                }
                else
                {

                    buttonWeaponTwo.transform.GetChild(0).GetComponent<Image>().sprite = WeaponImage.Find(x => x.name == nameWeapon);





                    RemoveWeapon(WeaponTwo.transform.GetChild(0), nameWeapon, category);

                    AddWeapon(nameWeapon, WeaponTwo.transform, level, category, 1);
                    WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().buttonWeapon = buttonWeaponTwo.transform;
                    return;

                }




            }







        }

    }

    private void Update ()
    {



    }

    public void AddWeapon (string name, Transform pos, int level, int category, float amuni)
    {



        if (category == 1)
        {
            GameObject weapon = pool.InstantiateAPS(name, pos.position, pos.rotation, pos.gameObject);
            weapon.GetComponent<BulletSystem>().level = level;
            weapon.GetComponent<BulletSystem>().WeaponAmmunition = amuni;
        }
        else
        {
            GameObject weapon = pool.InstantiateAPS(name, pos.position, handPlayer.transform.rotation, pos.gameObject);
            Hand.GetComponent<handWeapon>().level = level;
            Hand.GetComponent<handWeapon>().WeaponAmmunition = amuni;
        }


    }
    public void RemoveWeapon (Transform Weapon, string nameWe, int category)
    {
        if (category == 0)
        {
            _checkInWeaponCraft.OldWeapon(nameWe, null, Hand.GetComponent<handWeapon>(), player.transform.position + new Vector3(4, 0, 4));
        }
        else
        {
            _checkInWeaponCraft.OldWeapon(nameWe, Weapon.GetComponent<BulletSystem>(), null, player.transform.position);
        }


        Weapon.gameObject.DestroyAPS();
        Weapon.transform.SetParent(AdvancedPoolingSystem);

    }


    public void Ammunition (float value)
    {

        image.fillAmount = value;


    }

    public void ResetWeapon ()
    {

        m_anim.SetBool("handAttack", false);
        m_anim.SetLayerWeight(1, 0);
    }


    public void AnimationWeapon (int weight)
    {
        m_anim.SetFloat("WeightWeapon", weight);


        m_anim.SetLayerWeight(1, 1);
    }

}
