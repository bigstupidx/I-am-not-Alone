using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChildToggleEnables : MonoBehaviour
{

    ToggleGroup togGr;
    public SelectionWeaponForPC weapon;

    private void Start ()
    {
        togGr = GetComponent<ToggleGroup>();
    }
    public void EnablesToggleChild ()
    {
        togGr.allowSwitchOff = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<Toggle>().isOn = false;
        }
        togGr.allowSwitchOff = false;
    }

    public void ActiveFIrstWeapon ()
    {

        Button btn = transform.GetChild(0).GetComponent<Button>();
        weapon.Weapon1();
        transform.GetChild(0).GetChild(0).GetComponent<Toggle>().isOn = true;

    }
}
