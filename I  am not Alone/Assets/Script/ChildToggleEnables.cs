using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChildToggleEnables : MonoBehaviour
{

    ToggleGroup togGr;


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
}
