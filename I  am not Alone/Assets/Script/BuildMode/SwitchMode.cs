using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class SwitchMode : MonoBehaviour
{

    public GameObject BuildMode;
    public GameObject PlayerMode;

    public GameObject Hand;
    public GameObject HandWeapon;
    bool l;
    // Use this for initialization
    public List<GameObject> craft = new List<GameObject>();


    private void Start ()
    {


        l = false;
       
    }
    // Update is called once per frame
    void Update ()
    {

        if (Input.GetButtonDown("Build"))
        {
            l = !l;
            if (l)
            {

                BuildMode.SetActive(true);
                PlayerMode.SetActive(false);
                Hand.SetActive(false);
                HandWeapon.SetActive(false);
            }
            else
            {
          
                BuildMode.SetActive(false);
                PlayerMode.SetActive(true);
                Hand.SetActive(true);
                HandWeapon.SetActive(true);
            }
            CheckInBuiltWalls(l);
        }

    }

    void CheckInBuiltWalls (bool visible)
    {

        for (int i = 0; i < craft.Count; i++)
        {
            craft[i].gameObject.SetActive(visible);

        }


    }
}
