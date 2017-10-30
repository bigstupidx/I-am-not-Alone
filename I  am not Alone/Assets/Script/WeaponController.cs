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
    public GameObject WeaponThree;
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
                WeaponThree.SetActive(false);
                break;
            case 1:
                ResetWeapon();
                Hand.SetActive(false);
                WeaponThree.SetActive(false);
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
                WeaponThree.SetActive(false);
                if (WeaponTwo.transform.childCount != 0)
                {



                    AnimationWeapon(WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().WeightWeapon);
                }
                WeaponTwo.SetActive(true);
                break;
            case 3:
                ResetWeapon();
                Hand.SetActive(false);
                WeaponOne.SetActive(false);
                WeaponTwo.SetActive(false);
                if (WeaponThree.transform.childCount != 0)
                {



                    AnimationWeapon(WeaponThree.transform.GetChild(0).GetComponent<BulletSystem>().WeightWeapon);
                }
                WeaponThree.SetActive(true);
                break;
            default:
                break;
        }

    }

    public void PlayerWeapon (string nameWeapon, int level, float amunition)
    {




        if (WeaponOne.transform.childCount == 0)
        {
            AddWeapon(nameWeapon, WeaponOne.transform, level, amunition);
            WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
            buttonWeaponOne = Instantiate(WeaponImagepref, weaponPanel);
            buttonWeaponOne.name = "buttonWeaponTwo" + WeaponOne;
            buttonWeaponOne.transform.GetChild(0).GetComponent<Image>().sprite = WeaponImage.Find(x => x.name == nameWeapon);
            buttonWeaponOne.transform.GetChild(0).GetComponent<Toggle>().group = buttonWeaponOne.transform.parent.GetComponent<ToggleGroup>();
            Button btn = buttonWeaponOne.GetComponent<Button>();
            btn.onClick.AddListener(selectionWeaponPC.Weapon1);
            WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().buttonWeapon = buttonWeaponOne.transform;
            return;
        }
        else
        {

            if (WeaponOne.transform.GetChild(0).name.Equals(nameWeapon + "(Clone)"))
            {


                if (WeaponOne.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition == 1.0f)
                {
                    RemoveWeapon(WeaponOne.transform.GetChild(0), nameWeapon);
                    AddWeapon(nameWeapon, WeaponOne.transform, level, amunition);
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
            AddWeapon(nameWeapon, WeaponTwo.transform, level, amunition);
            WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
            buttonWeaponOne = Instantiate(WeaponImagepref, weaponPanel);
            buttonWeaponOne.name = "buttonWeaponTwo" + WeaponTwo;
            buttonWeaponOne.transform.GetChild(0).GetComponent<Image>().sprite = WeaponImage.Find(x => x.name == nameWeapon);
            buttonWeaponOne.transform.GetChild(0).GetComponent<Toggle>().group = buttonWeaponOne.transform.parent.GetComponent<ToggleGroup>();
            Button btn = buttonWeaponOne.GetComponent<Button>();
            btn.onClick.AddListener(selectionWeaponPC.Weapon2);
            WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().buttonWeapon = buttonWeaponOne.transform;
            return;
        }
        else
        {

            if (WeaponTwo.transform.GetChild(0).name.Equals(nameWeapon + "(Clone)"))
            {


                if (WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition == 1.0f)
                {
                    RemoveWeapon(WeaponTwo.transform.GetChild(0), nameWeapon);
                    AddWeapon(nameWeapon, WeaponTwo.transform, level, amunition);
                    WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
                }
                else
                {
                    WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
                }

                WeaponTwo.transform.GetChild(0).GetComponent<BulletSystem>().buttonWeapon = buttonWeaponOne.transform;
                return;
            }

        }
        if (WeaponThree.transform.childCount == 0)
        {
            AddWeapon(nameWeapon, WeaponThree.transform, level, amunition);
            WeaponThree.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;

            buttonWeaponTwo = Instantiate(WeaponImagepref, weaponPanel);
            buttonWeaponTwo.name = "buttonWeaponTwo" + WeaponThree;
            buttonWeaponTwo.transform.GetChild(0).GetComponent<Image>().sprite = WeaponImage.Find(x => x.name == nameWeapon);
            buttonWeaponTwo.transform.GetChild(0).GetComponent<Toggle>().group = buttonWeaponTwo.transform.parent.GetComponent<ToggleGroup>();
            Button btn = buttonWeaponTwo.GetComponent<Button>();
            btn.onClick.AddListener(selectionWeaponPC.Weapon3);
            WeaponThree.transform.GetChild(0).GetComponent<BulletSystem>().buttonWeapon = buttonWeaponTwo.transform;
            return;
        }
        else
        {

            if (WeaponThree.transform.GetChild(0).name.Equals(nameWeapon + "(Clone)"))
            {


                if (WeaponThree.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition == 1.0f)
                {
                    RemoveWeapon(WeaponThree.transform.GetChild(0), nameWeapon);
                    AddWeapon(nameWeapon, WeaponThree.transform, level, amunition);
                    WeaponThree.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
                }
                else
                {
                    WeaponThree.transform.GetChild(0).GetComponent<BulletSystem>().WeaponAmmunition = 1;
                }
                WeaponThree.transform.GetChild(0).GetComponent<BulletSystem>().buttonWeapon = buttonWeaponTwo.transform;
                return;
            }
            else
            {

                buttonWeaponTwo.transform.GetChild(0).GetComponent<Image>().sprite = WeaponImage.Find(x => x.name == nameWeapon);





                RemoveWeapon(WeaponThree.transform.GetChild(0), nameWeapon);

                AddWeapon(nameWeapon, WeaponThree.transform, level, 1);
                WeaponThree.transform.GetChild(0).GetComponent<BulletSystem>().buttonWeapon = buttonWeaponTwo.transform;
                return;

            }




        }
    }












    public void AddWeapon (string name, Transform pos, int level, float amuni)
    {




        GameObject weapon = pool.InstantiateAPS(name, pos.position, pos.rotation, pos.gameObject);
        weapon.GetComponent<BulletSystem>().level = level;
        weapon.GetComponent<BulletSystem>().WeaponAmmunition = amuni;




    }
    public void RemoveWeapon (Transform Weapon, string nameWe)
    {


        _checkInWeaponCraft.OldWeapon(nameWe, Weapon.GetComponent<BulletSystem>(), null, player.transform.position);



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
