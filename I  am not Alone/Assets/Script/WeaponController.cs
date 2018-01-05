using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
public class WeaponController : MonoBehaviour
{

    public GameObject Hand;

    public Image image;
    public AutoLookonEnemy autolookEnemy;
    public Transform handPlayer;
    public Animator m_anim;
    public GameObject[] Weapons;

    public Transform weaponPanel;
    public List<GameObject> WeaponImage = new List<GameObject>();
    public IKtarget ikTarget;
    public List<GameObject> weaponPreafab = new List<GameObject>();
    PoolingSystem pool;
    Transform AdvancedPoolingSystem;
    CheckInWeaponAndCraft _checkInWeaponCraft;
    SelectionWeaponForPC selectionWeaponPC;
    GameObject player;
    GameObject melee;

    Transform left;
    Transform right;
    IKweapon ikWeapon;

    PlayableDirector m_director;
    private void OnEnable ()
    {
        pool = PoolingSystem.Instance;
        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;
    }
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ikWeapon = player.GetComponent<IKweapon>();
        _checkInWeaponCraft = GetComponent<CheckInWeaponAndCraft>();
        pool = PoolingSystem.Instance;
        SelectionWeapon(0);
        AdvancedPoolingSystem = GameObject.Find("Advanced Pooling System").transform;
        selectionWeaponPC = GetComponent<SelectionWeaponForPC>();
        Ammunition(0);
    }


    public void SelectionWeapon (int IdWeapon)
    {

        switch (IdWeapon)
        {
            case 0:
                ResetWeapon();
                Hand.SetActive(true);

                Weapons[0].SetActive(false);
                Weapons[1].SetActive(false);
                m_anim.SetLayerWeight(1, 0);
                Weapons[2].SetActive(false);
                break;
            case 1:
                ResetWeapon();
                Hand.SetActive(false);
                Weapons[2].SetActive(false);

                BulletSystem weapon1 = Weapons[0].transform.GetChild(0).GetComponent<BulletSystem>();

                Weapons[0].transform.GetChild(0).localPosition = weapon1.WeaponPOsition;
                ikTarget.WeaponActive = Weapons[0].transform.GetChild(0);
                AnimationWeapon(weapon1.rightHand, weapon1.leftHand);
                ikTarget.GetBulletSystem();
                weapon1.UpdateAmunition();
                Weapons[0].SetActive(true);
                Weapons[1].SetActive(false);
                break;
            case 2:
                ResetWeapon();
                Hand.SetActive(false);
                Weapons[0].SetActive(false);
                Weapons[2].SetActive(false);

                BulletSystem weapon2 = Weapons[1].transform.GetChild(0).GetComponent<BulletSystem>();

                Weapons[1].transform.GetChild(0).localPosition = weapon2.WeaponPOsition;
                ikTarget.WeaponActive = Weapons[1].transform.GetChild(0);
                AnimationWeapon(weapon2.rightHand, weapon2.leftHand);
                ikTarget.GetBulletSystem();
                weapon2.UpdateAmunition();
                Weapons[1].SetActive(true);
                break;
            case 3:
                ResetWeapon();
                Hand.SetActive(false);
                Weapons[0].SetActive(false);
                Weapons[1].SetActive(false);
                BulletSystem weapon3 = Weapons[2].transform.GetChild(0).GetComponent<BulletSystem>();
                ikTarget.WeaponActive = Weapons[2].transform.GetChild(0);
                Weapons[2].transform.GetChild(0).localPosition = weapon3.WeaponPOsition;
                AnimationWeapon(weapon3.rightHand, weapon3.leftHand);
                ikTarget.GetBulletSystem();
                weapon3.UpdateAmunition();
                Weapons[2].SetActive(true);
                break;
            default:
                break;
        }

    }


    public void PlayerWeapon (string nameWeapon, int level, float amunition)
    {



        if (Weapons[0].transform.childCount != 0)
        {
            BulletSystem weapon1 = Weapons[0].transform.GetChild(0).GetComponent<BulletSystem>();
            if (Weapons[0].transform.GetChild(0).name.Equals(nameWeapon + "(Clone)"))
            {



                weapon1.WeaponAmmunition = 1;
                weapon1.resolution = true;
                //      Weapons[0].transform.GetChild(0).GetComponent<BulletSystem>().UpdateAmunition();
                m_director = weaponPanel.GetChild(0).GetComponent<PlayableDirector>();
                m_director.Play();
            }
        }

        if (Weapons[1].transform.childCount != 0)
        {
            BulletSystem weapon2 = Weapons[1].transform.GetChild(0).GetComponent<BulletSystem>();
            if (Weapons[1].transform.GetChild(0).name.Equals(nameWeapon + "(Clone)"))
            {



                weapon2.WeaponAmmunition = 1;

                weapon2.resolution = true;
                //     Weapons[1].transform.GetChild(0).GetComponent<BulletSystem>().UpdateAmunition();
                m_director = weaponPanel.GetChild(1).GetComponent<PlayableDirector>();
                m_director.Play();

            }
        }

        if (Weapons[2].transform.childCount != 0)
        {
            BulletSystem weapon3 = Weapons[2].transform.GetChild(0).GetComponent<BulletSystem>();
            if (Weapons[2].transform.GetChild(0).name.Equals(nameWeapon + "(Clone)"))
            {



                weapon3.WeaponAmmunition = 1;

                weapon3.resolution = true;
                //        Weapons[2].transform.GetChild(0).GetComponent<BulletSystem>().UpdateAmunition();
                m_director = weaponPanel.GetChild(2).GetComponent<PlayableDirector>();
                m_director.Play();
            }
        }

    }














    public void Ammunition (float value)
    {

        image.fillAmount = value;


    }

    public void ResetWeapon ()
    {

        ikWeapon.handRight = null;
        ikWeapon.handLeft = null;
    }


    public void AnimationWeapon (Transform right, Transform left)
    {
        ikWeapon.handRight = right;
        ikWeapon.handLeft = left;
    }

}
